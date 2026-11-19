using Group1Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Group1Blackboard.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly BlackboardContext _db;

        public CourseController(BlackboardContext db)
        {
            _db = db;
        }

        // GET: Admin/Course
        public async Task<IActionResult> Index()
        {
            var courses = await _db.Courses
                .Include(c => c.Major)
                .Include(c => c.Instructor)
                .OrderBy(c => c.CourseNumber)
                .ToListAsync();
            return View(courses);
        }

        // GET: Admin/Course/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View(new Course());
        }

        // POST: Admin/Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            string courseNumError = Check.CourseNumberExists(_db, course.CourseNumber);
            if (!string.IsNullOrEmpty(courseNumError))
            {
                ModelState.AddModelError(nameof(course.CourseNumber), courseNumError);
            }

            if (ModelState.IsValid)
            {
                _db.Courses.Add(course);
                await _db.SaveChangesAsync();
                TempData["message"] = $"Course \"{course.CourseNumber} – {course.Title}\" created successfully.";
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(course.MajorId, course.InstructorId);
            return View(course);
        }

        // GET: Admin/Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            await PopulateDropdownsAsync(course.MajorId, course.InstructorId);
            return View(course);
        }

        // POST: Admin/Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            string courseNumError = Check.CourseNumberExists(_db, course.CourseNumber, course.CourseId);
            if (!string.IsNullOrEmpty(courseNumError))
            {
                ModelState.AddModelError(nameof(course.CourseNumber), courseNumError);
            }

            if (ModelState.IsValid)
            {
                _db.Courses.Update(course);
                await _db.SaveChangesAsync();
                TempData["message"] = $"Course \"{course.CourseNumber} – {course.Title}\" updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(course.MajorId, course.InstructorId);
            return View(course);
        }

        // GET: Admin/Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _db.Courses
                .Include(c => c.Major)
                .Include(c => c.Instructor)
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: Admin/Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _db.Courses
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null) return NotFound();

            if (course.Reservations.Any())
            {
                TempData["error"] = $"Cannot delete this course because {course.Reservations.Count} student reservation(s) exist for it.";
                return RedirectToAction(nameof(Index));
            }

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();
            TempData["message"] = "Course deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync(int selectedMajor = 0, int selectedInstructor = 0)
        {
            ViewBag.Majors = new SelectList(
                await _db.Majors.OrderBy(m => m.Name).ToListAsync(),
                nameof(Major.MajorId), nameof(Major.Name), selectedMajor);

            ViewBag.Instructors = new SelectList(
                (await _db.Users.OrderBy(u => u.LastName).ToListAsync())
                    .Select(u => new { u.UserId, FullName = u.FullName }),
                "UserId", "FullName", selectedInstructor);

            ViewBag.Schedules = new SelectList(Course.Schedules);
        }
    }
}
