using Education.Models;

namespace Education.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        void Insert(Student student);
        void Delete(int studentId);
        Student GetById(int studentId); 
        void Update(Student student);   
    }
}
