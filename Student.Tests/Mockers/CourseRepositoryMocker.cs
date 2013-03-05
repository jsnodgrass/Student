using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Student.Data;
using Student.Domain;

namespace Student.Tests.Mockers
{
    public static class CourseRepositoryMocker
    {
        public static ICourseRepository MockUpRepository()
        {
            return GetCourseRepositoryMock().Object;
        }

        public static Mock<ICourseRepository> GetCourseRepositoryMock()
        {
            var mockCourseRepository = new Mock<ICourseRepository>();

            List<Course> courses = CreateCourses();
            mockCourseRepository.Setup(r => r.Get<Course>()).Returns(courses.AsQueryable());
            mockCourseRepository.Setup(r => r.Delete(It.IsAny<Course>())).Callback((Course course) => courses.Remove(course));
            mockCourseRepository.Setup(r => r.Create(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));
            mockCourseRepository.Setup(r => r.Update(It.IsAny<Course>())).Callback((Course course) =>
            {
                Course courseToEdit = courses.Single(s => s.Id == course.Id);
                courseToEdit = course;
            });

            return mockCourseRepository;
        }

        private static List<Course> CreateCourses()
        {
            Course course1 = new Course();
            course1.Id = 1;
            course1.Name = "History";

            Course course2 = new Course();
            course2.Id = 2;
            course2.Name = "English";

            Course course3 = new Course();
            course3.Id = 3;
            course3.Name = "Science";

            return new List<Course> { course1, course2, course3 };
        }
    }
}
