using AutoMapper;
using Education.Models;
using Education.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Education.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly INewStudentService _NewStudentService;
        private readonly IStudentRequestService _studentRequestService;
        private readonly IMapper _imapper;
        
       

        public StudentController(IStudentService studentService, INewStudentService NewStudentService, IStudentRequestService studentRequestService,IMapper imapper)
        {
            _studentService = studentService;
            _NewStudentService= NewStudentService;
            _studentRequestService= studentRequestService;
            _imapper = imapper;


        }
        public IActionResult Index()
        {
            List<Student> students = _studentService.GetAll().Where(s=>s.confirm==true).ToList();
            
            return View(students);
        }
        public IActionResult New()
        {
            ViewBag.EditButtonValue = "Create";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult New(Student student)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);

            ViewBag.EditButtonValue = "Create";
           
          
            if (ModelState.IsValid)
            {
                try
                {
                    if (student.ImageFile != null && student.ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            student.ImageFile.CopyTo(memoryStream);
                            student.image = memoryStream.ToArray();
                        }
                    }

                 
                    if (User.IsInRole("Admin"))
                    {

                        student.confirm = true;
                        _studentService.Insert(student);
                    }
                    else
                    {
                        student.confirm = false;
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
                   
                    return View();
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.Message);
                }
            }

            return View(student);

        }
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
                  
                    StudentRequests ExisitingRequest = _studentRequestService.GetAll().FirstOrDefault(r => r.StudentId == student.Id && r.OperationType == "delete");
                    if(ExisitingRequest==null)
                    {
                        _studentRequestService.insert(studentRequest);
                    }
                   
                }
               
            }
          

            return RedirectToAction("index");

        }
       
        public IActionResult Edit(int id)
        {
            ViewBag.EditButtonValue = "Edit";
            Student student = _studentService.GetById(id);

           
            return View("New",student);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);
            bool admin = false;
            ViewBag.EditButtonValue = "Edit";
            if (ModelState.IsValid)
            {
                try
                {
                    if (student.ImageFile != null && student.ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            student.ImageFile.CopyTo(memoryStream);
                            student.image = memoryStream.ToArray();
                        }
                    }
              
                    student.confirm = true;
                    if (User.IsInRole("Admin"))
                    {
                       
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
