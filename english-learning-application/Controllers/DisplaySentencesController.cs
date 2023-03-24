using System;
using System.Linq;
using english_learning_application.Controllers.Dto;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
	
        [ApiController]
        [Route("api/display/sentences")]
        public class DisplaySentencesController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public DisplaySentencesController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IEnumerable<DisplaySentenceResponseDto> GetDisplaySentences()
            {
                return _context.DisplaySentences
                .Include(l => l.Sentence)
                .ToList().Select(x => new DisplaySentenceResponseDto(x));
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<DisplaySentenceResponseDto>> GetDisplaySentence(int id)
            {
            var displaySentence = FindDisplaySentence(id);

                if (displaySentence == null)
                {
                    return NotFound();
                }

                return new DisplaySentenceResponseDto(displaySentence);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutDisplaySentence(int id, DisplaySentenceRequestDto dto)
            {
            DisplaySentence updatedContext = new DisplaySentence();
            updatedContext.Display = dto.Display;
            updatedContext.SentenceId = dto.SentenceId;
            updatedContext.ID = id;

            _context.Entry(updatedContext).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisplaySentenceExists(id))
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
            public async Task<ActionResult<DisplaySentenceResponseDto>> PostDisplaySentence(DisplaySentenceRequestDto dto)
            {
            DisplaySentence updatedContext = new DisplaySentence();
            updatedContext.Display = dto.Display;
            updatedContext.SentenceId = dto.SentenceId;
            _context.DisplaySentences.Add(updatedContext);

                await _context.SaveChangesAsync();

                return new DisplaySentenceResponseDto(updatedContext);
        }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteDisplaySentence(int id)
            {
                var displaySentence = await _context.DisplaySentences.FindAsync(id);
                if (displaySentence == null)
                {
                    return NotFound();
                }

                _context.DisplaySentences.Remove(displaySentence);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool DisplaySentenceExists(int id)
            {
                return _context.DisplaySentences.Any(e => e.ID == id);
            }

        ///

        private DisplaySentence FindDisplaySentence(int id)
        {
            return _context.DisplaySentences
                  .Include(l => l.Sentence)
                .FirstOrDefault(x => x.ID == id);

        }
    }
}

