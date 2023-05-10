using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VideoShareData.Models;
using VideoShareData.Enums;
using VideoShareData.DTOs;

namespace VideoShareData.Services
{
    public interface ICourseService {
        Task<Course?> GetCourseByIdAsync(int courseID);
        Task<ServiceTaskResults<Course?>> CreateCourseAsync(int ownerID);
        Task<Course> UpdateCourseAsync(Course courseToUpdate, List<string> propertiesUpdated);
        Task<ServiceTaskResults<Course>> UpdateCourseAsync(EditCourseModel courseToUpdate);
        Task<int> GetCompletionPercentageAsync(int userID, int courseID);
        IQueryable<Video> GetVideosQueryableOrdered(WebAppDbContext context, int courseID);
        Task<ServiceTaskResults<bool>> CheckUserInCourseAsync(int userID, int courseID);
        Task<ServiceTaskResults<UserxCourse?>> AddUserToCourseAsync(int userID, string courseCode);
        Task<ServiceTaskResults<List<UserxCourse>>> GetUserJoinedCoursesOrderedAsync(int userID, VideoShareData.Enums.SortOrder order);
        //TODO: Delete Course Async
    }
    public class CourseService : ICourseService
    {
        private readonly IDbContextFactory<WebAppDbContext> _contextFactory;

        public CourseService(IDbContextFactory<WebAppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Course?> GetCourseByIdAsync(int courseID)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Courses.FindAsync(courseID);
        }
        public async Task<ServiceTaskResults<Course?>> CreateCourseAsync(int ownerID)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            string courseCode = await GenerateCourseCodeAsync();
            var newCourse = new Course {CourseCode = courseCode, OwnerId = ownerID, CourseName = "New Course"};
            context.Add(newCourse);
            await context.SaveChangesAsync();
            return new ServiceTaskResults<Course?>() {TaskSuccessful = true, ReturnValue = newCourse};
        }

