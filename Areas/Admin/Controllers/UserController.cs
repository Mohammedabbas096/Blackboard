using Group1Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group1Blackboard.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly BlackboardContext _db;

        public UserController(BlackboardContext db)
        {
            _db = db;
        }

        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            var users = await _db.Users
                .Include(u => u.TeachingCourses)
                .Include(u => u.Reservations)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();
            return View(users);
        }

        // GET: Admin/User/Create
        public IActionResult Create()
        {
            return View(new AppUser());
        }

        // POST: Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                TempData["message"] = $"User \"{user.FullName}\" created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUser user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                TempData["message"] = $"User \"{user.FullName}\" updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users
                .Include(u => u.TeachingCourses)
                .Include(u => u.Reservations)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _db.Users
                .Include(u => u.TeachingCourses)
                .Include(u => u.Reservations)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return NotFound();

            if (user.TeachingCourses.Any())
            {
                TempData["error"] = $"Cannot delete \"{user.FullName}\" because they are assigned as instructor for {user.TeachingCourses.Count} course(s).";
                return RedirectToAction(nameof(Index));
            }

            if (user.Reservations.Any())
            {
                TempData["error"] = $"Cannot delete \"{user.FullName}\" because they have {user.Reservations.Count} existing course reservation(s). Reservation history must be preserved.";
                return RedirectToAction(nameof(Index));
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            TempData["message"] = $"User \"{user.FullName}\" deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
