using NHibernate;
using Repositories;

namespace Students.Domain
{
    public class StudentRepository : NhibernateRepository
    {
        public StudentRepository(ISession session)
            : base(session)
        {
        }
    }
}