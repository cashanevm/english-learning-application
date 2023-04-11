using System;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tag
        public IActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }

        // GET: /Tag/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _context.Tags
                .Include(l => l.Words)
                .FirstOrDefault(t => t.ID == id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: /Tag/Create
        public IActionResult Create()
        {
            ViewData["Words"] = _context.Words.ToList();
            return View();
        }

        // POST: /Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Tags.Add(tag);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: /Tag/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _context.Tags
                .Include(l => l.Words)
                .FirstOrDefault(t => t.ID == id);



            

            if (tag == null)
            {
                return NotFound();
            }

            ViewData["Words"] = _context.Words.ToList();

            return View(tag);
        }

        // POST: /Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Tag tag)
        {
            if (id != tag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Tags.Update(tag);
                _context.SaveChanges();

                //tag.Words.Select(word =>
                //{
                //    Word newWord = _context.Words.FirstOrDefault(s => s.ID == word.ID);
                //    if (newWord != null)
                //    {
                //        newWord.Tags.Add(tag);
                //        _context.SaveChanges();
                //    }

                //    return newWord;
                //});

                

                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        // GET: /Tag/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _context.Tags.FirstOrDefault(t => t.ID == id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: /Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.ID == id);
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

