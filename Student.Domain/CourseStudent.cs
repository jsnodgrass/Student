using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Student.Domain
{
    public class CourseStudent
    {
        public virtual int Id { get; set; }
        public virtual Guid StudentId { get; set; }
        public virtual int CourseId { get; set; }

        public virtual Course Course { get; set; }

        [JsonIgnore]
        public virtual MainStudent MainStudent { get; set; }
    }
}
