using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Model
{
    public partial class GroupCurator
    {
        public int Id { get; set; }

        public int CuratorId { get; set; }

        public Curator Curator { get; set; } = new Curator();

        public int GroupId { get; set; }

        public Group Group { get; set; } = new Group();
    }
}
