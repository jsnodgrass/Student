using FluentNHibernate.Mapping;
using Student.Domain;

namespace Student.Data.Mappings
{
    public class StudentMap : ClassMap<MainStudent>
    {
        public StudentMap()
        {
            Id(s => s.Id);
            Map(s => s.FirstName);
            Map(s => s.LastName);

            HasMany(s => s.CourseStudents).KeyColumn("StudentId");
        }
    }
}
