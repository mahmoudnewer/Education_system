using Education.Models;
using Education.Services;
using Education.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace Education.Controllers
{
    // /Requests/ViewAllRequests
    public class RequestsController : Controller
    {
        private readonly IRequestsServices _RequestService;
        public RequestsController(IRequestsServices RequestService)
        {
            _RequestService = RequestService;
        }

        [Authorize] 
        [HttpGet]
        public IActionResult ViewAllRequests()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            //var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            IEnumerable<StudentRequests> allRequests;
            if (User.IsInRole("Admin"))
            {
                allRequests = _RequestService.GetAll();
            }
            else
            {
                allRequests = _RequestService.GetAll().Where(r => r.InstructorId == userId);
            }

            List<RequestsViewModel> finalRes = new List<RequestsViewModel>();


            foreach (var item in allRequests)
            {
                RequestsViewModel req = new RequestsViewModel();

                req.Id = item.Id;

                req.StudentName = item.OperationType == "edit" ? item.NewStudentData.Name :
                item.Student.Name;

                req.Age = item.OperationType == "edit" ? item.NewStudentData.Age :
                item.Student.Age;

                req.Address = item.OperationType == "edit" ? item.NewStudentData.Address :
                item.Student.Address;

                req.Phone = item.OperationType == "edit" ? item.NewStudentData.Phone :
                item.Student.Phone;


                req.Image = item.OperationType == "edit" ? item.NewStudentData.image :
                item.Student.image;

                req.InstructorName = item.Instructor.Name;

                req.Status = item.Status;

                req.OperationType = item.OperationType;


                finalRes.Add(req);
            }

            return View(finalRes);
        }

        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public IActionResult EditRequest(int id)
        {
            var studentRequest = _RequestService.GetById(id);
            
            RequestsViewModel req = new RequestsViewModel();

            req.Id = studentRequest.Id;

            req.StudentName = studentRequest.OperationType == "edit" ? studentRequest.NewStudentData.Name :
            studentRequest.Student.Name;

            req.Age = studentRequest.OperationType == "edit" ? studentRequest.NewStudentData.Age :
            studentRequest.Student.Age;

            req.Address = studentRequest.OperationType == "edit" ? studentRequest.NewStudentData.Address :
            studentRequest.Student.Address;

            req.Phone = studentRequest.OperationType == "edit" ? studentRequest.NewStudentData.Phone :
            studentRequest.Student.Phone;


            req.Image = studentRequest.OperationType == "edit" ? studentRequest.NewStudentData.image :
            studentRequest.Student.image;
            req.InstructorName = studentRequest.Instructor.Name;

            req.Status = studentRequest.Status;

            req.OperationType = studentRequest.OperationType;

            return View(req);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Instructor")]
        public IActionResult EditRequest(RequestsViewModel request, IFormFile? imageFile)
        {

            var oldRequest = _RequestService.GetById(request.Id);
            request.Status = oldRequest.Status;
            request.OperationType = oldRequest.OperationType;
            request.InstructorName = oldRequest.Instructor.Name;
            if (ModelState.IsValid)
            {
               
                _RequestService.Update(request, imageFile);
                return RedirectToAction("ViewAllRequests");
            }
            return View(request);
        }


        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public IActionResult DeleteRefreshRequests(int id)
        {
            _RequestService.Delete(id);
            IEnumerable<StudentRequests>? allRequests = _RequestService.GetAll();
            List<RequestsViewModel> finalRes = new List<RequestsViewModel>();

            foreach (var item in allRequests)
            {
                RequestsViewModel req = new RequestsViewModel();

                req.Id = item.Id;

                req.StudentName = item.OperationType == "edit" ? item.NewStudentData.Name :
                item.Student.Name;

                req.Age = item.OperationType == "edit" ? item.NewStudentData.Age :
                item.Student.Age;

                req.Address = item.OperationType == "edit" ? item.NewStudentData.Address :
                item.Student.Address;

                req.Phone = item.OperationType == "edit" ? item.NewStudentData.Phone :
                item.Student.Phone;


                req.Image = item.OperationType == "edit" ? item.NewStudentData.image :
                item.Student.image;

                req.InstructorName = item.Instructor.Name;

                req.Status = item.Status;

                req.OperationType = item.OperationType;


                finalRes.Add(req);
            }
            return PartialView("_AllRequetsPartial", finalRes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AcceptRefreshRequests(int id)
        {
            var request = _RequestService.GetById(id);
            _RequestService.UpdateStatusToAceepted(request);
            IEnumerable<StudentRequests>? allRequests = _RequestService.GetAll();
            List<RequestsViewModel> finalRes = new List<RequestsViewModel>();
            foreach (var item in allRequests)
            {
                RequestsViewModel req = new RequestsViewModel();

                req.Id = item.Id;

                req.StudentName = item.OperationType == "edit" ? item.NewStudentData.Name :
                item.Student.Name;

                req.Age = item.OperationType == "edit" ? item.NewStudentData.Age :
                item.Student.Age;

                req.Address = item.OperationType == "edit" ? item.NewStudentData.Address :
                item.Student.Address;

                req.Phone = item.OperationType == "edit" ? item.NewStudentData.Phone :
                item.Student.Phone;


                req.Image = item.OperationType == "edit" ? item.NewStudentData.image :
                item.Student.image;

                req.InstructorName = item.Instructor.Name;

                req.Status = item.Status;

                req.OperationType = item.OperationType;


                finalRes.Add(req);
            }
            return PartialView("_AllRequetsPartial", finalRes);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RejectRefreshRequests(int id)
        {
            var request = _RequestService.GetById(id);
            _RequestService.UpdateStatusToRejected(request);
            IEnumerable<StudentRequests>? allRequests = _RequestService.GetAll();
            List<RequestsViewModel> finalRes = new List<RequestsViewModel>();
            foreach (var item in allRequests)
            {
                RequestsViewModel req = new RequestsViewModel();

                req.Id = item.Id;

                req.StudentName = item.OperationType == "edit" ? item.NewStudentData.Name :
                item.Student.Name;

                req.Age = item.OperationType == "edit" ? item.NewStudentData.Age :
                item.Student.Age;

                req.Address = item.OperationType == "edit" ? item.NewStudentData.Address :
                item.Student.Address;

                req.Phone = item.OperationType == "edit" ? item.NewStudentData.Phone :
                item.Student.Phone;


                req.Image = item.OperationType == "edit" ? item.NewStudentData.image :
                item.Student.image;

                req.InstructorName = item.Instructor.Name;

                req.Status = item.Status;

                req.OperationType = item.OperationType;


                finalRes.Add(req);
            }
            return PartialView("_AllRequetsPartial", finalRes);
        }




    }
}
