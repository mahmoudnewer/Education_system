using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Education.Services
{
    public class StudentRequestService : IStudentRequestService
    {
        IGenericRepository<StudentRequests> _studentRequestRepo;
        public StudentRequestService(IGenericRepository<StudentRequests> studentRequestRepo)
        {
            _studentRequestRepo = studentRequestRepo;
        }

        public IEnumerable<StudentRequests> GetAll()
        {
           return _studentRequestRepo.GetAll();
        }

       

        public void insert(StudentRequests studentRequest)
        {
            _studentRequestRepo.Insert(studentRequest);
            _studentRequestRepo.Save();
        }
    }
}
