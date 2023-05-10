using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoShareData.DTOs;
using VideoShareData.Models;
using VideoShareData.Enums;

namespace VideoShareData.Services
{
    public interface IVideoService {
        Task<ServiceTaskResults<Video?>> GetVideoByIdAsync(int videoID);
        Task<ServiceTaskResults<UserxVideo?>> GetUserVideoByIdAsync(int userID, int videoID);
        Task<ServiceTaskResults<UserxVideo?>> VisitUserVideoAsync(int userID, int videoID, bool allowCreate = false);
        Task<ServiceTaskResults<Video?>> CreateVideoAsync(EditVideoModel newVideo, int courseID, bool checkID = true);
        Task<ServiceTaskResults<Video>> UpdateVideoAsync(Video videoToUpdate, List<string> attributesUpdated);
        Task<ServiceTaskResults<Video>> UpdateVideoAsync(EditVideoModel videoToUpdate);
        Task<ServiceTaskResults<UserxVideo>> UpdateUserVideoAsync(UserxVideo uvToUpdate, List<string> attributesUpdated);
        Task<ServiceTaskResults<UserxVideo?>> CreateUserVideoAsync(int userID, int videoID, bool checkExists = true);
        Task<ServiceTaskResults<bool>> DeleteVideoAsync(int videoID);
    }
    public class VideoService : IVideoService
    {
        private readonly IDbContextFactory<WebAppDbContext> _contextFactory;

