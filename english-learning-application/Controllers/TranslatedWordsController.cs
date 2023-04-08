using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class TranslatedWordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TranslatedWordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TranslatedWords
        public IActionResult Index()
        {
            return View(_context.TranslatedWords.ToList());
        }

        // GET: TranslatedWords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translatedWord = await _context.TranslatedWords
                .Include(t => t.Word)
                .Include(t => t.Language)
                .Include(t => t.SpeechPart)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (translatedWord == null)
            {
                return NotFound();
            }

            return View(translatedWord);
        }

        // GET: TranslatedWords/Create
        public IActionResult Create()
        {
            ViewData["Words"] = new SelectList(_context.Words, "ID", "OriginalWord");
            ViewData["Languages"] = new SelectList(_context.Languages, "ID", "Key");
            ViewData["SpeechParts"] = new SelectList(_context.SpeechParts, "ID", "Name");
            return View();
        }

        // POST: TranslatedWords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerId,WordId,Translation,LanguageId,SpeechPartId")] TranslatedWord translatedWord)
        {
            var speechPart = await _context.SpeechParts.FirstOrDefaultAsync(m => m.ID == translatedWord.SpeechPartId);
            var language = await _context.Languages.FirstOrDefaultAsync(m => m.ID == translatedWord.LanguageId);
            var word = await _context.Words.FirstOrDefaultAsync(m => m.ID == translatedWord.WordId);


            if (speechPart != null && language != null && word != null)
            {
                translatedWord.SpeechPart = speechPart;
                translatedWord.Language = language;
                translatedWord.Word = word;

                _context.Add(translatedWord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Words"] = new SelectList(_context.Words, "ID", "OriginalWord");
            ViewData["Languages"] = new SelectList(_context.Languages, "ID", "Key");
            ViewData["SpeechParts"] = new SelectList(_context.SpeechParts, "ID", "Name");
            return View(translatedWord);
        }

        // GET: TranslatedWords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translatedWord = await _context.TranslatedWords.FindAsync(id);
            if (translatedWord == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "ID", "Name", translatedWord.LanguageId);
            ViewData["SpeechPartId"] = new SelectList(_context.SpeechParts, "ID", "Name", translatedWord.SpeechPartId);
            ViewData["WordId"] = new SelectList(_context.Words, "ID", "Text", translatedWord.WordId);
            return View(translatedWord);
        }

        // POST: TranslatedWords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OwnerId,WordId,Translation,LanguageId,SpeechPartId")] TranslatedWord translatedWord)
        {
            if (id != translatedWord.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(translatedWord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslatedWordExists(translatedWord.ID))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "ID", "Name", translatedWord.LanguageId);
            ViewData["SpeechPartId"] = new SelectList(_context.SpeechParts, "ID", "Name", translatedWord.SpeechPartId);
            ViewData["WordId"] = new SelectList(_context.Words, "ID", "Text", translatedWord.WordId);
            return View(translatedWord);
        }

        // GET: TranslatedWords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translatedWord = await _context.TranslatedWords
                .Include(t => t.Word)
                .Include(t => t.Language)
                .Include(t => t.SpeechPart)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (translatedWord == null)
            {
                return NotFound();
            }

            return View(translatedWord);
        }

        // POST: TranslatedWords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var translatedWord = await _context.TranslatedWords.FindAsync(id);
            _context.TranslatedWords.Remove(translatedWord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranslatedWordExists(int id)
        {
            return _context.TranslatedWords.Any(e => e.ID == id);
        }

        [HttpGet]
        public JsonResult IsTranslationUnique(int ID, string Translation)
        {
            var isUnique = !_context.TranslatedWords.Any(tw => tw.ID != ID && tw.Translation == Translation);
            return Json(isUnique);
        }
    }
}


