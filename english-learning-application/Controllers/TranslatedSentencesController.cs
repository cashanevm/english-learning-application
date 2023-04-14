using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    public class TranslatedSentencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TranslatedSentencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TranslatedSentence
        public async Task<IActionResult> Index()
        {
            var translatedSentences = await _context.TranslatedSentences.Include(l => l.Contexts)
                .Include(l => l.Word)
                .Include(l => l.Sentence)
                .Include(l => l.Language).ToListAsync();
            return View(translatedSentences);
        }

        // GET: TranslatedSentence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translatedSentence = await _context.TranslatedSentences.Include(l => l.Contexts)
                .Include(l => l.Word)
                .Include(l => l.Sentence)
                .Include(l => l.Language)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (translatedSentence == null)
            {
                return NotFound();
            }

            return View(translatedSentence);
        }

        // GET: TranslatedSentence/Create
        public IActionResult Create()
        {
            ViewData["Words"] = _context.Words.ToList();
            ViewData["Sentences"] = _context.Sentences.ToList();
            ViewData["Languages"] = _context.Languages.ToList();

            return View();
        }

        // POST: TranslatedSentence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerId,WordId,SentenceId,LanguageId,Translation")] TranslatedSentence translatedSentence)
        {

            var sentence = await _context.Sentences.FirstOrDefaultAsync(m => m.ID == translatedSentence.SentenceId);
            var language = await _context.Languages.FirstOrDefaultAsync(m => m.ID == translatedSentence.LanguageId);
            var word = await _context.Words.FirstOrDefaultAsync(m => m.ID == translatedSentence.WordId);


            if (sentence!= null && language!=null && word!= null && IsUnique(translatedSentence.ID, translatedSentence.Translation))
            {
                translatedSentence.Sentence = sentence;
                translatedSentence.Language = language;
                translatedSentence.Word = word;


                _context.Add(translatedSentence);
                if (_context.SaveChanges() != 1)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Words"] = _context.Words.ToList();
            ViewData["Sentences"] = _context.Sentences.ToList();
            ViewData["Languages"] = _context.Languages.ToList();
            return View(translatedSentence);
        }

        // GET: TranslatedSentence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translatedSentence = await _context.TranslatedSentences.FindAsync(id);
            if (translatedSentence == null)
            {
                return NotFound();
            }

            ViewData["Words"] = _context.Words.ToList();
            ViewData["Sentences"] = _context.Sentences.ToList();
            ViewData["Languages"] = _context.Languages.ToList();
            return View(translatedSentence);
        }

        // POST: TranslatedSentence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OwnerId,WordId,SentenceId,LanguageId,Translation")] TranslatedSentence translatedSentence)
        {
            if (id != translatedSentence.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid && IsUnique(translatedSentence.ID, translatedSentence.Translation))
            {
                try
                {
                    _context.Update(translatedSentence);
                    if (_context.SaveChanges() != 1)
                    {
                        return BadRequest();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslatedSentenceExists(translatedSentence.ID))
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
            return View(translatedSentence);
        }

        // GET: TranslatedSentence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translatedSentence = await _context.TranslatedSentences
                .FirstOrDefaultAsync(m => m.ID == id);
            if (translatedSentence == null)
            {
                return NotFound();
            }

            return View(translatedSentence);
        }

        // POST: TranslatedSentence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var translatedSentence = await _context.TranslatedSentences.FindAsync(id);
            _context.TranslatedSentences.Remove(translatedSentence);
            if (_context.SaveChanges() != 1)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TranslatedSentenceExists(int id)
        {
            return _context.TranslatedSentences.Any(e => e.ID == id);
        }

        [HttpGet]
        public JsonResult IsTranslationUnique(int ID, string Translation)
        {
            var isUnique = !_context.TranslatedSentences.Any(t => t.ID != ID && t.Translation == Translation);
            return Json(isUnique);
        }

        public bool IsUnique(int ID, string Translation)
        {
            return !_context.TranslatedSentences.Any(t => t.ID != ID && t.Translation == Translation);
        }
    }

}


