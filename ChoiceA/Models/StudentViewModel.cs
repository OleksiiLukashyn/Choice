using System.Collections.Generic;

namespace ChoiceA.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<DisciplineViewItem> Disciplines { get; set; }
    }
}
