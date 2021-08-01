using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Entities
{
    public class TagAssignment
    {
        public Guid TagAssignmentId { get;set;}
        public Guid TagId { get; set; }
        public Guid ExerciseId { get; set; }

        public Tag Tag { get; set; }
        public Exercise Exercise { get; set; }
    }
}
