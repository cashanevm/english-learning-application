using System;
using System.Linq;
using english_learning_application.Controllers.Api.Dto;
using english_learning_application.Data;
using english_learning_application.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    [Route("api/translated/words")]
    [ApiController]
    public class TranslatedWordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TranslatedWordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TranslatedWords
        [HttpGet]
        public  IActionResult GetTranslatedWords()
        {
            var translatedWords = _context.TranslatedWords
                   .Include(l => l.Contexts)
                   .Include(l => l.Word)
                   .Include(l => l.Language)
                   .Include(l => l.SpeechPart)
                .ToList().Select(x => new TranslatedWordResponseDto(x));

            return Ok(translatedWords);
        }

        // GET: api/TranslatedWords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranslatedWord(int id)
        {
            var translatedWord = _context.TranslatedWords

                   .Include(l => l.Contexts)
                   .Include(l => l.Word)
                   .Include(l => l.Language)
                   .Include(l => l.SpeechPart)
                .FirstOrDefault(x => x.ID == id);

            if (translatedWord == null)
            {
                return NotFound();
            }

            return Ok(new TranslatedWordResponseDto(translatedWord));
        }

        // PUT: api/TranslatedWords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranslatedWord(int id, TranslatedWordRequestDto dto)
        {
            TranslatedWord translatedWord = new TranslatedWord();
            translatedWord.OwnerId = dto.OwnerId;
                translatedWord.WordId = dto.WordId;
            translatedWord.Translation = dto.Translation;
            translatedWord.LanguageId = dto.LanguageId;
            translatedWord.SpeechPartId = dto.SpeechPartId;
            translatedWord.ID = id;



            _context.Entry(translatedWord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                UpdateRelation(dto, translatedWord);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslatedWordExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/TranslatedWords
        [HttpPost]
        public async Task<IActionResult> PostTranslatedWord(TranslatedWordRequestDto dto)
        {
            TranslatedWord translatedWord = new TranslatedWord();
            translatedWord.OwnerId = dto.OwnerId;
            translatedWord.WordId = dto.WordId;
            translatedWord.Translation = dto.Translation;
            translatedWord.LanguageId = dto.LanguageId;
            translatedWord.SpeechPartId = dto.SpeechPartId;


            _context.TranslatedWords.Add(translatedWord);
            await _context.SaveChangesAsync();

            UpdateRelation(dto, translatedWord);


            return CreatedAtAction("GetTranslatedWord", new { id = translatedWord.ID }, new TranslatedWordResponseDto(translatedWord));
        }

        // DELETE: api/TranslatedWords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslatedWord(int id)
        {
            var translatedWord = await _context.TranslatedWords.FindAsync(id);
            if (translatedWord == null)
            {
                return NotFound();
            }

            _context.TranslatedWords.Remove(translatedWord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TranslatedWordExists(int id)
        {
            return _context.TranslatedWords.Any(e => e.ID == id);
        }

        private void Modify(TranslatedWord updatedContext)
        {
            _context.Entry(updatedContext).State = EntityState.Modified;
            _context.TranslatedWords.Update(updatedContext);

            _context.SaveChanges();
        }

        private void UpdateRelation(TranslatedWordRequestDto dto, TranslatedWord updatedContext)
        {
            TranslatedWord oldContext = _context.TranslatedWords

                   .Include(l => l.Contexts)
                   .Include(l => l.Word)
                   .Include(l => l.Language)
                   .Include(l => l.SpeechPart)
                .FirstOrDefault(x => x.ID == updatedContext.ID);

            oldContext.Contexts.ForEach(translatedSentences =>
            {
                translatedSentences.TranslatedWords.Remove(oldContext);
            });

            Modify(updatedContext);

            dto.ContextIds.ForEach(translatedSentenceId =>
            {
                Context newTranslatedSentence =
                _context
                .Contexts
                .Include(t => t.TranslatedWords)
                .FirstOrDefault(x => x.ID == translatedSentenceId);

                if (newTranslatedSentence != null)
                {
                    newTranslatedSentence.TranslatedWords.Add(updatedContext);
                    updatedContext.Contexts.Add(newTranslatedSentence);
                }
            }
               );

            Modify(updatedContext);
        }
    }
}

