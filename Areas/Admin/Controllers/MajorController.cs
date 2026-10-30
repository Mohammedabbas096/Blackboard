using Group1Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group1Blackboard.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MajorController : Controller
    {
        private readonly BlackboardContext _db;

        public MajorController(BlackboardContext db)
        {
            _db = db;
        }

        // GET: Admin/Major
        public async Task<IActionResult> Index()
        {
            var majors = await _db.Majors
                .Include(m => m.Courses)
                .OrderBy(m => m.Name)
                .ToListAsync();
            return View(majors);
        }

        // GET: Admin/Major/Create
        public IActionResult Create()
        {
            return View(new Major());
        }

        // POST: Admin/Major/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Major major)
        {
            if (ModelState.IsValid)
            {
                _db.Majors.Add(major);
                await _db.SaveChangesAsync();
                TempData["message"] = $"Major \"{major.Name}\" created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(major);
        }

        // GET: Admin/Major/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var major = await _db.Majors.FindAsync(id);
            if (major == null) return NotFound();
            return View(major);
        }

        // POST: Admin/Major/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Major major)
        {
            if (ModelState.IsValid)
            {
                _db.Majors.Update(major);
                await _db.SaveChangesAsync();
                TempData["message"] = $"Major \"{major.Name}\" updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(major);
        }

        // GET: Admin/Major/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var major = await _db.Majors
                .Include(m => m.Courses)
                .FirstOrDefaultAsync(m => m.MajorId == id);
            if (major == null) return NotFound();
            return View(major);
        }

        // POST: Admin/Major/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var major = await _db.Majors
                .Include(m => m.Courses)
                .FirstOrDefaultAsync(m => m.MajorId == id);
            if (major == null) return NotFound();

            if (major.Courses.Any())
            {
                TempData["error"] = $"Cannot delete \"{major.Name}\" because it has {major.Courses.Count} course(s) assigned to it.";
                return RedirectToAction(nameof(Index));
            }

            _db.Majors.Remove(major);
            await _db.SaveChangesAsync();
            TempData["message"] = $"Major \"{major.Name}\" deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
