using Education.Models;
using Education.ViewModel;

namespace Education.Services
{
    public interface IRequestsServices
    {
        IEnumerable<StudentRequests> GetAll();
        StudentRequests GetById(int id);
        void Update(RequestsViewModel request, IFormFile imageFile);
        void UpdateStatusToAceepted(StudentRequests request);
        void UpdateStatusToRejected(StudentRequests request);
        IEnumerable<StudentRequests> GetAllRequestInfo();
        void insert(StudentRequests studentRequest);

        void Delete(int id);


    }
}
