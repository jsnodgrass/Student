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
    public class CoursesControllerTest
    {
        private CoursesController _coursesController;
        private Mock<ICourseRepository> _courseRepository;
        private Course _newCourse;

        [SetUp]
        public void Init()
        {
            _newCourse = new Course();
            _newCourse.Name = "Health";
            _newCourse.Id = 4;

            _courseRepository = CourseRepositoryMocker.GetCourseRepositoryMock();

            _coursesController = new CoursesController(_courseRepository.Object);
            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            _coursesController.Request = request;
            _coursesController.Request.Properties["MS_HttpConfiguration"] = configuration;
        }

        [Test]
        public void Get()
        {
            IQueryable<Course> result = _coursesController.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("History", result.First().Name);
            Assert.AreEqual("English", result.ElementAt(1).Name);
            Assert.AreEqual("Science", result.Last().Name);
        }

        [Test]
        public void GetById()
        {
            // all these are probably not necessary, but doing it anyway
            IQueryable result1 = _coursesController.Get(1);
            IQueryable result2 = _coursesController.Get(2);
            IQueryable result3 = _coursesController.Get(3);

            Assert.AreEqual(1, result1.Cast<Course>().Count());
            Assert.AreEqual("History", result1.Cast<Course>().First().Name);
            Assert.AreEqual("English", result2.Cast<Course>().First().Name);
            Assert.AreEqual("Science", result3.Cast<Course>().First().Name);
        }

        [Test]
        public void Post()
        {
            HttpResponseMessage response = _coursesController.Post(_newCourse);
            IQueryable<Course> allCourses = _coursesController.Get();
            
            Course course = response.Content.ReadAsAsync<Course>().Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.AreEqual("Health", course.Name);
            Assert.AreEqual(4, course.Id);

            Assert.AreEqual(4, allCourses.Count());
            Assert.AreEqual("Health", allCourses.Last().Name);
        }

        [Test]
        public void Put()
        {
            IQueryable<Course> allCourses = _coursesController.Get();
            Course courseToEdit = allCourses.First();
            courseToEdit.Name = "Physics";
            HttpResponseMessage response = _coursesController.Put(courseToEdit.Id, courseToEdit);
            IQueryable<Course> allCoursesNew = _coursesController.Get();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual("Physics", response.Content.ReadAsAsync<Course>().Result.Name);
            Assert.AreEqual("Physics", allCoursesNew.First().Name);
        }

        [Test]
        public void Delete()
        {
            IQueryable<Course> allCourses = _coursesController.Get();
            Course courseToDelete = allCourses.First();
            HttpResponseMessage response = _coursesController.Delete(courseToDelete.Id);
            IQueryable<Course> allCoursesNew = _coursesController.Get();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(2, allCoursesNew.Count());
        }
    }
}
