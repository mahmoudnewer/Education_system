using AutoMapper;
using Education.Models;
using Education.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Security.Claims;

namespace Education.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly INewStudentService _NewStudentService;
        private readonly IRequestsServices _studentRequestService;
        private readonly IMapper _imapper;
        
       

        public StudentController(IStudentService studentService, INewStudentService NewStudentService, IRequestsServices studentRequestService,IMapper imapper)
        {
            _studentService = studentService;
            _NewStudentService= NewStudentService;
            _studentRequestService= studentRequestService;
            _imapper = imapper;


        }
        [Authorize]
        public IActionResult Index()
        {
            List<Student> students = _studentService.GetAll().Where(s=>s.confirm== "accepted" && s.IsDeleted==false).ToList();
            
            return View(students);
        }
        [Authorize]
        public IActionResult New()
        {
            ViewBag.EditButtonValue = "Create";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult New(Student student, IFormFile? ImageFile)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);

            ViewBag.EditButtonValue = "Create";

            ModelState.Remove("confirm");
            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ImageFile.CopyTo(memoryStream);
                            student.image = memoryStream.ToArray();
                        }
                    }

                 
                    if (User.IsInRole("Admin"))
                    {

                        student.confirm = "accepted";
                        _studentService.Insert(student);
                    }
                    else
                    {
                        student.confirm = "pending";
                        _studentService.Insert(student);
                        StudentRequests studentRequest = new StudentRequests()
                        {
                            StudentId = student.Id,
                            InstructorId = Id,
                            Status = "pending",
                            OperationType = "add"
                        };
                        _studentRequestService.insert(studentRequest);
                    }
                    return RedirectToAction("New");

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.Message);
                }
            }

            return View(student);

        }
        [Authorize]
        public IActionResult Remove(int id)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);
            if (User.IsInRole("Admin"))
            {
                _studentService.Delete(id);
            }else
            {
                Student student=_studentService.GetById(id);
                if(student!=null)
                {
                    StudentRequests studentRequest = new StudentRequests()
                    {
                        StudentId = student.Id,
                        
                        InstructorId = Id,
                        Status = "pending",
                        OperationType = "delete"
                    };
                  
                    StudentRequests ExisitingRequest = _studentRequestService.GetAllRequestInfo().FirstOrDefault(r => r.StudentId == student.Id && r.OperationType == "delete");
                    if(ExisitingRequest==null)
                    {
                        _studentRequestService.insert(studentRequest);
                    }
                   
                }
               
            }
          

            return RedirectToAction("index");

        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            ViewBag.EditButtonValue = "Edit";
            Student student = _studentService.GetById(id);

           
            return View("New",student);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public IActionResult Edit(Student student,IFormFile? ImageFile)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);
            ViewBag.EditButtonValue = "Edit";
            ModelState.Remove("confirm");
            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ImageFile.CopyTo(memoryStream);
                            student.image = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        
                        student.image = _studentService.GetByIdAsNoTracking(student.Id).image;

                    }

                    if (User.IsInRole("Admin"))
                    {
                    student.confirm = "accepted";

                        _studentService.Update(student);
                    }else
                    {
                        
                        StudentRequests studentRequest = new StudentRequests()
                        {
                            StudentId = student.Id,
                            InstructorId = Id,
                            Status = "pending",
                            OperationType = "edit"
                        };
                        _studentRequestService.insert(studentRequest);
                        var requestId=studentRequest.Id;
                      
                        var newStudentData=  _imapper.Map<NewStudentData>(student);
                         newStudentData.StudentReqId=requestId;

                        _NewStudentService.insert(newStudentData);
                      
                    }
                   
                    
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.Message);
                }
                return RedirectToAction("Index");
            }
            
            return View("New", student);

        }

    }
}
