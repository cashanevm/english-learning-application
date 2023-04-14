using System;
using System.Collections.Generic;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    public class TestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tests
        public IActionResult Index()
        {
            return View(_context.Tests
                .Include(l => l.Language)
                .ToList());
        }

        // GET: Tests/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = _context.Tests
                .Include(t => t.Language)
                .Include(t => t.Words)
                .FirstOrDefault(t => t.ID == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            ViewData["Words"] = new SelectList(_context.Words, "ID", "OriginalWord");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "ID", "Key");



            return View();
        }

        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TimePerWord,OwnerId,Options,LanguageId")] Test test, List<int> Words)
        {
            if (test != null)
            {
                try
                {

                  if (Words != null && Words.Any())
                    {
                        var wordsToAdd = _context.Words.Where(w => Words.Contains(w.ID)).ToList();
                        test.Words.AddRange((IEnumerable<Word>)wordsToAdd);
                    }

                    _context.Update((Test)test);

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.ID))
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
            ViewData["Words"] = new SelectList(_context.Words, "ID", "OriginalWord");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "ID", "Key");

            return View(test);
        }

        // GET: Tests/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = _context.Tests.Find(id);
            if (test == null)
            {
                return NotFound();
            }

            ViewData["Words"] = new SelectList(_context.Words, "ID", "OriginalWord");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "ID", "Key");


            return View(test);
        }

        // POST: Tests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Test test, List<int> Words)
        {
            if (id != test.ID)
            {
                return NotFound();
            }

            var oldTest = _context.Tests
              .Include(t => t.Language)
              .Include(t => t.Words)
              .FirstOrDefault(t => t.ID == test.ID);

            if (oldTest != null)
            {
                try
                {
                    oldTest.TimePerWord = test.TimePerWord;
                    oldTest.OwnerId = test.OwnerId;
                    oldTest.Options = test.Options;
                    oldTest.LanguageId = test.LanguageId;
                    oldTest.Words.Clear();

                    if (Words != null && Words.Any())
                    {
                        var wordsToAdd = _context.Words.Where(w => Words.Contains(w.ID)).ToList();
                        oldTest.Words.AddRange(wordsToAdd);
                    }

                    _context.Update(oldTest);

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.ID))
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
            ViewData["Words"] = new SelectList(_context.Words, "ID", "OriginalWord");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "ID", "Key");

            return View(test);
        }

        // GET: Tests/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = _context.Tests
                .FirstOrDefault(t => t.ID == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var test = _context.Tests.Find(id);
            _context.Tests.Remove(test);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(int id)
        {
            return _context.Tests.Any(t => t.ID == id);
        }

        // GET: Tests/Start
        public IActionResult Start(int id)
        {
            var test = _context.Tests
                .Include(t => t.Language)
                .Include(t => t.Words).ThenInclude(w => w.TranslatedWords)
                .FirstOrDefault(t => t.ID == id);

            if (test == null)
            {
                return NotFound();
            }

            var wordIdToEnglishTranslationMap = new Dictionary<int, TranslatedWord>();

            test.Words.ForEach(
                wordd =>
                {
                    TranslatedWord tt = wordd.TranslatedWords
                .FirstOrDefault(tw => tw.LanguageId == test.LanguageId);

                    if (!wordIdToEnglishTranslationMap.ContainsKey(wordd.ID))
                    {
                        wordIdToEnglishTranslationMap[wordd.ID] = tt;
                    }
                }

                );

            ViewData["WordIdToTranslationMap"] = wordIdToEnglishTranslationMap;

            ViewData["TranslatedWords"] = ((IEnumerable<SelectListItem>)new SelectList(_context.TranslatedWords.Where(tw => tw.LanguageId.Equals(test.LanguageId)), "ID", "Translation")).ToList();


            return View("Start", test);
        }

        public ActionResult StartTest(int wordId, int? option, string originalWord, string translationWord)
        {
            if (option.HasValue)
            { 
                TranslatedWord translatin = _context.TranslatedWords
                    .Include(t => t.Word)
                    .FirstOrDefault(t => t.ID == option);

                // Check if the selected option is correct
                bool isCorrect = !(translatin == null || translatin.WordId != wordId);

                ViewData["originalWord"] = originalWord;

                if (isCorrect) {
                    ViewData["translationWord"] = translatin.Translation;
                } else {
                    ViewData["translationWord"] = translationWord;
                }
                
                return View("StartTest", isCorrect);
            }
            else
            {
                ViewData["originalWord"] = originalWord;
                ViewData["translationWord"] = translationWord;
                ViewData["Message"] = "Time is up";

                return View("StartTest", false);
            }
        }

    }
}

