using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Instructor> _iInstructorRepository;

        public UserService(IGenericRepository<Instructor> iInstructorRepository)
        {
            _iInstructorRepository = iInstructorRepository;
        }

        public bool Find(string email, string password)
        {
            Instructor user = _iInstructorRepository.GetAll().FirstOrDefault(a => a.Email == email && a.Password == password);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public Instructor Get(string email, string password)
        {
            return _iInstructorRepository.GetAll().FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        public string GetRole(int id)
        {
            Instructor user = _iInstructorRepository.GetAll().FirstOrDefault(a => a.Id == id);
            return user.role.Name;
        }


    }
}
