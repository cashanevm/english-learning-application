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
    [Route("api/words")]
    public class WordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var words = _context.Words
                .Include(l => l.Tags)
                .Include(l => l.DisplayWords)
                .Include(l => l.Sentences)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.TranslatedWords)
                .Include(l => l.Tests)
                .ToList().Select(x => new WordResponseDto(x)); ;

            return Ok(words);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var word = _context.Words

                        .Include(l => l.Tags)
                .Include(l => l.DisplayWords)
                .Include(l => l.Sentences)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.TranslatedWords)
                .Include(l => l.Tests)
                .FirstOrDefault(x => x.ID == id);
            if (word == null)
            {
                return NotFound();
            }
            return Ok(new WordResponseDto(word));
        }

        [HttpPost]
        public async Task<IActionResult> Create(WordRequestDto dto)


        {
            Word word = new Word();
            word.OriginalWord = dto.OriginalWord;

            _context.Words.Add(word);
            await _context.SaveChangesAsync();

            UpdateRelation(dto, word);

            return CreatedAtAction(nameof(GetById), new { id = word.ID }, new WordResponseDto(word));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WordRequestDto dto)
        {
            Word word = new Word();
            word.OriginalWord = dto.OriginalWord;
            word.ID = id;

            _context.Entry(word).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                UpdateRelation(dto, word);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Words.Any(w => w.ID == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var word = await _context.Words.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }
            _context.Words.Remove(word);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private void Modify(Word updatedContext)
        {
            _context.Entry(updatedContext).State = EntityState.Modified;
            _context.Words.Update(updatedContext);

            _context.SaveChanges();
        }

        private void UpdateRelation(WordRequestDto dto, Word updatedContext)
        {
            Word oldContext = _context.Words

                        .Include(l => l.Tags)
                .Include(l => l.DisplayWords)
                .Include(l => l.Sentences)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.TranslatedWords)
                .Include(l => l.Tests)
                .FirstOrDefault(x => x.ID == updatedContext.ID);

            oldContext.Tags.ForEach(translatedSentences =>
            {
                translatedSentences.Words.Remove(oldContext);
            });

            Modify(updatedContext);

            dto.TagIds.ForEach(translatedSentenceId =>
            {
                Tag newTranslatedSentence =
                _context
                .Tags
                .Include(t => t.Words)
                .FirstOrDefault(x => x.ID == translatedSentenceId);

                if (newTranslatedSentence != null)
                {
                    newTranslatedSentence.Words.Add(updatedContext);
                    updatedContext.Tags.Add(newTranslatedSentence);
                }
            }
               );

            Modify(updatedContext);
        }
    }

}