
using System;
using english_learning_application.Controllers.Dto;
using english_learning_application.Data;
using english_learning_application.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    [ApiController]
    [Route("api/display/words")]
    public class DisplayWordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DisplayWordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<DisplayWordResponseDto> GetDisplayWords()
        {
            return _context.DisplayWords
                  .Include(l => l.Word)
                .ToList().Select(x => new DisplayWordResponseDto(x));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayWordResponseDto>> GetDisplayWord(int id)
        {
            var displayWord = await _context.DisplayWords.FindAsync(id);

            if (displayWord == null)
            {
                return NotFound();
            }

            return new DisplayWordResponseDto(displayWord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisplayWord(int id, DisplayWordRequestDto displayWord)
        {

            DisplayWord updatedContext = new DisplayWord();
            updatedContext.Display = displayWord.Display;
            updatedContext.WordId = displayWord.WordId;
            updatedContext.ID = id;

            _context.Entry(updatedContext).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisplayWordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DisplayWordResponseDto>> PostDisplayWord(DisplayWordRequestDto displayWord)
        {
            DisplayWord updatedContext = new DisplayWord();
            updatedContext.Display = displayWord.Display;
            updatedContext.WordId = displayWord.WordId;

            _context.DisplayWords.Add(updatedContext);
            await _context.SaveChangesAsync();

            return new DisplayWordResponseDto(updatedContext);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisplayWord(int id)
        {
            var displayWord = await _context.DisplayWords.FindAsync(id);
            if (displayWord == null)
            {
                return NotFound();
            }

            _context.DisplayWords.Remove(displayWord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DisplayWordExists(int id)
        {
            return _context.DisplayWords.Any(e => e.ID == id);
        }
    }

}

