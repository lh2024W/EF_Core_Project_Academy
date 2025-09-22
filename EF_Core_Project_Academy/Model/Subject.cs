using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
