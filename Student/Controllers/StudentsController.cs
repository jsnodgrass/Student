using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student.Data;
using Student.Domain;

namespace Student.Controllers
{
    public class StudentsController : ApiController
    {

        private IStudentRepository _studentRepository;


        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
   

        // GET api/values
        public IQueryable<MainStudent> Get()
        {
            var student = _studentRepository.Get<MainStudent>();


            return student;
        }

        // GET api/values/5
        public IQueryable Get(Guid id)
        {
            return _studentRepository.Get<MainStudent>().Where(m => m.Id == id);
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]MainStudent newStudent)
        {
            newStudent.Id = Guid.NewGuid();
            _studentRepository.Create<MainStudent>(newStudent);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/values/5
        public HttpResponseMessage Put(Guid id, [FromBody]MainStudent student)
        {
            MainStudent studentToEdit = null;
            studentToEdit = _studentRepository.Get<MainStudent>().Where(m => m.Id == id).SingleOrDefault();

            if (studentToEdit == null || studentToEdit.Id != id) return Request.CreateResponse(HttpStatusCode.NotFound);

            studentToEdit.FirstName = student.FirstName;
            studentToEdit.LastName = student.LastName;

            _studentRepository.Update<MainStudent>(studentToEdit);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(Guid id)
        {
            MainStudent studentToDelete = null;
            studentToDelete = _studentRepository.Get<MainStudent>().Where(m => m.Id == id).SingleOrDefault();
            if (studentToDelete == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            _studentRepository.Delete<MainStudent>(studentToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}