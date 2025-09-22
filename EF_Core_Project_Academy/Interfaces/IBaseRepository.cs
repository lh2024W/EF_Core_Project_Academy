﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Interfaces
{
    public interface IBaseRepository<T>
    {
        public int Insert(T entity);
        public bool Delete(T entity);
        public int Update(T entity);
        public IEnumerable<T> Select();
        public T GetById(int id);
        public int GetIdByName (string name);
        
    }
}
