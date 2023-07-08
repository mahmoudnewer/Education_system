using Education.Enums;
using Education.Models;
using Education.Repositories;
using Education.Services;
using Education.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Education.Controllers
{
    public class LessonController : Controller
    {
       
        IInstructorService instructor_repo;
        ITopicService topic_repo;
        public LessonController(IInstructorService _intructor_repo, ITopicService _topic_repo)
        {
            instructor_repo = _intructor_repo;
            topic_repo = _topic_repo;
        }
        [Authorize]
        public IActionResult Alllesson()
        {
            var lesson = topic_repo.GetAll();
            List<Lesson_infoViewModel> lesson_InfoViewModels = new List<Lesson_infoViewModel>();
            int Number = 0;

            foreach (var item in lesson)
            {
                ++Number;
                lesson_InfoViewModels.Add(new Lesson_infoViewModel()
                {
                    Number = Number,
                    Name = item.Name,
                    Date = item.Date,
                    Type = item.Type,
                    InstructorName =item.instructor.Name
                }) ;
            }
           
            return View(lesson_InfoViewModels);
        }
        [Authorize]
        public IActionResult Mylesson()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);

            var lesson = topic_repo.GetAll().Where(l=>l.InstructorId==Id);
            List<Lesson_infoViewModel> lesson_InfoViewModels = new List<Lesson_infoViewModel>();
            int Number = 0;

            foreach (var item in lesson)
            {
                ++Number;
                lesson_InfoViewModels.Add(new Lesson_infoViewModel()
                {
                    Number = Number,
                    Name = item.Name,
                    Date = item.Date,
                    Type = item.Type,
                });
            }

            return View(lesson_InfoViewModels);
        }
        [Authorize(Roles="Admin")]
        public IActionResult Addlesson()
        {
            ViewData["intructor"] = instructor_repo.GetAll();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Addlesson(Topic topic)
        {
            if (ModelState.IsValid)
            {

                topic_repo.Insert(topic);
                return RedirectToAction("Alllesson");
            }
            ViewData["intructor"] = instructor_repo.GetAll();
            return View(topic);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search_alllesson(DateTime date)
        {

            var lesson = topic_repo.GetAll().Where(i => i.Date.Date == date.Date).ToList();

            if (lesson.Count == 0)
            {
                TempData["NoData"] = "No data found.";
                return RedirectToAction("alllesson");
            }
            List<Lesson_infoViewModel> lesson_InfoViewModels = new List<Lesson_infoViewModel>();
            int Number = 0;

            foreach (var item in lesson)
            {
                ++Number;
                lesson_InfoViewModels.Add(new Lesson_infoViewModel()
                {
                    Number = Number,
                    Name = item.Name,
                    Date = item.Date,
                    Type = item.Type,
                    InstructorName = item.instructor.Name
                });
            }


            return View("alllesson", lesson_InfoViewModels);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search_Mylesson(DateTime date)
        {

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int Id = int.Parse(userIdClaim.Value);

            var lesson = topic_repo.GetAll().Where(l => l.InstructorId == Id && l.Date.Date == date.Date).ToList();
            if (lesson.Count == 0)
            {
                TempData["NoData"] = "No data found.";
                return RedirectToAction("mylesson");
            }
            List<Lesson_infoViewModel> lesson_InfoViewModels = new List<Lesson_infoViewModel>();

            int Number = 0;

            foreach (var item in lesson)
            {
                ++Number;
                lesson_InfoViewModels.Add(new Lesson_infoViewModel()
                {
                    Number = Number,
                    Name = item.Name,
                    Date = item.Date,
                    Type = item.Type,
                });
            }

            return View("mylesson", lesson_InfoViewModels);
        }
    }
}
