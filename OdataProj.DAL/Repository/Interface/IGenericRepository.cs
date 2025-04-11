﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataProj.DAL.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(); 
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
