using AppEventos.Persistence.Context;
using AppEventos.Persistence.IRepositories;
using System.Threading.Tasks;

namespace AppEventos.Persistence.Repositories
{
    public class BasePersistence : IBasePersistence
    {

        private readonly AppEventosContext _context;
        
        public BasePersistence(AppEventosContext context)
        {
            _context = context;
        }

        public void Save<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }       
    }
}
