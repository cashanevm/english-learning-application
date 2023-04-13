using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class SpeechPartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpeechPartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SpeechPart
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpeechParts.ToListAsync());
        }

        // GET: SpeechPart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speechPart = await _context.SpeechParts
                  .Include(l => l.TranslatedWords)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (speechPart == null)
            {
                return NotFound();
            }

            return View(speechPart);
        }

        // GET: SpeechPart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpeechPart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] SpeechPart speechPart)
        {
            if (ModelState.IsValid && IsUnique(speechPart.ID, speechPart.Name))
            {
                _context.Add(speechPart);
                if (_context.SaveChanges() != 1)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(speechPart);
        }

        // GET: SpeechPart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speechPart = await _context.SpeechParts.FindAsync(id);
            if (speechPart == null)
            {
                return NotFound();
            }
            return View(speechPart);
        }

        // POST: SpeechPart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] SpeechPart speechPart)
        {
            if (id != speechPart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid && IsUnique(speechPart.ID, speechPart.Name))
            {
                try
                {
                    _context.Update(speechPart);
                    if (_context.SaveChanges() != 1)
                    {
                        return BadRequest();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeechPartExists(speechPart.ID))
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
            return View(speechPart);
        }

        // GET: SpeechPart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speechPart = await _context.SpeechParts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (speechPart == null)
            {
                return NotFound();
            }

            return View(speechPart);
        }

        // POST: SpeechPart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speechPart = await _context.SpeechParts.FindAsync(id);
            _context.SpeechParts.Remove(speechPart);
            if (_context.SaveChanges() != 1)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SpeechPartExists(int id)
        {
            return _context.SpeechParts.Any(e => e.ID == id);
        }

        [HttpGet]
        public JsonResult IsNameUnique(int ID, string Name)
        {
            var isUnique = !_context.SpeechParts.Any(s => s.ID != ID && s.Name == Name);
            return Json(isUnique);
        }

        [HttpGet]
        public bool IsUnique(int ID, string Name)
        {
            return !_context.SpeechParts.Any(s => s.ID != ID && s.Name == Name);
        }
    }
}

