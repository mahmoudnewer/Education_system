using Education.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Education.ViewModel;
using BCrypt.Net;
using Education.Services;

namespace Education.Controllers
{
    public class AccountController : Controller
    {
      
       private readonly IInstructorService InstructorService;
        private readonly IRoleService RoleService;
        public AccountController( IInstructorService _InstructorService, IRoleService _RoleService)
        {
           
            InstructorService = _InstructorService;
            RoleService = _RoleService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword (InstructorPassword instructor)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);
            Instructor instructorLogined = InstructorService.GetById(Id);
        
            try
            {
                if (ModelState.IsValid)
                {
                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(instructor.OldPassword, instructorLogined.Password);
                    if (isPasswordCorrect)
                    {
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(instructor.Password);
                        InstructorService.UpdatePassword(Id, hashedPassword);

                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Please enter correct password");
                    }
                    
                }
                return View(instructor);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
            }
            return View(instructor);
            
        }
      
        public IActionResult ChangePassword ()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);
            Instructor instructorLogined = InstructorService.GetById(Id);
            ViewBag.email = instructorLogined.Email;
            return View("ChangePassword");
        }
        [HttpPost]
        public IActionResult Login(User_ViewModel user)
        {
            Instructor instructor = InstructorService.GetAll().FirstOrDefault(i => i.Email == user.Email);
            if (instructor == null)
            {
                ModelState.AddModelError("Email", "No user found with this email.");
                return View(user);
            }

            string hashedPassword = InstructorService.GetAll().FirstOrDefault(i => i.Email == user.Email).Password;
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword);

            if (isPasswordCorrect)
            {
                instructor = InstructorService.GetByEmaillAndPassword(user.Email, hashedPassword);
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, instructor.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, instructor.Name.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, RoleService.GetById(instructor.RoleId).Name));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Incorrect password.");
            }


            return View(user);
        }
        [Authorize]
        public  IActionResult Profile()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
           
                int Id = int.Parse(userIdClaim.Value);
                Instructor instructor = InstructorService.GetById(Id);
            ProfileViewModel profile = new ProfileViewModel()
            {
                Id = Id,
                Name = instructor.Name,
                Email = instructor.Email,
                Phone = instructor.Phone,
                image = instructor.image,
                Address = instructor.Address,
                Age = instructor.Age,
                Role = RoleService.GetById(instructor.RoleId).Name
                };
                return View(profile);
            

        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Login");
        }

    }
}
