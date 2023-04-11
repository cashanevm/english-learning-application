using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class DisplaySentenceController : Controller
    {
		

       private readonly ApplicationDbContext _context;

        public DisplaySentenceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DisplaySentence
        public async Task<IActionResult> Index()
        {
            var displaySentences = await _context.DisplaySentences
                .Include(d => d.Sentence)
                .ToListAsync();
            return View(displaySentences);
        }

        // GET: DisplaySentence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var displaySentence = await _context.DisplaySentences
                .Include(d => d.Sentence)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (displaySentence == null)
            {
                return NotFound();
            }

            return View(displaySentence);
        }

        // GET: DisplaySentence/Create
        public IActionResult Create()
        {
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "ID", "OriginalSentence");
            return View();
        }

        // POST: DisplaySentence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SentenceId,Display")] DisplaySentence displaySentence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(displaySentence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "ID", "OriginalSentence", displaySentence.SentenceId);
            return View(displaySentence);
        }

        // GET: DisplaySentence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var displaySentence = await _context.DisplaySentences.FindAsync(id);
            if (displaySentence == null)
            {
                return NotFound();
            }
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "ID", "OriginalSentence", displaySentence.SentenceId);
            return View(displaySentence);
        }

        // POST: DisplaySentence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SentenceId,Display")] DisplaySentence displaySentence)
        {
            if (id != displaySentence.ID)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(displaySentence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisplaySentenceExists(displaySentence.ID))
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
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "ID", "OriginalSentence", displaySentence.SentenceId);
            return View(displaySentence);
        }

        // GET: DisplaySentence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var displaySentence = await _context.DisplaySentences
                .Include(d => d.Sentence)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (displaySentence == null)
            {
                return NotFound();
            }

            return View(displaySentence);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var displaySentence = await _context.DisplaySentences.FindAsync(id);
            if (displaySentence == null)
            {
                return NotFound();
            }

            _context.DisplaySentences.Remove(displaySentence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisplaySentenceExists(int id)
        {
            return _context.DisplaySentences.Any(e => e.ID == id);
        }

    }
}

