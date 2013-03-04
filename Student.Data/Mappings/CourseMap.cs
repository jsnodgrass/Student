using FluentNHibernate.Mapping;
using Student.Domain;

namespace Student.Data.Mappings
{
    public class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Id(c => c.Id);
            Map(c => c.Name);

            References(c => c.CourseStudent, "Id").ReadOnly();
        }
    }
}
