namespace SKAutoNew.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        IQueryable<T> AllAsNoTracking();
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
        Task SaveAsync();
    }
}
