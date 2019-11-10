using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreateExamApp.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string context { get; set; }

        public string question1 { get; set; }
        public string q1a { get; set; }
        public string q1b { get; set; }
        public string q1c { get; set; }
        public string q1d { get; set; }
        public string q1Option { get; set; }

        public string question2 { get; set; }
        public string q2a { get; set; }
        public string q2b { get; set; }
        public string q2c { get; set; }
        public string q2d { get; set; }
        public string q2Option { get; set; }

        public string question3 { get; set; }
        public string q3a { get; set; }
        public string q3b { get; set; }
        public string q3c { get; set; }
        public string q3d { get; set; }
        public string q3Option { get; set; }

        public string question4 { get; set; }
        public string q4a { get; set; }
        public string q4b { get; set; }
        public string q4c { get; set; }
        public string q4d { get; set; }
        public string q4Option { get; set; }
    }
}
