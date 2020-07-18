﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Choice.Models
{
    public class Student
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Incorrect length of {0}.")]
        [RegularExpression(@"[A-Z][a-z]+[ ][A-Z]+[a-z]+")]
        [Remote("ValidateName", "Students")]
        public string Name { set; get; }
        [Required(ErrorMessage = "{0} is required.")]
        public string Group { set; get; }

        public List<StudDisc> StudDiscs { set; get; }
    }
}
