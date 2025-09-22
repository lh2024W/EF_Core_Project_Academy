using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class GroupLecture
    {
        public int GroupId { get; set; }

        public Group Group { get; set; } = new Group();

        public int LectureId { get; set; }

        public Lecture Lecture { get; set; } = new Lecture();
    }
}
