using EF_Core_Project_Academy.AcademyDBContext;
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
    public class GroupRepository : IBaseRepository<Group>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(Group entity)
        {
            throw new NotImplementedException();
        }

        public Group GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var gr = context.Groups.FirstOrDefault(g => g.Id == id);
                return gr;
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var grId = context.Groups.Where(g => g.Name == name).Select(g => g.Id).FirstOrDefault();
                return grId;
            }
        }

        public int Insert(Group entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Group entity)
        {
            throw new NotImplementedException();
        }
    }
    
}