        public async Task<int> GetCompletionPercentageAsync(int userID, int courseID)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var userParam = new SqlParameter("@userID", userID);
            var courseParam = new SqlParameter("@courseID", courseID);
            try
            {
                return await context.Database.SqlQueryRaw<int>($"SELECT dbo.UDF_CompletionPercentage(@userID, @courseID) AS Value", userParam, courseParam).SingleAsync();
            }
            catch
            {
                return -1; //Very clearly an error value. Indicates that there was a problem executing the query.
            }
        }

        public IQueryable<Video> GetVideosQueryableOrdered(WebAppDbContext context, int courseID)
        {
            return context.Videos.Where(v => v.CourseId == courseID).OrderBy(v => v.OrderInCourse).AsQueryable();
        }

        public async Task<Course> UpdateCourseAsync(Course courseToUpdate, List<string> propertiesUpdated)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Attach(courseToUpdate);
            if (propertiesUpdated.Count > 0)
            {
                propertiesUpdated.ForEach(x => context.Entry(courseToUpdate).Property(x).IsModified = true);
            }
            await context.SaveChangesAsync();
            return courseToUpdate;
        }
        public async Task<ServiceTaskResults<Course>> UpdateCourseAsync(EditCourseModel courseToUpdate) {
            if (courseToUpdate is null) {
                return new ServiceTaskResults<Course> { TaskSuccessful = false, TaskMessage = "Edit Course Model is null" };
            }
            using var context = await _contextFactory.CreateDbContextAsync();
            var course = await context.Courses.Where(c => c.CourseId == courseToUpdate.CourseId).FirstOrDefaultAsync();
            if (course is null) {
                return new ServiceTaskResults<Course> { TaskSuccessful = false, TaskMessage = "Course not found" };
            }
            course.CourseName = courseToUpdate.CourseName;
            course.CourseDescription = courseToUpdate.CourseDescription;
            course.LessonLimitType = courseToUpdate.LessonLimitType;
            await context.SaveChangesAsync();
            return new ServiceTaskResults<Course> { TaskSuccessful = true, ReturnValue = course };
        }

        public async Task<string> GenerateCourseCodeAsync() {
            //Generates a course code that does not already exist in the database.
            //Unsuitable for large scale applications - repeat generation will occur more often as database fills up
            byte[] bytes;
            string returnValue;
            char[] validchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            int charsLength = validchars.Length;
            returnValue = await Task.Run(() =>
            {
                using var context = _contextFactory.CreateDbContext();
                string newCode;
                do
                {
                    newCode = "";
                    bytes = RandomNumberGenerator.GetBytes(6);
                    foreach (byte b in bytes)
                    {
                        newCode += validchars[b % charsLength];
                    }
                } while (context.Courses.Any(c => c.CourseCode == newCode));
                return newCode;
            });
            return returnValue;
        }

        public async Task<ServiceTaskResults<bool>> CheckUserInCourseAsync(int userID, int courseID) {
            using var context = await _contextFactory.CreateDbContextAsync();
            bool result = await context.UserxCourses.AnyAsync(uc => uc.UserId == userID && uc.CourseId == courseID);
            return new ServiceTaskResults<bool> { TaskSuccessful = true, ReturnValue = result };
        }
        public async Task<ServiceTaskResults<UserxCourse?>> AddUserToCourseAsync(int userID, string courseCode) { 
            using var context = await _contextFactory.CreateDbContextAsync();
            UserxCourse? newRelation = null;
            if (!(await context.Users.AnyAsync(u => u.UserId == userID))) {
                //You are here if the user ID does not exist
                return new ServiceTaskResults<UserxCourse?> { TaskSuccessful = false, TaskMessage = "The User to add does not exist"};
            }
            var course = await context.Courses.Where(c => c.CourseCode == courseCode).AsNoTracking().FirstOrDefaultAsync();
            if (course is not null)
            {
                if (course.OwnerId == userID)
                {
                    //You are here if the user you are attempting to add is the owner of the course
                    //you are trying to add them to.
                    return new ServiceTaskResults<UserxCourse?> { TaskSuccessful = false, TaskMessage = "A course owner cannot join their own course" };
                }
                else if (await context.UserxCourses.AnyAsync(uc => uc.UserId == userID && uc.CourseId == course.CourseId))
                {
                    var result = await this.CheckUserInCourseAsync(userID, course.CourseId);
                    if (result.ReturnValue) {
                        //You are here if there is already a relation for this User and Course combination
                        return new ServiceTaskResults<UserxCourse?> { TaskSuccessful = false, TaskMessage = "The User to add is already a member of that course" };
                    }
                }
                /*Now that you have verified that:
                    -The userID is valid
                    -The course code is valid
                    -The user is not the owner of the course
                    -The user is not already a member of the course,
                you can finally add the relation to the database*/
                newRelation = new UserxCourse()
                {
                    UserId = userID,
                    CourseId = course.CourseId
                };
                await context.AddAsync(newRelation);
                await context.SaveChangesAsync();
                return new ServiceTaskResults<UserxCourse?> { TaskSuccessful = true, ReturnValue = newRelation };
            }
            else {
                //You are here if the course code does not match an existing course
                return new ServiceTaskResults<UserxCourse?> { TaskSuccessful = false, TaskMessage = "Invalid course code" };
            }
        }
        public async Task<ServiceTaskResults<List<UserxCourse>>> GetUserJoinedCoursesOrderedAsync(int userID, VideoShareData.Enums.SortOrder order) {
            List<UserxCourse> returnList = new List<UserxCourse>();
            using var context = await _contextFactory.CreateDbContextAsync();
            switch (order) {
                case Enums.SortOrder.Unordered: {
                        returnList = await context.UserxCourses.Where(uc => uc.UserId == userID).ToListAsync();
                    break;
                }
                case Enums.SortOrder.AscendingAlphabetical: {
                        returnList = await context.UserxCourses.Where(uc => uc.UserId == userID).OrderBy(uc => uc.Course.CourseName).ToListAsync();
                        break;
                }
                case Enums.SortOrder.MostRecent: {
                        returnList = await context.UserxCourses.Where(uc => uc.UserId == userID).OrderByDescending(uc => context.UDF_CourseMostRecentVisit(uc.UserId, uc.CourseId)).ToListAsync();
                        break;
                }
                case Enums.SortOrder.DescendingCompletion:{
                        returnList = await context.UserxCourses.Where(uc => uc.UserId == userID).OrderByDescending(uc => context.UDF_CompletionPercentage(uc.UserId, uc.CourseId)).ToListAsync();
                        break;
                }
            }
            return new ServiceTaskResults<List<UserxCourse>> { TaskSuccessful = true, ReturnValue = returnList };
        }
    }
}
