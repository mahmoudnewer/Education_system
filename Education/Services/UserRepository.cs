using Education.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class UserRepository : IUserRepository
    {
        DBContext DB;
        public UserRepository(DBContext _DB_Context)
        {
            DB = _DB_Context;
        }

        public bool Find(string email, string password)
        {
            Instructor user = DB.instructors.FirstOrDefault(a => a.Email == email && a.Password == password);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public Instructor Get(string email, string password)
        {
            return DB.instructors.FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        public string GetRole(int id)
        {
            Instructor user = DB.instructors.Include(i => i.role).FirstOrDefault(a => a.Id == id);
            return user.role.Name;
        }


    }
}
