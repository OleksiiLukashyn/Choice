using System.Collections.Generic;

namespace Choice.Models
{
    public class Teacher
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public List<Discipline> Disciplines { set; get; }
    }
}
