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
    public class GroupStudentRepository : IBaseRepository<GroupStudent>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(GroupStudent entity)
        {
            throw new NotImplementedException();
        }

        public GroupStudent GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Insert(GroupStudent entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupStudent> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(GroupStudent entity)
        {
            throw new NotImplementedException();
        }
    }
    
}
