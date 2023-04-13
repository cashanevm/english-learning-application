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
    [Route("api/speech-parts")]
    public class SpeechPartApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SpeechPartApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /SpeechPart
        [HttpGet]
        public IEnumerable<SpeechPartResponseDto> GetSpeechParts()
        {
            return _context.SpeechParts
                .Include(l => l.TranslatedWords)
                .ToList().Select(x => new SpeechPartResponseDto(x)); ;

        }

        // GET: /SpeechPart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpeechPartResponseDto>> GetSpeechPart(int id)
        {
            var speechPart = _context.SpeechParts
                .Include(l => l.TranslatedWords)
                .FirstOrDefault(x => x.ID == id);

            if (speechPart == null)
            {
                return NotFound();
            }

            return new SpeechPartResponseDto(speechPart);
        }

        // PUT: /SpeechPart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeechPart(int id, SpeechPartRequestDto dto)
        {
            SpeechPart speechPart = new SpeechPart();
            speechPart.ID = id;
            speechPart.Name = dto.Name;

            _context.Entry(speechPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeechPartExists(id))
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

        // POST: /SpeechPart
        [HttpPost]
        public async Task<ActionResult<SpeechPartResponseDto>> PostSpeechPart(SpeechPartRequestDto dto)
        {
            SpeechPart speechPart = new SpeechPart();
            speechPart.Name = dto.Name;

            _context.SpeechParts.Add(speechPart);
            await _context.SaveChangesAsync();

            return new SpeechPartResponseDto(speechPart);
        }

        // DELETE: /SpeechPart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeechPart(int id)
        {
            var speechPart = await _context.SpeechParts.FindAsync(id);
            if (speechPart == null)
            {
                return NotFound();
            }

            _context.SpeechParts.Remove(speechPart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpeechPartExists(int id)
        {
            return _context.SpeechParts.Any(e => e.ID == id);
        }
    }

}

