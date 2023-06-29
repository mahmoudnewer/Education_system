using AutoMapper;
using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class InstructorService: IInstructorService
    {
        private readonly IGenericRepository<Instructor> _iInstructorRepository;

        public InstructorService(IGenericRepository<Instructor> iInstructorRepository)
        {
            _iInstructorRepository = iInstructorRepository;
        }


        public IEnumerable<Instructor> GetAll()
        {
            return _iInstructorRepository.GetAll().Include(x=>x.role);
        }

        public Instructor GetById(int id)
        {
            return _iInstructorRepository.GetById(id);
        }

     
        public Instructor GetByIdAsNoTracking(int id)
        {
            return _iInstructorRepository.GetByIdAsNoTracking(id);
        }

    }
}
