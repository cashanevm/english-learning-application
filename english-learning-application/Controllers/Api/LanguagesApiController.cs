using System;
using System.Linq;
using english_learning_application.Controllers.Dto;
using english_learning_application.Data;
using english_learning_application.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    [ApiController]
    [Route("api/languages")]
    public class LanguagesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LanguagesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<LanguageResponseDto> GetLanguages()
        {
            return _context.Languages
                .Include(l => l.TranslatedWords)
                .Include(l => l.Tests)
                .Include(l => l.TranslatedSentences)
                .ToList().Select(x => new LanguageResponseDto(x));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageResponseDto>> GetLanguage(int id)
        {
            var language = _context.Languages
                    .Include(l => l.TranslatedWords)
                .Include(l => l.Tests)
                .Include(l => l.TranslatedSentences)
                .FirstOrDefault(x => x.ID == id); 

            if (language == null)
            {
                return NotFound();
            }

            return new LanguageResponseDto(language);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguage(int id, LanguageRequestDto dto)
        {
            Language language = new Language();
            language.ID = id;
            language.Key = dto.Key;

            _context.Entry(language).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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
        public async Task<ActionResult<LanguageResponseDto>> PostLanguage(LanguageRequestDto dto)
        {
            Language language = new Language();
          
            language.Key = dto.Key;

            _context.Languages.Add(language);

            await _context.SaveChangesAsync();

            return new LanguageResponseDto(language);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.ID == id);
        }

      
    
}

}

