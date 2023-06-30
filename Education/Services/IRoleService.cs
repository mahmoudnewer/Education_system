using Education.Models;

namespace Education.Services
{
    public interface IRoleService
    {
        Role GetById(int id);
        IEnumerable<Role> GetAll();
       
    }
}
