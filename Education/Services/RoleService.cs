using Education.Models;
using Education.Repositories;

namespace Education.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _RoleRepo;
        public RoleService(IGenericRepository<Role> RoleRepo)
        {
            _RoleRepo = RoleRepo;
        }

        public IEnumerable<Role> GetAll()
        {
           return _RoleRepo.GetAll();   
        }

        public Role GetById(int id)
        {
            return _RoleRepo.GetById(id);
        }

      
        
    }
}
