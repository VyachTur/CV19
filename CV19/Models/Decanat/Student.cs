using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Models.Decanat
{
	internal class Student
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronimic { get; set; }

        public DateTime Birthday { get; set; }

        public double Rating { get; set; }
    }
}
