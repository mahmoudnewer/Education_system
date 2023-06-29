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
    public class UserController : Controller
    {
        IUserService userrepository;
        IInstructorService instructorRepository;
        public UserController(IUserService _userrepository, IInstructorService _instructorRepository)
        {
            userrepository = _userrepository;
            instructorRepository = _instructorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User_ViewModel user)
        {
            string hashedPassword =instructorRepository.GetAll().FirstOrDefault(i=>i.Email== user.Email).Password;

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword);

            if (isPasswordCorrect)
            {
                Instructor instructor = userrepository.Get(user.Email, user.Password);
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, instructor.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, instructor.Name.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, userrepository.GetRole(instructor.Id)));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("index", "Home");
            }
          
            return View(user);
        }
        [Authorize]
        public  IActionResult Profile()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
           
                int Id = int.Parse(userIdClaim.Value);
                Instructor instructor = instructorRepository.GetById(Id);
                ProfileViewModel profile = new ProfileViewModel()
                {
                    Name = instructor.Name,
                    Email = instructor.Email,
                    Phone = instructor.Phone,
                    image = instructor.image,
                    Address = instructor.Address,
                    Age=instructor.Age,
                    Role=userrepository.GetRole(Id)
                };
                return View(profile);
            

        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("index");
        }

    }
}
