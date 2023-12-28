using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string ExerciseName { get; set; }
        public string ExplanationLink { get; set; } 
        public string Notes { get; set; }

        public Exercise(int exerciseID, string exerciseName, string explanationLink, string notes)
        {
            ExerciseID = exerciseID;
            ExerciseName = exerciseName;
            ExplanationLink = explanationLink;
            Notes = notes;
        }

        public override string ToString()
        {
            return ExerciseName;
        }
    }
}
