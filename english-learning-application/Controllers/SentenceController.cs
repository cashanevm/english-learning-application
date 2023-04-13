using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class SentenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SentenceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sentence
        public async Task<IActionResult> Index()
        {
            var sentences = await _context.Sentences
                .Include(s => s.Word)
                .ToListAsync();
            return View(sentences);
        }

        // GET: Sentence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sentence = await _context.Sentences
                
                 .Include(l => l.Contexts)
                .Include(l => l.DisplaySentences)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.Word)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sentence == null)
            {
                return NotFound();
            }

            return View(sentence);
        }

        // GET: Sentence/Create
        public IActionResult Create()
        {
            ViewData["WordId"] = new SelectList(_context.Words, "ID", "OriginalWord");
            return View();
        }

        // POST: Sentence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WordId,OwnerId,OriginalSentence")] Sentence sentence)
        {
            var word = await _context.Words.FirstOrDefaultAsync(m => m.ID == sentence.WordId);

            if (word != null)
            {
                sentence.Word = word;

                _context.Add(sentence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WordId"] = new SelectList(_context.Words, "ID", "OriginalWord", sentence.WordId);
            return View(sentence);
        }

        // GET: Sentence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sentence = await _context.Sentences.FindAsync(id);
            if (sentence == null)
            {
                return NotFound();
            }
            ViewData["WordId"] = new SelectList(_context.Words, "ID", "OriginalWord", sentence.WordId);
            return View(sentence);
        }

        // POST: Sentence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,WordId,OwnerId,OriginalSentence")] Sentence sentence)
        {
            if (id != sentence.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sentence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SentenceExists(sentence.ID))
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
            ViewData["WordId"] = new SelectList(_context.Words, "ID", "OriginalWord", sentence.WordId);
            return View(sentence);
        }

        // GET: Sentence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sentence = await _context.Sentences
                .Include(s => s.Word)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sentence == null)
            {
                return NotFound();
            }

            return View(sentence);
        }

        // POST: Sentence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sentence = await _context.Sentences.FindAsync(id);
            if (sentence == null)
            {
                return NotFound();
            }

            // Check if the sentence has any associated display sentences or translated sentences
            var displaySentences = _context.DisplaySentences.Where(d => d.SentenceId == sentence.ID);
            var translatedSentences = _context.TranslatedSentences.Where(t => t.SentenceId == sentence.ID);

            if (displaySentences.Any() || translatedSentences.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot delete the sentence because it has associated display or translated sentences.");
                return View(sentence);
            }

            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(e => e.ID == id);
        }

        [HttpGet]
        public JsonResult IsOriginalSentenceUnique(int ID, string OriginalSentence)
        {
            var isUnique = !_context.Sentences.Any(s => s.ID != ID && s.OriginalSentence == OriginalSentence);
            return Json(isUnique);
        }
    }
}

