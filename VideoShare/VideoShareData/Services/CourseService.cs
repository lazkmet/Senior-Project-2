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

namespace VideoShareData.Services
{
    public interface ICourseService {
        Task<Course?> GetCourseByIdAsync(int courseID);
        Task<int> CreateCourseAsync(int ownerID);
        Task<Course> UpdateCourseAsync(Course courseToUpdate, List<string> propertiesUpdated);
        Task<int> GetCompletionPercentageAsync(int userID, int courseID);
        IQueryable<Video> GetVideosQueryableOrdered(WebAppDbContext context, int courseID);
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
        public async Task<int> CreateCourseAsync(int ownerID)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            string courseCode = await GenerateCourseCodeAsync();
            var newCourse = new Course {CourseCode = courseCode, OwnerId = ownerID, CourseName = "New Course"};
            context.Add(newCourse);
            await context.SaveChangesAsync();
            return newCourse.CourseId;
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

        public async Task<string> GenerateCourseCodeAsync() {
            //Generates a course code that does not already exist in the database.
            //Unsuitable for large scale applications - repeat generation occurs more often as database fills up
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
    }
}
