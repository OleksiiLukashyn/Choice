using System.Collections.Generic;

namespace Choice.Models
{
    public class Student
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Group { set; get; }

        public List<StudDisc> StudDiscs { set; get; }
    }
}
