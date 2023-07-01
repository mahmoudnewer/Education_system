using Education.Models;

namespace Education.Services
{
    public interface IInstructorService
    {
        IEnumerable<Instructor> GetAll();
        Instructor GetById(int id);
        Instructor GetByIdAsNoTracking(int id);
        Instructor Insert(Instructor instructor);
        void Update(Instructor instructor);
        void Delete(int id);
        void Save();
        void UpdatePassword(int instructorId,string NewPassword);
         bool IsFound(string email, string password);
        Instructor GetByEmaillAndPassword(string email, string password);
    
    }
}
