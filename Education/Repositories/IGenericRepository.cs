namespace Education.Repositories
{
        public interface IGenericRepository<T> where T : class
        {
            //Guid Id { get; set; }
            IQueryable<T> GetAll();
            T GetById(int id);
            T GetByIdAsNoTracking(int id);
            void Insert(T obj);
            void Update(T obj);
            void Delete(int id);
            void Save();
        }
    }
