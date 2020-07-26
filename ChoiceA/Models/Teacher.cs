using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChoiceA.Models
{
    public class Teacher
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"[A-Z][a-z]+[ ][A-Z]+[a-z]+")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Incorrect length of {0}.")]
        [Remote("ValidateName", "Teachers")]
        public string Name { set; get; }

        public List<Discipline> Disciplines { set; get; }
    }
}
