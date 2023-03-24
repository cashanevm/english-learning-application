
using System;
using System.Linq;
using english_learning_application.Controllers.Dto;
using english_learning_application.Data;
using english_learning_application.Models;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    [ApiController]
    [Route("api/translated/sentences")]
    public class TranslatedSentencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TranslatedSentencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TranslatedSentences
        [HttpGet]
        public IEnumerable<TranslatedSentenceResponseDto> GetTranslatedSentences()
        {
            return _context.TranslatedSentences
                .Include(l => l.Contexts)
                .Include(l => l.Word)
                .Include(l => l.Sentence)
                .Include(l => l.Language)
                .ToList().Select(x => new TranslatedSentenceResponseDto(x));
        }

        // GET: api/TranslatedSentences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TranslatedSentenceResponseDto>> GetTranslatedSentence(int id)
        {
            var translatedSentence = _context.TranslatedSentences
                .Include(l => l.Contexts)
                .Include(l => l.Word)
                .Include(l => l.Sentence)
                .Include(l => l.Language)
                .FirstOrDefault(x => x.ID == id);

            if (translatedSentence == null)
            {
                return NotFound();
            }

            return new TranslatedSentenceResponseDto(translatedSentence);
        }

        // PUT: api/TranslatedSentences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranslatedSentence(int id, TranslatedSentenceRequestDto dto)
        {
            TranslatedSentence translatedSentence = new TranslatedSentence();
            translatedSentence.OwnerId = dto.OwnerId;
            translatedSentence.WordId = dto.WordId;
            translatedSentence.Translation = dto.Translation;
            translatedSentence.LanguageId = dto.LanguageId;
            translatedSentence.SentenceId = dto.SentenceId;
            translatedSentence.ID = id;

            _context.Entry(translatedSentence).State = EntityState.Modified;

            UpdateRelation(dto, translatedSentence);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslatedSentenceExists(id))
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


        // POST: api/TranslatedWords
        [HttpPost]
        public async Task<IActionResult> PostTranslatedWord(TranslatedSentenceRequestDto dto)
        {

            TranslatedSentence translatedSentence = new TranslatedSentence();
            translatedSentence.OwnerId = dto.OwnerId;
            translatedSentence.WordId = dto.WordId;
            translatedSentence.Translation = dto.Translation;
            translatedSentence.LanguageId = dto.LanguageId;
            translatedSentence.SentenceId = dto.SentenceId;
       

            _context.TranslatedSentences.Add(translatedSentence);
            await _context.SaveChangesAsync();

            UpdateRelation(dto, translatedSentence);

            return CreatedAtAction("GetTranslatedWord", new { id = translatedSentence.ID }, translatedSentence);
        }

        // DELETE: api/TranslatedWords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslatedWord(int id)
        {
            var translatedSentence = await _context.TranslatedSentences.FindAsync(id);
            if (translatedSentence == null)
            {
                return NotFound();
            }

            _context.TranslatedSentences.Remove(translatedSentence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TranslatedSentenceExists(int id)
        {
            return _context.TranslatedSentences.Any(e => e.ID == id);
        }

        private void Modify(TranslatedSentence updatedContext)
        {
            _context.Entry(updatedContext).State = EntityState.Modified;
            _context.TranslatedSentences.Update(updatedContext);

            _context.SaveChanges();
        }

        private void UpdateRelation(TranslatedSentenceRequestDto dto, TranslatedSentence updatedContext)
        {
            TranslatedSentence oldContext = _context.TranslatedSentences
                .Include(l => l.Contexts)
                .Include(l => l.Word)
                .Include(l => l.Sentence)
                .Include(l => l.Language)
                .FirstOrDefault(x => x.ID == updatedContext.ID);

            oldContext.Contexts.ForEach(translatedSentences =>
            {
                translatedSentences.TranslatedSentences.Remove(oldContext);
            });

            Modify(updatedContext);

            dto.ContextIds.ForEach(translatedSentenceId =>
            {
                Context newTranslatedSentence =
                _context
                .Contexts
                .Include(t => t.TranslatedSentences)
                .FirstOrDefault(x => x.ID == translatedSentenceId);

                if (newTranslatedSentence != null)
                {
                    newTranslatedSentence.TranslatedSentences.Add(updatedContext);
                    updatedContext.Contexts.Add(newTranslatedSentence);
                }
            }
               );

            Modify(updatedContext);
        }
    }
}

