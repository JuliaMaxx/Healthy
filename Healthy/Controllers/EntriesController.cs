using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Healthy.Data;
using Healthy.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace GetHealthy.Controllers
{
    [Authorize]
    public class EntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Entries
        [Authorize]

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Entry.Include(e => e.Food).Include(e => e.User).Where(e => e.IntakeTime.Date == today).Where(e => e.IdentityUserId == currentUserID);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Entries/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry
                .Include(e => e.Food)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: Entries/Create
        [Authorize]

        public IActionResult Create()
        {
            PopulateSelectLists();
            return View();
        }

        // POST: Entries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MealType,Ate,Quantity,FoodId,IntakeTime")] Entry entry)
        {
            entry.IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            entry.IntakeTime = DateTime.Today.Add(entry.IntakeTime.TimeOfDay);
            entry.Name = "Intake";

            _context.Add(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void PopulateSelectLists(object selectedFood = null)
        {
            var foodsQuery = from f in _context.Food
                             orderby f.Name
                             select f;
            ViewBag.FoodList = new SelectList(foodsQuery.AsNoTracking(), "Id", "Name", selectedFood);
        }

        // GET: Entries/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry.FindAsync(id);
            entry.IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            entry.IntakeTime = DateTime.Today.Add(entry.IntakeTime.TimeOfDay);
            entry.Name = "Intake";

            PopulateSelectLists(entry.FoodId);
            return View(entry);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MealType,Ate,Quantity,FoodId,IntakeTime,IdentityUserId")] Entry entry)
        {
            if (id != entry.Id)
            {
                return NotFound();
            }
            entry.IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            entry.Name = "Intake";
            entry.IntakeTime = DateTime.Today.Add(entry.IntakeTime.TimeOfDay);
            try
            {
                _context.Update(entry);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(entry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Entries/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry
                .Include(e => e.Food)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _context.Entry.FindAsync(id);
            if (entry != null)
            {
                _context.Entry.Remove(entry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Entries/History
        [Authorize]

        public async Task<IActionResult> History()
        {
            var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Entry
            .Include(e => e.Food)
            .Include(e => e.User)
            .Where(e => e.IdentityUserId == currentUserID);

            return View(await applicationDbContext.ToListAsync());
        }

        private bool EntryExists(int id)
        {
            return _context.Entry.Any(e => e.Id == id);
        }
    }
}
