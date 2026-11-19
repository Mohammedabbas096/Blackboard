using System;
using System.Linq;

namespace Group1Blackboard.Models
{
    public static class Check
    {
        public static string EmailExists(BlackboardContext context, string email, int userId = 0)
        {
            if (string.IsNullOrWhiteSpace(email))
                return string.Empty;

            string normalizedEmail = email.Trim().ToLower();
            bool exists = context.Users.Any(u =>
                u.UserId != userId &&
                u.Email.ToLower() == normalizedEmail);

            return exists ? $"Email address {email} is already in use." : string.Empty;
        }

        public static string CourseNumberExists(BlackboardContext context, string courseNumber, int courseId = 0)
        {
            if (string.IsNullOrWhiteSpace(courseNumber))
                return string.Empty;

            string normalizedCourseNumber = courseNumber.Trim().ToLower();
            bool exists = context.Courses.Any(c =>
                c.CourseId != courseId &&
                c.CourseNumber.ToLower() == normalizedCourseNumber);

            return exists ? $"Course number {courseNumber} is already in use." : string.Empty;
        }
    }
}

