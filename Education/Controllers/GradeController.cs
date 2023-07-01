using Education.Models;
using Education.Repositories;
using Education.Services;
using Education.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Education.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeServices _GradeService;
        private readonly IStudentService _StudentService;


        public GradeController(IGradeServices GradeService, IStudentService StudentService)
        {
            _GradeService = GradeService;
            _StudentService = StudentService;
        }
        [Authorize]
        public IActionResult GetAll()
        {
            var grades = _GradeService.GetAll();
            return View(grades);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            ViewBag.Students = _StudentService.GetAll().Where(s=>s.IsDeleted==false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Add(Grade g)
        {
            var allStds = _StudentService.GetAll().Where(s => s.IsDeleted == false);
            try
            {
                if (ModelState.IsValid)
                {
                    _GradeService.Insert(g);
                    return RedirectToAction("Add");
                }
            }
            catch 
            {
                ModelState.AddModelError("studentId", "Please select Student");
            }
            if (g.studentId == 0)
            {
                ModelState.AddModelError("studentId", "Please select Student");
            }

            ViewBag.Students = allStds;
            return View(g);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            ViewBag.Students = _StudentService.GetAll().Where(s => s.IsDeleted == false);
            var g = _GradeService.GetById(id);
            return View(g);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Grade g)
        {
            if (ModelState.IsValid)
            {
                _GradeService.Update(g);
                return RedirectToAction("GetAll");

            }
            ViewBag.Students = _StudentService.GetAll().Where(s => s.IsDeleted == false);
            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteRefreshGrade(int id)
        {
            _GradeService.Delete(id);

            IEnumerable<Grade> g = _GradeService.GetAll();


            return PartialView("_AllGradePartial", g);
        }

    }
}
