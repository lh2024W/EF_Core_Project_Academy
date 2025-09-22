using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class Lecture
    {
        public int Id { get; set; }
        public DateOnly LectureDate { get; set; } = new DateOnly();

        public int SubjectId { get; set; }

        public Subject Subject { get; set; } = new Subject();

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; } = new Teacher();

        public List<GroupLecture> GroupsLectures { get; set; } = new List<GroupLecture>();


    }

}
