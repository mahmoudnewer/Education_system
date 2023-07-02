using Education.Models;
using Education.Repositories;
using Education.Services;
using Education.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;

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
        public IActionResult GenerateReport()
        {
            return View();
        }

            [HttpPost]
        public IActionResult GenerateReport(string selectedDate)
        {
            DateTime endDate = DateTime.ParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime startDate = new DateTime(endDate.Year, endDate.Month, 1);
            DateTime inclusiveEndDate = endDate.AddDays(1);
            var averageNewGrades = _GradeService.GetAll()
                .Where(g => g.Date >= startDate && g.Date < inclusiveEndDate && g.Type == "New")
                .GroupBy(g => g.student.Name)
                .Select(g => new
                {
                    StudentName = g.Key,
                    AverageGrade = g.Average(s => s.grade)
                })
                .ToList();

            var averageOldGrades = _GradeService.GetAll()
                .Where(g => g.Date >= startDate && g.Date < inclusiveEndDate && g.Type == "Old")
                .GroupBy(g => g.student.Name)
                .Select(g => new
                {
                    StudentName = g.Key,
                    AverageGrade = g.Average(s => s.grade)
                })
                .ToList();

            var attendanceDays = _GradeService.GetAll()
                .Where(g => g.Date >= startDate && g.Date < inclusiveEndDate)
                .GroupBy(g => g.student.Name)
                .Select(g => new
                {
                    StudentName = g.Key,
                    AttendanceDays = g.Select(g => g.Date.Date).Distinct().Count()
                })
                .ToList();

            var reportData = averageNewGrades
                .Join(averageOldGrades, n => n.StudentName, o => o.StudentName, (n, o) => new
                {
                    StudentName = n.StudentName,
                    AverageNewGrade = n.AverageGrade,
                    AverageOldGrade = o.AverageGrade
                })
                .Join(attendanceDays, grades => grades.StudentName, attendance => attendance.StudentName, (grades, attendance) => new
                {
                    StudentName = grades.StudentName,
                    AverageNewGrade = Math.Round(grades.AverageNewGrade, 2), // Round the average grade to 2 decimal places
                    AverageOldGrade = Math.Round(grades.AverageOldGrade, 2), // Round the average grade to 2 decimal places
                    AttendanceDays = attendance.AttendanceDays
                })
                .OrderByDescending(data => data.AttendanceDays + data.AverageNewGrade + data.AverageOldGrade)
                .ToList();
            return View(reportData.Cast<dynamic>().ToList());
        }

    }
}
