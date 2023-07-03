using Education.Models;
using Education.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Web.Helpers;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;

namespace Education.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorService _InstructorService = null;
        private readonly IRoleService _RoleService;

        public InstructorController(IInstructorService instructorService, IRoleService RoleService)
        {
            //instructorGeneric = instructor;
            this._InstructorService = instructorService;
            _RoleService = RoleService;

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);
            IEnumerable<Instructor> instructors = _InstructorService.GetAll().Where(instructor => instructor.Id != Id);
            return View(instructors);
        }

        public IActionResult ShowImage()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);
            var currInstructor = _InstructorService.GetById(Id);
            if (currInstructor?.image?.Length > 0 && currInstructor?.image != null)
            {
                string base64Image = Convert.ToBase64String(currInstructor.image);
                string imageSrc = $"data:image/jpeg;base64,{base64Image}";
                return PartialView("_ImagePartial", imageSrc);
            }
            return PartialView("_ImagePartial", "/img/undraw_profile.svg");


        }
        [Authorize(Roles = "Admin")]
        IActionResult getRoleByInstructor(Instructor instructor)
        {
            Role roles = _RoleService.GetById(instructor.RoleId);
            return Json(roles);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            ViewData["RoleList"] = _RoleService.GetAll();
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult New(Instructor _instructor, IFormFile ? image)
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
        [Authorize(Roles = "Admin")]
        public ActionResult Save(Instructor _instructor, IFormFile imageFile)
        {
            if (ModelState.IsValid == true)
            {

                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        _instructor.image = memoryStream.ToArray();
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
        [Authorize]
        public IActionResult Edit(int id)
        {
            Instructor instructor = _InstructorService.GetById(id);
            ViewData["RoleList"] = _RoleService.GetAll();
            ViewBag.EncryptedPassword = instructor.Password;
            return View(instructor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Instructor instructor, IFormFile? imageFile)
        {
            try
            {


                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("Password");
                if (ModelState.IsValid == true)//Server Side Check
                {

                    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    int Id = int.Parse(userIdClaim.Value);
                    string password = _InstructorService.GetById(Id).Password;
                    instructor.Password = password;
                    var StoredInstructor = _InstructorService.GetByIdAsNoTracking(Id);

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            imageFile.CopyTo(memoryStream);
                            instructor.image = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        instructor.image = StoredInstructor.image;

                    }



                    if (instructor.RoleId == 0)
                    {
                        instructor.RoleId = StoredInstructor.RoleId;
                    }

                    _InstructorService.Update(instructor);

                    if (instructor.RoleId != StoredInstructor.RoleId)
                    {

                        // Retrieve the current user's claims identity and create a new claims principal
                        ClaimsIdentity identity = (ClaimsIdentity)User.Identity;

                        // Locate the existing claim for the role
                        Claim existingRoleClaim = identity.FindFirst(ClaimTypes.Role);

                        // Remove the existing role claim
                        identity.RemoveClaim(existingRoleClaim);

                        // Add the updated role claim

                        identity.AddClaim(new Claim(ClaimTypes.Role, _RoleService.GetById(instructor.RoleId).Name));


                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);



                        // Sign in the user with the updated claims principal
                         HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);




                    }
                    if (Id == instructor.Id)
                    {
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
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _InstructorService.Delete(id);
            return RedirectToAction("Index");

        }
    }

}
