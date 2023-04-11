using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class DisplayWordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisplayWordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DisplayWord
        public async Task<IActionResult> Index()
        {
            var displayWords = await _context.DisplayWords
                .Include(d => d.Word)
                .ToListAsync();

            return View(displayWords);
        }

        // GET: DisplayWord/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var displayWord = await _context.DisplayWords
                .Include(d => d.Word)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (displayWord == null)
            {
                return NotFound();
            }

            return View(displayWord);
        }

        // GET: DisplayWord/Create
        public IActionResult Create()
        {
            ViewData["Words"] = _context.Words.Select(w => new SelectListItem { Value = w.ID.ToString(), Text = w.OriginalWord });

            return View();
        }

        // POST: DisplayWord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WordId,Display")] DisplayWord displayWord)
        {
            var word = await _context.Words.FirstOrDefaultAsync(m => m.ID == displayWord.WordId);

            if (word!= null)
            {

                displayWord.Word = word;
                _context.Add(displayWord);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["Words"] = _context.Words.Select(w => new SelectListItem { Value = w.ID.ToString(), Text = w.OriginalWord });

            return View(displayWord);
        }

        // GET: DisplayWord/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var displayWord = await _context.DisplayWords.FindAsync(id);

            if (displayWord == null)
            {
                return NotFound();
            }

            ViewData["Words"] = _context.Words.Select(w => new SelectListItem { Value = w.ID.ToString(), Text = w.OriginalWord });
              

            return View(displayWord);
        }

        // POST: DisplayWord/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,WordId,Display")] DisplayWord displayWord)
        {
            if (id != displayWord.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(displayWord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisplayWordExists(displayWord.ID))
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

            ViewData["Words"] = _context.Words.Select(w => new SelectListItem { Value = w.ID.ToString(), Text = w.OriginalWord });

            return View(displayWord);
        }

        // GET: DisplayWord/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var displayWord = await _context.DisplayWords
                .Include(d => d.Word)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (displayWord == null)
            {
                return NotFound();
            }

            return View(displayWord);
        }

        // POST: DisplayWord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var displayWord = await _context.DisplayWords.FindAsync(id);
            if (displayWord == null)
            {
                return NotFound();
            }

            _context.DisplayWords.Remove(displayWord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisplayWordExists(int id)
        {
            return _context.DisplayWords.Any(e => e.ID == id);
        }

        [HttpGet]
        public JsonResult IsDisplayUnique(int ID, string Display)
        {
            var isUnique = !_context.DisplayWords.Any(dw => dw.ID != ID && dw.Display == Display);
            return Json(isUnique);
        }
    }
}

