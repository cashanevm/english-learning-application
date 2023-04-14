using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class WordController : Controller
	{
        private readonly ApplicationDbContext _context;

        public WordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
            return View(await _context.Words.ToListAsync());
        }

        // GET: Words/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Words
                .Include(w => w.Tags)
                .Include(w => w.DisplayWords)
                .Include(w => w.Sentences)
                .Include(w => w.TranslatedSentences)
                .Include(w => w.TranslatedWords)
                .Include(w => w.Tests)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // GET: Words/Create
        public IActionResult Create()
        {

            ViewData["Tags"] = _context.Tags.ToList();

            return View();
        }

        // POST: Words/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OriginalWord")] Word word)
        {
            if (ModelState.IsValid && IsWordUnique(word.ID, word.OriginalWord))
            {
                _context.Add(word);
                if (_context.SaveChanges() != 1)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        // GET: Words/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

           

            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Words.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }
            ViewData["Tags"] = _context.Tags.ToList();

            return View(word);
        }

        // POST: Words/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OriginalWord,Tags")] Word word)
        {
            if (id != word.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid && IsWordUnique(word.ID, word.OriginalWord))
            {
                try
                {
                    // Load the entity with its related entities
                    var entity = _context.Words
                        .Include(e => e.Tags)
                        .Single(e => e.ID == id);

                    List<int> tags = word.Tags.ToList().Select(
                        l => l.ID
                        ).ToList();

                    // Update the related entities
                    entity.Tags.Clear(); // Remove existing related entities
                    foreach (int relatedEntity in tags)
                    {
                        var relatedEntitynew = _context.Tags.Find(relatedEntity);
                        relatedEntitynew.Words.Add(entity);
                        entity.Tags.Add(relatedEntitynew);
                    }

                    // Update the word properties
                    entity.OriginalWord = word.OriginalWord;

                    // Save the changes to the database
                    if (_context.SaveChanges() != 1)
                    {
                        return BadRequest();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordExists(word.ID))
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

            ViewData["Tags"] = _context.Tags.ToList();
            return View(word);
        }

        // GET: Words/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Words
                .FirstOrDefaultAsync(m => m.ID == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var word = await _context.Words.FindAsync(id);
            _context.Words.Remove(word);
            if (_context.SaveChanges() != 1)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool WordExists(int id)
        {
            return _context.Words.Any(e => e.ID == id);
        }

        [HttpGet]
        public JsonResult IsOriginalWordUnique(int ID, string OriginalWord)
        {
            var isUnique = !_context.Words.Any(w => w.ID != ID && w.OriginalWord == OriginalWord);
            return Json(isUnique);
        }

        public bool IsWordUnique(int ID, string OriginalWord)
        {
            return !_context.Words.Any(w => w.ID != ID && w.OriginalWord == OriginalWord);
        }
    }
}

