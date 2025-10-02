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
    public class GroupLectureRepository : IBaseRepository<GroupLecture>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(GroupLecture entity)
        {
            throw new NotImplementedException();
        }

        public GroupLecture GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Insert(GroupLecture entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupLecture> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(GroupLecture entity)
        {
            throw new NotImplementedException();
        }
    }
    
}
