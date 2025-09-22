using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class Department
    {
        public int Id { get; set; }

        public int Building { get; set; }
        public decimal Financing { get; set; }

        public string Name { get; set; } = null!;

        public int FacultyId { get; set; }

        public Faculty Faculty { get; set; } = new Faculty();

        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
