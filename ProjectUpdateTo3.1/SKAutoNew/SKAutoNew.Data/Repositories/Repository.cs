namespace SKAutoNew.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class Repository<T> : IRepository<T> where T : class
    {
        private SKAutoDbContext context = null;
        private DbSet<T> table = null;
        public Repository(SKAutoDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public IQueryable<T> All()
        {
            var needed = table;
            return needed;
        }
        public IQueryable<T> AllAsNoTracking()
        {
            return this.table.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var needed = await table.FindAsync(id);
            return needed;
        }
        public async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(T obj)
        {
            if(obj == null) {
                throw new ArgumentNullException("entity");
            }
            table.Remove(obj);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
