using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Student.Data;
using Student.Domain;

namespace Student.Tests.Mockers
{
    public static class RepositoryMocker
    {
        public static IStudentRepository MockUpRepository()
        {
            return GetStudentRepositoryMock().Object;
        }

        public static Mock<IStudentRepository> GetStudentRepositoryMock()
        {
            var mockStudentRepository = new Mock<IStudentRepository>();

            List<MainStudent> students = CreateStudents();
            mockStudentRepository.Setup(r => r.Get<MainStudent>()).Returns(students.AsQueryable());

            return mockStudentRepository;
        }

        private static List<MainStudent> CreateStudents()
        {
            MainStudent student1 = new MainStudent();
            student1.Id = Guid.NewGuid();
            student1.FirstName = "Jason";
            student1.LastName = "Snodgrass";
            student1.CourseStudents = CreateCourseStudents(student1.Id);

            MainStudent student2 = new MainStudent();
            student2.Id = Guid.NewGuid();
            student2.FirstName = "John";
            student2.LastName = "Snow";
            student2.CourseStudents = CreateCourseStudents(student2.Id);

            return new List<MainStudent> { student1, student2 };
        }

        private static List<CourseStudent> CreateCourseStudents(Guid studentId)
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


            CourseStudent courseStudent1 = new CourseStudent();
            courseStudent1.StudentId = studentId;
            courseStudent1.CourseId = 1;
            courseStudent1.Course = course1;

            CourseStudent courseStudent2 = new CourseStudent();
            courseStudent2.StudentId = studentId;
            courseStudent2.CourseId = 2;
            courseStudent2.Course = course2;

            CourseStudent courseStudent3 = new CourseStudent();
            courseStudent3.StudentId = studentId;
            courseStudent3.CourseId = 3;
            courseStudent3.Course = course3;

            return new List<CourseStudent> { courseStudent1, courseStudent2, courseStudent3 };

        }

    }
}
