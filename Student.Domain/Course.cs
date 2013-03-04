using System;
using Newtonsoft.Json;

namespace Student.Domain
{
    public class Course
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual CourseStudent CourseStudent { get; set; }

    }
}
