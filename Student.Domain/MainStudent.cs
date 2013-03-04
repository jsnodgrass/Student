using System;
using System.Collections.Generic;

namespace Student.Domain
{
    public class MainStudent
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }


        public virtual IEnumerable<CourseStudent> CourseStudents { get; set; }
    }
}
