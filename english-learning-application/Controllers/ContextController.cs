using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class ContextController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContextController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Context
        public IActionResult Index()
        {
            var contexts = _context.Contexts.ToList();
            return View(contexts);
        }

        // GET: Context/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var context = _context.Contexts
                .Include(c => c.TranslatedWords)
                .Include(c => c.TranslatedSentences)
                .Include(c => c.Sentences)
                .FirstOrDefault(c => c.ID == id);

            if (context == null)
            {
                return NotFound();
            }

            return View(context);
        }

        // GET: Context/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Context/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name")] Context context)
        {
            if (ModelState.IsValid)
            {
                _context.Add(context);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(context);
        }

        // GET: Context/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var context = _context.Contexts.Find(id);

            if (context == null)
            {
                return NotFound();
            }

            return View(context);
        }

        // POST: Context/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Name")] Context context)
        {
            if (id != context.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(context);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContextExists(context.ID))
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

            return View(context);
        }

        // GET: Context/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var context = _context.Contexts
                .FirstOrDefault(c => c.ID == id);

            if (context == null)
            {
                return NotFound();
            }

            return View(context);
        }

        // POST: Context/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var context = _context.Contexts.Find(id);
            _context.Contexts.Remove(context);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ContextExists(int id)
        {
            return _context.Contexts.Any(c => c.ID == id);
        }
    }
}

