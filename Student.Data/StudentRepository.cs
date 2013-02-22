using System.Linq;
using NHibernate;
using Repositories;
using Repositories.Interfaces;

namespace Student.Data
{
    public interface IStudentRepository : IRepository
    {
    }

    public class StudentRepository : NhibernateRepository, IStudentRepository
    {
        public StudentRepository(ISession session) : base(session)
        {
        }
    }
}