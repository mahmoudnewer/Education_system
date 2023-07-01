using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class GradeServices : IGradeServices
    {
        private readonly IGenericRepository<Grade> _iGradeRepository;

        public GradeServices(IGenericRepository<Grade> iGradeRepository )
        {
            _iGradeRepository = iGradeRepository;
        }

        public void Delete(int id)
        {
            var g = GetById(id);
            g.IsDeleted = true;
            _iGradeRepository.Save();
        }

        public IEnumerable<Grade> GetAll()
        {
            return _iGradeRepository.GetAll().Where(g=>g.IsDeleted==false).Include(g => g.student);
        }

        public Grade GetById(int id)
        {
           return _iGradeRepository.GetById(id);
        }

        public void Insert(Grade grade)
        {
            _iGradeRepository.Insert(grade);
            _iGradeRepository.Save();
        }

        public void Update(Grade g)
        {
            _iGradeRepository.Update(g);
            _iGradeRepository.Save();
        }
    }
}
