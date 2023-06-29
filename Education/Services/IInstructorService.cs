using Education.Models;

namespace Education.Services
{
    public interface IInstructorService
    {
        IEnumerable<Instructor> GetAll();
        Instructor GetById(int id);
        
    }
}