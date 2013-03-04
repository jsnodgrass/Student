using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Repositories;
using Repositories.Interfaces;

namespace Student.Data
{
    public interface ICourseRepository : IRepository
    {
 
    }
    public class CourseRepository : NhibernateRepository, ICourseRepository
    {
        public CourseRepository(ISession session) : base(session)
        {
        }
    }
}
