using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class Faculty
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
