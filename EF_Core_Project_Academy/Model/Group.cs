using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class Group
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Year { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = new Department();

        public List<GroupLecture> GroupsLectures { get; set; } = new List<GroupLecture>();

        public List<GroupStudent> GroupsStudents { get; set; } = new List<GroupStudent>();

        public List<GroupCurator> GroupsCurators { get; set; } = new List<GroupCurator>();
    }
}
