using System;
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
        public IActionResult Create([Bind("ID,TimePerWord,OwnerId,Options,LanguageId")] Test test)
        {
            ViewData["Words"] = new SelectList(_context.Words, "ID", "OriginalWord");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "ID", "Key");

            if (ModelState.IsValid)
            {
                _context.Add(test);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
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
        public IActionResult Edit(int id, [Bind("ID,TimePerWord,OwnerId,Options,LanguageId")] Test test)
        {
            if (id != test.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
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
    }
}

