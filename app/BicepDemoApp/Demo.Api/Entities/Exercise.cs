using System;
using System.Collections.Generic;

namespace Demo.Api.Entities
{
    public class Exercise
    {
        public Guid ExerciseId { get; set; }
        public string Name { get; set; }
        public BodyPart? BodyPart { get; set; }
        public string GearNeeded { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
