using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class UserService : IUserService
    {
        private readonly IInstructorService _IInstructorService;
        private readonly IRoleService _IRoleService;

        public UserService(IInstructorService iInstructorService, IRoleService IRoleService)
        {
            _IInstructorService = iInstructorService;
            _IRoleService = IRoleService;
        }

        public bool Find(string email, string password)
        {
            Instructor user = _IInstructorService.GetAll().FirstOrDefault(a => a.Email == email && a.Password == password);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public Instructor Get(string email, string password)
        {
            return _IInstructorService.GetAll().FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        public string GetRole(int id)
        {
            return _IRoleService.GetById(id).Name;
            
        }


    }
}
