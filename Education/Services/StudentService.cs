using Education.Models;
using Education.Repositories;


namespace Education.Services
{
    public class StudentService : IStudentService
    {
        IGenericRepository<Student> _studentRepo;
        public StudentService(IGenericRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public void Delete(int studentId)
        {
           _studentRepo.Delete(studentId);
            _studentRepo.Save();
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepo.GetAll();
        }

        public Student GetById(int studentId)
        {

            return _studentRepo.GetById(studentId);
        }

        public Student GetByIdAsNoTracking(int studentId)
        {
            return _studentRepo.GetByIdAsNoTracking(studentId);
        }

        public void Insert(Student student)
        {
            _studentRepo.Insert(student);
            _studentRepo.Save();
        }

        public void Update(Student student)
        {
            _studentRepo.Update(student);
            _studentRepo.Save();
        }
    }
}
