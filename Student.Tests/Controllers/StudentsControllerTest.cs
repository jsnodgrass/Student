using System;
using System.Collections.Generic;
using System.Linq;
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
        Mock<IStudentRepository> _studentRepository;

        [SetUp]
        public void Init()
        {
            _studentRepository = RepositoryMocker.GetStudentRepositoryMock();
        }

        [Test]
        public void Get()
        {
            // Arrange
            StudentsController controller = new StudentsController(_studentRepository.Object);

            // Act
            IQueryable<MainStudent> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Jason", result.ElementAt(0).FirstName);
            Assert.AreEqual("Snodgrass", result.ElementAt(0).LastName);
            Assert.AreEqual("History 101", result.ElementAt(0).CourseStudents.ElementAt(0).Name);
            Assert.AreEqual("History 102", result.ElementAt(0).CourseStudents.ElementAt(1).Name);
            Assert.AreEqual("History 103", result.ElementAt(0).CourseStudents.ElementAt(2).Name);

            Assert.AreEqual("John", result.ElementAt(1).FirstName);
            Assert.AreEqual("Snow", result.ElementAt(1).LastName);
            Assert.AreEqual("History 101", result.ElementAt(1).CourseStudents.ElementAt(0).Name);
            Assert.AreEqual("History 102", result.ElementAt(1).CourseStudents.ElementAt(1).Name);
            Assert.AreEqual("History 103", result.ElementAt(1).CourseStudents.ElementAt(2).Name);

        }

        //[Test]
        //public void GetById()
        //{
        //    // Arrange
        //    StudentsController controller = new StudentsController();

        //    // Act
        //    //string result = controller.Get(5);

        //    // Assert
        //    //Assert.AreEqual("value", result);
        //}

        //[Test]
        //public void Post()
        //{
        //    // Arrange
        //    StudentsController controller = new StudentsController();

        //    // Act
        //    //controller.Post("value");

        //    // Assert
        //}

        //[Test]
        //public void Put()
        //{
        //    // Arrange
        //    StudentsController controller = new StudentsController();

        //    // Act
        //    //controller.Put(5, "value");

        //    // Assert
        //}

        //[Test]
        //public void Delete()
        //{
        //    // Arrange
        //    StudentsController controller = new StudentsController();

        //    // Act
        //    //controller.Delete(5);

        //    // Assert
        //}
    }
}
