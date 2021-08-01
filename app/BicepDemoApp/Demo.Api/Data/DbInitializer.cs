using Demo.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ExerciseAppContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Exercises.Any())
            {
                var exercises = new Exercise[] {
                    new Exercise { Name = "Bicep Curl", BodyPart= BodyPart.Arms, GearNeeded = "Dumbell, Barbell, or Ez Curl Bar" },
                    new Exercise { Name = "Bench Press", BodyPart= BodyPart.Chest, GearNeeded = "Barbell or Dumbell" },
                    new Exercise { Name = "Pushup", BodyPart= BodyPart.Chest, GearNeeded = "" },
                    new Exercise { Name = "Squat", BodyPart= BodyPart.Legs, GearNeeded = "Barbell" }
                };
                context.AddRange(exercises);
                context.SaveChanges();
            }
        }
    }
}
