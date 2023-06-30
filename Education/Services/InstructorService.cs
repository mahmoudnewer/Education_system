using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class InstructorService : IInstructorService
    {
        IGenericRepository<Instructor> _instructorRepository;
        private DbSet<Instructor> table = null;
        private DBContext _context = null;
        public InstructorService(IGenericRepository<Instructor> _instructorRepository)
        {
            this._instructorRepository = _instructorRepository;
        }
        public void Delete(int id)
        {
            _instructorRepository.Delete(id);
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

        public Instructor GetInstructorSaved()
        {
            return _context.instructors.SingleOrDefault();
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
        }
    }
}
