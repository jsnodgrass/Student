using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Student.Data;
using Student.Domain;

namespace Student.Tests.Mockers
{
    public static class CourseStudentRepositoryMocker
    {
        public static ICourseStudentRepository MockUpRepository()
        {
            return GetCourseStudentRepositoryMock().Object;
        }

        public static Mock<ICourseStudentRepository> GetCourseStudentRepositoryMock()
        {
            var mockCourseStudentRepository = new Mock<ICourseStudentRepository>();

            List<CourseStudent> courseStudents = CreateCourseStudents();
            mockCourseStudentRepository.Setup(r => r.Get<CourseStudent>()).Returns(courseStudents.AsQueryable());
            mockCourseStudentRepository.Setup(r => r.Create(It.IsAny<CourseStudent>())).Callback((CourseStudent courseStudent) => courseStudents.Add(courseStudent));
            mockCourseStudentRepository.Setup(r => r.Delete(It.IsAny<CourseStudent>())).Callback((CourseStudent courseStudent) => courseStudents.Remove(courseStudent));

            return mockCourseStudentRepository;
        }

        private static List<CourseStudent> CreateCourseStudents()
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


            MainStudent student1 = new MainStudent();
            student1.Id = Guid.NewGuid();
            student1.FirstName = "Jason";
            student1.LastName = "Snodgrass";

            MainStudent student2 = new MainStudent();
            student2.Id = Guid.NewGuid();
            student2.FirstName = "John";
            student2.LastName = "Snow";


            CourseStudent courseStudent1 = new CourseStudent();
            courseStudent1.StudentId = student1.Id;
            courseStudent1.CourseId = 1;
            courseStudent1.Course = course1;
            courseStudent1.Id = 1;

            CourseStudent courseStudent2 = new CourseStudent();
            courseStudent2.StudentId = student1.Id;
            courseStudent2.CourseId = 2;
            courseStudent2.Course = course2;
            courseStudent2.Id = 2;

            CourseStudent courseStudent3 = new CourseStudent();
            courseStudent3.StudentId = student2.Id;
            courseStudent3.CourseId = 3;
            courseStudent3.Course = course3;
            courseStudent3.Id = 3;

            return new List<CourseStudent> { courseStudent1, courseStudent2, courseStudent3 };

        }

    }
}
