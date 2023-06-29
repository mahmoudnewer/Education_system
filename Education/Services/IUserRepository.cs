using Education.Models;

namespace Education.Services
{
    public interface IUserRepository
    {
        bool Find(string email, string password);
        Instructor Get(string email, string password);
        string GetRole(int id);
    }
}