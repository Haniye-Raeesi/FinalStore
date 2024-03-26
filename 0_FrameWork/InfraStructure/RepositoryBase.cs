using _0_FrameWork.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace _0_FrameWork.InfraStructure
{
    public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T:class
    {
        private readonly DbContext _Context;

        public RepositoryBase(DbContext context)
        {
            _Context = context;
        }

        public void Create(T entity)
        {
            _Context.Add(entity);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _Context.Set<T>().Any(expression);
        }


        public T Get(TKey id)
        {
            return _Context.Find<T>(id);
        }

        public List<T> Get()
        {
            return _Context.Set<T>().ToList();
        }

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
