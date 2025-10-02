using EF_Core_Project_Academy.Interfaces;
using EF_Core_Project_Academy.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Repository
{
    public class GroupCuratorRepository : IBaseRepository<GroupCurator>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(GroupCurator entity)
        {
            throw new NotImplementedException();
        }

        public GroupCurator GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Insert(GroupCurator entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupCurator> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(GroupCurator entity)
        {
            throw new NotImplementedException();
        }
    }
    
}