        public VideoService(IDbContextFactory<WebAppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<ServiceTaskResults<Video?>> GetVideoByIdAsync(int videoID)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            Video? video = await context.Videos.FindAsync(videoID);
            if (video is null)
            {
                return new ServiceTaskResults<Video?>() { TaskSuccessful = false, ReturnValue = video };
            }
            else {
                return new ServiceTaskResults<Video?>() { TaskSuccessful = true, ReturnValue = video };
            }
        }
        public async Task<ServiceTaskResults<UserxVideo?>> GetUserVideoByIdAsync(int userID, int videoID) {
            using var context = await _contextFactory.CreateDbContextAsync();
            UserxVideo? video = await context.UserxVideos.FindAsync(userID, videoID);
            if (video is null)
            {
                return new ServiceTaskResults<UserxVideo?>() { TaskSuccessful = false, ReturnValue = video };
            }
            else
            {
                return new ServiceTaskResults<UserxVideo?>() { TaskSuccessful = true, ReturnValue = video };
            }
        }
        public async Task<ServiceTaskResults<UserxVideo?>> VisitUserVideoAsync(int userID, int videoID, bool allowCreate = false)
        {
            //This task also updates the LastVisited attribute of the UserxVideo
            UserxVideo? uv = null;
            var results = await this.GetUserVideoByIdAsync(userID, videoID);
            if (results.TaskSuccessful)
            {
                //You are here if a UserxVideo exists for the user and video provided in args.
                uv = results.ReturnValue;
                uv.LastVisited = DateTime.Now;
                var results2 = await this.UpdateUserVideoAsync(uv, new List<string>() { "LastVisited" });
                if (results2.TaskSuccessful)
                {
                    uv = results2.ReturnValue;
                    return new ServiceTaskResults<UserxVideo?> { TaskSuccessful = true, ReturnValue = uv };
                }
                else
                {
                    return new ServiceTaskResults<UserxVideo?> { TaskSuccessful = false, TaskMessage = "Unexpected problem updating LastVisited" };
                }
            }
            else if (allowCreate)
            {
                var results2 = await this.CreateUserVideoAsync(userID, videoID, false);
                //Don't need to checkExists, because I already verified it with the above task
                if (results2.TaskSuccessful)
                {
                    //You are here if a new UserxVideo was created for the userID and videoID
                    uv = results2.ReturnValue;
                    return new ServiceTaskResults<UserxVideo?> { TaskSuccessful = true, ReturnValue = uv };
                }
                else {
                    return new ServiceTaskResults<UserxVideo?> { TaskSuccessful = false, TaskMessage = "Unexpected error creating new UserxVideo", ReturnValue = uv };
                }
            }
            else {
                //You are here if the UserxVideo requested does not exist, and the user specified that you should not create a new one
                return new ServiceTaskResults<UserxVideo?> { TaskSuccessful = false, TaskMessage = "Could not get or create UserxVideo", ReturnValue = uv };
            }
        }
        public async Task<ServiceTaskResults<Video?>> CreateVideoAsync(EditVideoModel newVideo, int courseID, bool checkID = true) {
            if (newVideo is null) 
            {
                return new ServiceTaskResults<Video?> { TaskSuccessful = false, TaskMessage = "Video Edit Model is null", ReturnValue = null };
            }

            using var context = await _contextFactory.CreateDbContextAsync();

            if (checkID)
            {
                if (!await context.Courses.AnyAsync(c => c.CourseId == courseID))
                {
                    return new ServiceTaskResults<Video?> { TaskSuccessful = false, TaskMessage = "Provided course ID does not exist", ReturnValue = null };
                }
            }

            Video created = new Video() { CourseId = courseID, VideoTitle = newVideo.VideoTitle, VideoDescription = newVideo.VideoDescription, OrderInCourse = newVideo.orderInCourse };
            switch (newVideo)
            {
                case YouTubeEditVideoModel:
                    var yv = (YouTubeEditVideoModel)newVideo;
                    created.YtvideoId = yv.YouTubeID;
                    created.YtuseDescription = yv.UseYTDescription;
                    break;
                default:
                    return new ServiceTaskResults<Video?> { TaskSuccessful = false, TaskMessage = "Unsupported Video type", ReturnValue = null };
            }

            await context.AddAsync(created);
            await context.SaveChangesAsync();

            return new ServiceTaskResults<Video?> { TaskSuccessful = true, ReturnValue = created };            
        }
        public async Task<ServiceTaskResults<Video>> UpdateVideoAsync(EditVideoModel videoToUpdate) {
            if (videoToUpdate is null) {
                return new ServiceTaskResults<Video> { TaskSuccessful = false, TaskMessage = "Video Edit Model is null" };
            }

            using var context = await _contextFactory.CreateDbContextAsync();

            var video = await context.Videos.FindAsync(videoToUpdate.VideoId);

            if (video is null) {
                return new ServiceTaskResults<Video> { TaskSuccessful = false, TaskMessage = "Video could not be found" };
            }

            video.VideoTitle = videoToUpdate.VideoTitle;
            video.VideoDescription = videoToUpdate.VideoDescription;
            video.OrderInCourse = videoToUpdate.orderInCourse;

            switch (videoToUpdate) {
                case YouTubeEditVideoModel:
                    var yv = (YouTubeEditVideoModel)videoToUpdate;
                    video.VideoType = VideoType.Youtube;
                    video.YtvideoId = yv.YouTubeID;
                    video.YtuseDescription = yv.UseYTDescription;
                    break;
                default:
                    return new ServiceTaskResults<Video> { TaskSuccessful = false, TaskMessage = "Unsupported video type" };
            }

            //TODO: Add/Remove Attachments
            await context.SaveChangesAsync();
            return new ServiceTaskResults<Video> { TaskSuccessful = true, ReturnValue = video };
        }
        public async Task<ServiceTaskResults<Video>> UpdateVideoAsync(Video videoToUpdate, List<string> attributesUpdated)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Attach(videoToUpdate);
            if (attributesUpdated.Count > 0)
            {
                attributesUpdated.ForEach(x => context.Entry(videoToUpdate).Property(x).IsModified = true);
            }
            await context.SaveChangesAsync();
            return new ServiceTaskResults<Video> { TaskSuccessful = true, ReturnValue = videoToUpdate};
        }
        public async Task<ServiceTaskResults<UserxVideo>> UpdateUserVideoAsync(UserxVideo uvToUpdate, List<string> attributesUpdated)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Attach(uvToUpdate);
            if (attributesUpdated.Count > 0)
            {
                attributesUpdated.ForEach(x => context.Entry(uvToUpdate).Property(x).IsModified = true);
            }
            await context.SaveChangesAsync();
            return new ServiceTaskResults<UserxVideo> {TaskSuccessful = true, ReturnValue = uvToUpdate };
        }
        public async Task<ServiceTaskResults<UserxVideo?>> CreateUserVideoAsync(int userID, int videoID, bool checkExists = true) {
            if (checkExists) {
                var results = await this.GetUserVideoByIdAsync(userID, videoID);
                if (results.TaskSuccessful) {
                    return new ServiceTaskResults<UserxVideo?> { TaskSuccessful = false, TaskMessage = "Could not create UserxVideo One already exists"};
                }
            }
            using var context = await _contextFactory.CreateDbContextAsync();
            UserxVideo newUV = new UserxVideo { UserId = userID, VideoId = videoID, LastVisited = DateTime.Now};
            context.Add(newUV);
            await context.SaveChangesAsync();
            return new ServiceTaskResults<UserxVideo?> { TaskSuccessful = true, ReturnValue = newUV};
        }
        public async Task<ServiceTaskResults<bool>> DeleteVideoAsync(int videoID)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var videoToDelete = await context.Videos.Where(v => v.VideoId == videoID).Include(v => v.UserxVideos).FirstOrDefaultAsync();
            if (videoToDelete is null)
            {
                return new ServiceTaskResults<bool> { TaskSuccessful = false, TaskMessage = "Video not found", ReturnValue = false };
            }

            try
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                context.RemoveRange(videoToDelete.UserxVideos);
                context.SaveChanges();
                context.Remove(videoToDelete);
                context.SaveChanges();
                await transaction.CommitAsync();
            }
            catch (Exception ex) {
                return new ServiceTaskResults<bool> { TaskSuccessful = false, TaskMessage = ex.Message, ReturnValue = false };
            }
            return new ServiceTaskResults<bool> { TaskSuccessful = true, ReturnValue = true };
        }
    }
}
