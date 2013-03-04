using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Student.Domain;

namespace Student.Data.Mappings
{
    public class CourseStudentMap : ClassMap<CourseStudent>
    {
        public CourseStudentMap()
        {
            Id(c => c.Id);
            Map(c => c.StudentId);
            Map(c => c.CourseId);

            References(c => c.MainStudent, "StudentId").ReadOnly();
            References(c => c.Course, "CourseId").ReadOnly();
        }
    }
}
