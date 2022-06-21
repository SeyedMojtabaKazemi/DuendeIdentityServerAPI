using Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IdentityServerDbContext _context;
        private readonly DbSet<T> Entity;

        public Repository(IdentityServerDbContext context)
        {
            _context = context;
            Entity = _context.Set<T>();
        }

        public void Add(T entity)
        {
            Entity.Add(entity);
        }

        public void Delete(T entity)
        {
            Entity.Remove(entity);
        }
    }
}
