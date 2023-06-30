using Education.Models;
using Education.Services;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Web.Helpers;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Education.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorService _InstructorService=null;
        private readonly IRoleService _RoleService;
     
        public InstructorController(IInstructorService instructorService, IRoleService RoleService)
        {
            //instructorGeneric = instructor;
            this._InstructorService = instructorService;
            _RoleService = RoleService;
           
        }
        public IActionResult Index()
        {
            IEnumerable<Instructor> instructors = _InstructorService.GetAll();
            return View(instructors);
        }

        IActionResult getRoleByInstructor(Instructor instructor)
        {
            Role roles = _RoleService.GetById(instructor.RoleId);
            return Json(roles);

        }

        public IActionResult New()
        {
            ViewData["RoleList"] = _RoleService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult New(Instructor _instructor, IFormFile image)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            image.CopyTo(memoryStream);
                            _instructor.image = memoryStream.ToArray();
                        }
                    }
                    _instructor.Password = BCrypt.Net.BCrypt.HashPassword(_instructor.Password);
                    _instructor.Isdeleted = false;


                    _InstructorService.Insert(_instructor);
                    _InstructorService.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewData["RoleList"] = _RoleService.GetAll();
            return View(_instructor);
        }
        [HttpPost]
        public ActionResult Save(Instructor _instructor, IFormFile imageFile)
        {
            if (ModelState.IsValid == true)
            {

                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        _instructor.image= memoryStream.ToArray();
                    }
                }
                //
                _instructor.Password = BCrypt.Net.BCrypt.HashPassword(_instructor.Password);
                _instructor.Isdeleted = false;


                _InstructorService.Insert(_instructor);

                _InstructorService.Save();
                return RedirectToAction("Index");
            }
            return View("New", _instructor);
        }
        public IActionResult Edit(int id)
        {
            Instructor instructor = _InstructorService.GetById(id);
            ViewData["RoleList"] = _RoleService.GetAll();
            ViewBag.EncryptedPassword = instructor.Password;
            return View(instructor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Instructor instructor, IFormFile? imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        instructor.image = memoryStream.ToArray();
                    }
                }
               
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("Password");
                if (ModelState.IsValid == true)//Server Side Check
                {
                   
                    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    int Id = int.Parse(userIdClaim.Value);
                    string password = _InstructorService.GetById(Id).Password;
                    instructor.Password = password;

                    //_InstructorService.Update(instructor);
                    _InstructorService.Save();
                    if (Id == instructor.Id)
                    {
                        //instructor.Id = Id;
                     
                        return RedirectToAction("Profile", "Account");
                    }

                   
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.InnerException.Message);
            }
            ViewData["RoleList"] = _RoleService.GetAll();
            return View(instructor);
        }
        public IActionResult Delete(int id)
        {
            Instructor instructor = _InstructorService.GetById(id);

            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = _InstructorService.GetById(id);

            if (instructor == null)
            {
                return NotFound();
            }

            _InstructorService.Delete(id);
            _InstructorService.Save();
            return RedirectToAction("Index");
        }
        /*[HttpPost]
        public string SaveFile(FileUpload fileObj)
        {
            Instructor instructor = JsonConvert.DeserializeObject<Instructor>(fileObj.instructor);
            if (fileObj.file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileObj.file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    instructor.image = fileBytes;

                    instructor = _InstructorService.Insert(instructor);
                    _InstructorService.Save();
                    if (instructor.Id > 0)
                    {
                        return "Saved";
                    }

                }
            }
            return "Failed";
        }

        [HttpGet]
        public JsonResult GetSavedInstructor()
        {
            var ins=_InstructorService.GetInstructorSaved();
            ins.image=this.GetImage(Convert.ToBase64String(ins.image));
            return Json(ins);

        }
        public byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if(!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }
            return bytes;
        }*/
    }

}

/*
if(BCrypt.Net.BCrypt.Verify(password,_insrtcutor.Password))
{
    //Do...
}*/