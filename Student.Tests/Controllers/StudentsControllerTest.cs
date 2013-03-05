using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Student;
using Student.Controllers;
using Student.Data;
using Student.Domain;
using Student.Tests.Mockers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Student.Tests.Controllers
{
    public class StudentsControllerTest
    {
        private StudentsController _studentsController;
        Mock<IStudentRepository> _studentRepository;
        private MainStudent _newStudent;

        [SetUp]
        public void Init()
        {
            _newStudent = new MainStudent();
            _newStudent.FirstName = "Peyton";
            _newStudent.LastName = "Manning";

            _studentRepository = StudentRepositoryMocker.GetStudentRepositoryMock();

            _studentsController = new StudentsController(_studentRepository.Object);
            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            _studentsController.Request = request;
            _studentsController.Request.Properties["MS_HttpConfiguration"] = configuration;
        }

        [Test]
        public void Get()
        {
            // Act
            IQueryable<MainStudent> result = _studentsController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Jason", result.ElementAt(0).FirstName);
            Assert.AreEqual("Snodgrass", result.ElementAt(0).LastName);
            Assert.AreEqual("History", result.ElementAt(0).CourseStudents.ElementAt(0).Course.Name);
            Assert.AreEqual("English", result.ElementAt(0).CourseStudents.ElementAt(1).Course.Name);
            Assert.AreEqual("Science", result.ElementAt(0).CourseStudents.ElementAt(2).Course.Name);

            Assert.AreEqual("John", result.ElementAt(1).FirstName);
            Assert.AreEqual("Snow", result.ElementAt(1).LastName);
            Assert.AreEqual("History", result.ElementAt(0).CourseStudents.ElementAt(0).Course.Name);
            Assert.AreEqual("English", result.ElementAt(0).CourseStudents.ElementAt(1).Course.Name);
            Assert.AreEqual("Science", result.ElementAt(0).CourseStudents.ElementAt(2).Course.Name);

        }

        [Test]
        public void GetById()
        {
            // Act
            IQueryable<MainStudent> allStudents = _studentsController.Get();

            IQueryable result1 = _studentsController.Get(allStudents.ElementAt(0).Id);
            var student1 = result1.Cast<MainStudent>();

            IQueryable result2 = _studentsController.Get(allStudents.ElementAt(1).Id);
            var student2 = result2.Cast<MainStudent>();
            
            // Assert
            Assert.IsNotNull(student1);
            Assert.AreEqual("Jason", student1.ElementAt(0).FirstName);
            Assert.AreEqual("Snodgrass", student1.ElementAt(0).LastName);

            Assert.IsNotNull(student2);
            Assert.AreEqual("John", student2.ElementAt(0).FirstName);
            Assert.AreEqual("Snow", student2.ElementAt(0).LastName);
        }

        [Test]
        public void Post()
        {
            // Act
            HttpResponseMessage response = _studentsController.Post(_newStudent);
            IQueryable<MainStudent> allStudents = _studentsController.Get();

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            MainStudent student = response.Content.ReadAsAsync<MainStudent>().Result;
            Assert.IsNotNull(student);
            Assert.AreEqual("Peyton", student.FirstName);
            Assert.AreEqual("Manning", student.LastName);
            Assert.AreEqual(3, allStudents.Count());
        }

        [Test]
        public void Put()
        {
            // Act
            IQueryable<MainStudent> allStudents = _studentsController.Get();
            var studentToEdit = allStudents.ElementAt(0);
            studentToEdit.LastName = "Snoddy";

            HttpResponseMessage response = _studentsController.Put(studentToEdit.Id, studentToEdit);

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            MainStudent student = response.Content.ReadAsAsync<MainStudent>().Result;
            Assert.IsNotNull(student);
            Assert.AreEqual("Jason", student.FirstName);
            Assert.AreEqual("Snoddy", student.LastName);
        }

        [Test]
        public void Delete()
        {
            // Act
            IQueryable<MainStudent> allStudents = _studentsController.Get();
            var studentToDelete = allStudents.ElementAt(0);

            HttpResponseMessage response = _studentsController.Delete(studentToDelete.Id);

            IQueryable<MainStudent> allStudents2 = _studentsController.Get();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(1, allStudents2.Count());
        }
    }
}
