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
    public class CourseStudentController : ApiController
    {

        private ICourseStudentRepository _courseStudentRepository;

        public CourseStudentController(ICourseStudentRepository courseStudentRepository)
        {
            _courseStudentRepository = courseStudentRepository;
        }

        // GET api/coursestudent
        public IQueryable<CourseStudent> Get()
        {
            return _courseStudentRepository.Get<CourseStudent>();
        }

        // GET api/coursestudent/5
        public IQueryable Get(int id)
        {
            return _courseStudentRepository.Get<CourseStudent>().Where(c => c.Id == id);
        }

        // POST api/coursestudent
        public HttpResponseMessage Post([FromBody]CourseStudent newCourseStudent)
        {
            _courseStudentRepository.Create<CourseStudent>(newCourseStudent);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/coursestudent/5
        public HttpResponseMessage Put(int id, [FromBody]CourseStudent values)
        {

            // i am putting this in here basically for practice. in this context 
            // i believe we will only use create and delete, never update
            CourseStudent courseStudentToEdit = null;
            courseStudentToEdit = _courseStudentRepository.Get<CourseStudent>().Where(c => c.Id == id).SingleOrDefault();

            if (courseStudentToEdit == null || courseStudentToEdit.Id != values.Id)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            courseStudentToEdit.StudentId = values.StudentId;
            courseStudentToEdit.CourseId = values.CourseId;
            _courseStudentRepository.Update<CourseStudent>(courseStudentToEdit);

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // DELETE api/coursestudent/5
        public HttpResponseMessage Delete(int id)
        {
            CourseStudent courseStudentToDelete = null;
            courseStudentToDelete = _courseStudentRepository.Get<CourseStudent>().Where(c => c.Id == id).SingleOrDefault();

            if (courseStudentToDelete == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            _courseStudentRepository.Delete<CourseStudent>(courseStudentToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
