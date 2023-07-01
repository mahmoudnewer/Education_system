using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class InstructorService : IInstructorService
    {
        IGenericRepository<Instructor> _instructorRepository;
        public InstructorService(IGenericRepository<Instructor> instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
        public void Delete(int id)
        {
            _instructorRepository.Delete(id);
            _instructorRepository.Save();
        }

        public IEnumerable<Instructor> GetAll()
        {
            return _instructorRepository.GetAll();
        }

        public Instructor GetById(int id)
        {
            return _instructorRepository.GetById(id);
        }

        public Instructor GetByIdAsNoTracking(int id)
        {
            return _instructorRepository.GetByIdAsNoTracking(id);
        }

        public Instructor Insert(Instructor instructor)
        {
            _instructorRepository.Insert(instructor);
            return instructor;
        }

        public void Save()
        {
             _instructorRepository.Save();
        }

        public void Update(Instructor instructor)
        {
            _instructorRepository.Update(instructor);
            _instructorRepository.Save();
        }

        public void UpdatePassword(int instructorId,string NewPassword)
        {
            var instructor=_instructorRepository.GetById(instructorId);
            instructor.Password = NewPassword;
            _instructorRepository.Update(instructor);
            _instructorRepository.Save();
        }

        public bool IsFound(string email, string password)
        {
            Instructor user = _instructorRepository.GetAll().FirstOrDefault(a => a.Email == email && a.Password == password);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public Instructor GetByEmaillAndPassword(string email, string password)
        {
            return _instructorRepository.GetAll().FirstOrDefault(a => a.Email == email && a.Password == password);
        }
    }
}
