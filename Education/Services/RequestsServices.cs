using Education.Models;
using Education.Repositories;
using Education.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Education.Services
{
    public class RequestsServices : IRequestsServices
    {
        private readonly IGenericRepository<StudentRequests> _iRequestsRepository;

        public RequestsServices(IGenericRepository<StudentRequests> iRequestsRepository)
        {
            _iRequestsRepository = iRequestsRepository;
        }

        public IEnumerable<StudentRequests> GetAllRequestInfo()
        {
            return _iRequestsRepository.GetAll();
        }

        public void insert(StudentRequests studentRequest)
        {
            _iRequestsRepository.Insert(studentRequest);
            _iRequestsRepository.Save();
        }


        public void Delete(int id)
        {
            var currRequest = GetById(id);
            currRequest.IsDeleted = true;
            if (currRequest.OperationType == "edit")
            {
                currRequest.NewStudentData.IsDeleted = true;
            }
            else if (currRequest.OperationType == "add") 
            {
                currRequest.Student.IsDeleted = true;
            }
            _iRequestsRepository.Save();
        }

        public IEnumerable<StudentRequests> GetAll()
        {
            return _iRequestsRepository.GetAll().Include(r => r.Instructor)
                .Include(r => r.Student)
                .Include(r => r.NewStudentData).Where(r => r.IsDeleted == false);
        }

        public StudentRequests GetById(int id)
        {
            return GetAll().SingleOrDefault(r => r.Id == id);
        }

        public void Update(RequestsViewModel request, IFormFile imageFile)
        {
            var oldRequest = GetById(request.Id);

            if (request.OperationType == "edit")
            {
                oldRequest.NewStudentData.Name = request.StudentName;
                oldRequest.NewStudentData.Age = request.Age;
                oldRequest.NewStudentData.Address = request.Address;
                oldRequest.NewStudentData.Phone = request.Phone;
                if (imageFile!=null && imageFile.Length>0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        oldRequest.NewStudentData.image = memoryStream.ToArray();
                    }
                }

            }
            else if(request.OperationType == "add")
            {
                oldRequest.Student.Name = request.StudentName;
                oldRequest.Student.Age = request.Age;
                oldRequest.Student.Address = request.Address;
                oldRequest.Student.Phone = request.Phone;

                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        oldRequest.Student.image = memoryStream.ToArray();
                    }
                }

            }
            //_iRequestsRepository.Update(oldRequest);
            _iRequestsRepository.Save();
        }

        public void UpdateStatusToAceepted(StudentRequests request)
        {
            request.Status = "accepted";
            if (request.OperationType == "edit")
            {
                request.Student.Name = request.NewStudentData.Name;
                request.Student.Age = request.NewStudentData.Age;
                request.Student.Address = request.NewStudentData.Address;
                request.Student.Phone = request.NewStudentData.Phone;
                request.Student.image = request.NewStudentData.image;
            }
            else if (request.OperationType == "add")
            {
                request.Student.confirm = "accepted";
            }
            else if (request.OperationType == "delete") 
            {
                request.Student.IsDeleted = true;
            }
            _iRequestsRepository.Save();
        }

        public void UpdateStatusToRejected(StudentRequests request)
        {
            request.Status = "rejected";
            if (request.OperationType == "add")
            {
                request.Student.confirm = "rejected";
            }
            _iRequestsRepository.Save();
        }
    }
}
