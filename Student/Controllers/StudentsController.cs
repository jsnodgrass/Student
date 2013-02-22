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
    public class StudentsController : ApiController
    {

        private IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            //_studentRepository = new StudentRepository(NHibernateConfigurator.BuildSessionFactory<MainStudent>());
        }
   

        // GET api/values
        public IQueryable<MainStudent> Get()
        {
            return _studentRepository.Get<MainStudent>();
        }

        // GET api/values/5
        public IQueryable Get(Guid id)
        {
            return _studentRepository.Get<MainStudent>().Where(m => m.Id == id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}