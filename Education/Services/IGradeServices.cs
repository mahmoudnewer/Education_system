using Education.Models;

namespace Education.Services
{
    public interface IGradeServices
    {
        IEnumerable<Grade> GetAll();
        void Insert(Grade grade);
        Grade GetById(int id);
        void Update(Grade g);
        void Delete(int id);

    }
}
