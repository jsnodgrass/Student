using NHibernate;
using Repositories;
using Repositories.Interfaces;

namespace Student.Data
{
    public interface ICourseStudentRepository : IRepository
    {
    }
    public class CourseStudentRepository : NhibernateRepository, ICourseStudentRepository
    {
        public CourseStudentRepository(ISession session) : base(session)
        {
        }
    }
}
