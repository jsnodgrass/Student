using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Moq;
using NUnit.Framework;
using Student.Controllers;
using Student.Data;
using Student.Domain;
using Student.Tests.Mockers;

namespace Student.Tests.Controllers
{
    public class CourseStudentControllerTest
    {
        private CourseStudentController _courseStudentController;
        private StudentsController _studentsController;
        Mock<IStudentRepository> _studentRepository;
        private Mock<ICourseStudentRepository> _courseStudentRepository;
        private CourseStudent _newCourseStudent;


        [SetUp]
        public void Init()
        {
            _studentRepository = StudentRepositoryMocker.GetStudentRepositoryMock();
            _studentsController = new StudentsController(_studentRepository.Object);
            IQueryable<MainStudent> students = _studentsController.Get();
            MainStudent student = students.First();

            _newCourseStudent = new CourseStudent();
            _newCourseStudent.StudentId = student.Id;
            _newCourseStudent.CourseId = 10;
            _newCourseStudent.Id = 4;

            _courseStudentRepository = CourseStudentRepositoryMocker.GetCourseStudentRepositoryMock();
            _courseStudentController = new CourseStudentController(_courseStudentRepository.Object);

            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            _courseStudentController.Request = request;
            _courseStudentController.Request.Properties["MS_HttpConfiguration"] = configuration;
        }

        [Test]
        public void Get()
        {
            IQueryable<CourseStudent> courseStudents = _courseStudentController.Get();

            Assert.AreEqual(3, courseStudents.Count());
            Assert.AreEqual("History", courseStudents.First().Course.Name);
            Assert.AreEqual("English", courseStudents.ElementAt(1).Course.Name);
            Assert.AreEqual("Science", courseStudents.Last().Course.Name);
        }

        [Test]
        public void Post()
        {
            HttpResponseMessage response = _courseStudentController.Post(_newCourseStudent);
            IQueryable<CourseStudent> allCourseStudents = _courseStudentController.Get();

            Assert.AreEqual(4, allCourseStudents.Count());
            Assert.AreEqual(10, allCourseStudents.Last().CourseId);

            CourseStudent courseStudent = response.Content.ReadAsAsync<CourseStudent>().Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(10, courseStudent.CourseId);
        }

        [Test]
        public void Delete()
        {
            IQueryable<CourseStudent> allCourseStudents = _courseStudentController.Get();
            var courseStudentToDelete = allCourseStudents.First();
            HttpResponseMessage response = _courseStudentController.Delete(courseStudentToDelete.Id);

            IQueryable<CourseStudent> allCourseStudents2 = _courseStudentController.Get();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(2, allCourseStudents2.Count());
        }
    }
}
