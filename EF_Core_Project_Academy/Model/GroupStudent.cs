using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class GroupStudent
    {
        public int Id { get; set; }

        public int GroupId { get; set; } 

        public Group Group { get; set; } = new Group();

        public int StudentId { get; set; }

        public Student Student { get; set; } = new Student();
    }
}
