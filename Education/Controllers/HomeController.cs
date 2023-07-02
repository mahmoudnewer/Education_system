using Education.Models;
using Education.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace Education.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentService studentService;
        private readonly ITopicService topicService;
        private readonly IRequestsServices requestsService;
        private readonly IInstructorService instructorService;
        public HomeController(ILogger<HomeController> logger, IInstructorService _instructorService, IStudentService _studentService, ITopicService _topicService, IRequestsServices _requestsServices)
        {
            _logger = logger;
            instructorService = _instructorService;
            studentService = _studentService;
            topicService = _topicService;
            requestsService = _requestsServices;
        }
        [Authorize]
        public IActionResult Index()
        {

            TempData["instructor"] = instructorService.GetAll().Where(s=>s.Isdeleted==false).ToList().Count;
            TempData["student"] = studentService.GetAll().Where(s=>s.confirm=="accepted"&s.IsDeleted==false).ToList().Count;
            TempData["topic"] = topicService.GetAll().ToList().Count;

            if (User.IsInRole("Admin"))
                TempData["request"] = requestsService.GetAllRequestInfo().ToList().Count;

            else
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                int Id = int.Parse(userIdClaim.Value);
                TempData["request"] = requestsService.GetAllRequestInfo().Where(r => r.InstructorId == Id).ToList().Count;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}