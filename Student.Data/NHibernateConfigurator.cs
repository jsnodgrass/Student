using FluentNHibernate.Cfg.Db;
using NHibernate;
using FluentNHibernate.Cfg;
using Student.Domain;

namespace Student.Data
{
    public static class NHibernateConfigurator
    {
        public static ISession BuildSessionFactory<T>()
        {
            return GetConfiguration<T>().BuildSessionFactory().OpenSession();
        }

        private static FluentConfiguration GetConfiguration<T>()
        {
            return Fluently
                .Configure()
                .Database(MsSqlConfiguration
                              .MsSql2005
                              .ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MainStudent>());
        }
    }
}