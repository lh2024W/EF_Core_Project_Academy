using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Rating { get; set; }

        public string Surname { get; set; } = null!;

        public List<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();
        
    }
}
