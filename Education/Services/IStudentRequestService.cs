using Education.Models;

namespace Education.Services
{
    public interface IStudentRequestService
    {
        void insert(StudentRequests studentRequest);
        IEnumerable<StudentRequests> GetAll();
    }
}
