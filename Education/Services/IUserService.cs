using Education.Models;

namespace Education.Services
{
    public interface IUserService
    {
        bool Find(string email, string password);
        Instructor Get(string email, string password);
        string GetRole(int id);
    }
}