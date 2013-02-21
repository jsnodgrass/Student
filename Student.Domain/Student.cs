using System;
using System.Collections.Generic;

namespace Student.Domain
{
    public class MainStudent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> Courses { get; set; }
    }
}
