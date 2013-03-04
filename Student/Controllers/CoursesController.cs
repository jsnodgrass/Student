using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student.Data;
using Student.Domain;

namespace Student.Controllers
{
    public class CoursesController : ApiController
    {

        private ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET api/course
        public IQueryable<Course> Get()
        {
            return _courseRepository.Get<Course>();
        }

        // GET api/course/5
        public IQueryable Get(int id)
        {
            return _courseRepository.Get<Course>().Where(c => c.Id == id);
        }

        // POST api/course
        public HttpResponseMessage Post([FromBody]Course newCourse)
        {
            _courseRepository.Create<Course>(newCourse);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/course/5
        public HttpResponseMessage Put(int id, [FromBody]Course course)
        {
            Course courseToEdit = null;
            courseToEdit = _courseRepository.Get<Course>().Where(c => c.Id == id).SingleOrDefault();
            if (courseToEdit == null || courseToEdit.Id != id)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            courseToEdit.Name = course.Name;
            _courseRepository.Create<Course>(courseToEdit);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // DELETE api/course/5
        public HttpResponseMessage Delete(int id)
        {
            Course courseToDelete = null;
            courseToDelete = _courseRepository.Get<Course>().Where(c => c.Id == id).SingleOrDefault();
            if (courseToDelete == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            _courseRepository.Delete<Course>(courseToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}
