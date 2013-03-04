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
            List<Course> courses = CreateCourses();

            MainStudent student1 = new MainStudent();
            student1.Id = new Guid();
            student1.FirstName = "Jason";
            student1.LastName = "Snodgrass";
            student1.CourseStudents = courses;

            MainStudent student2 = new MainStudent();
            student2.Id = new Guid();
            student2.FirstName = "John";
            student2.LastName = "Snow";
            student2.CourseStudents = courses;

            return new List<MainStudent> { student1, student2 };
        }

        private static List<Course> CreateCourses()
        {

            Course course1 = new Course();
            course1.Id = 0;
            course1.Name = "History 101";

            Course course2 = new Course();
            course2.Id = 1;
            course2.Name = "History 102";

            Course course3 = new Course();
            course3.Id = 2;
            course3.Name = "History 103";

            return new List<Course> { course1, course2, course3 };
        }



    }
}
