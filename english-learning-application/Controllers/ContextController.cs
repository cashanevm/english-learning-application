using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using english_learning_application.Controllers.Dto;
using english_learning_application.Data;
using english_learning_application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    [Route("api/contexts")]
    public class ContextController : ControllerBase
    {
        private readonly ILogger<ContextController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public ContextController(
            ILogger<ContextController> logger,
            ApplicationDbContext applicationDbContext
            )
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IEnumerable<ContextResponseDto> GetContexts()
        {
            return _applicationDbContext.Contexts
                .Include(l => l.TranslatedWords)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.Sentences)
                .ToList().Select(x => new ContextResponseDto(x));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Context context = GetContext(id);

            if (context == null) {
                return NotFound();
            }


            return Ok(
                new ContextResponseDto(context)
                ) ;
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody] ContextRequestDto post)
        {
            try
            {
                Context context = new Context();
                context.Name = post.Name;

                _applicationDbContext.Contexts.Add(context);
                _applicationDbContext.SaveChanges();

                UpdateRelation(post, context);

                return Ok(
                    new ContextResponseDto(
                        GetContext(context.ID)
                    ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContext([FromRoute] int id, [FromBody] ContextRequestDto context)
        {
            try
            {
                Save(context, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContextExists(id))
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
        public async Task<IActionResult> DeleteContext(int id)
        {
            var context = await _applicationDbContext.Contexts.FindAsync(id);
            if (context == null)
            {
                return NotFound();
            }

            _applicationDbContext.Contexts.Remove(context);
            await _applicationDbContext.SaveChangesAsync();
            //to-do handle returned list 

            return NoContent();
        }

        private bool ContextExists(int id)
        {
            return _applicationDbContext.Contexts.Any(e => e.ID == id);
        }

        private Context GetContext(int id)
        {
            return _applicationDbContext.Contexts
              .Include(l => l.TranslatedWords)
            .Include(l => l.TranslatedSentences)
            .Include(l => l.Sentences)
            .FirstOrDefault(x => x.ID == id);
        }

        private void Save(ContextRequestDto dto, int id)
        {
            Context updatedContext = new Context();
            updatedContext.Name = dto.Name;
            updatedContext.ID = id;

            Modify(updatedContext);

            UpdateRelation(dto, updatedContext);
        }

        private void Modify(Context updatedContext)
        {
            _applicationDbContext.Entry(updatedContext).State = EntityState.Modified;
            _applicationDbContext.Contexts.Update(updatedContext);

            _applicationDbContext.SaveChanges();
        }

        private void UpdateRelation(ContextRequestDto dto, Context updatedContext)
        {
            Context oldContext = GetContext(updatedContext.ID);

            oldContext.TranslatedSentences.ForEach(translatedSentences =>
            {
                translatedSentences.Contexts.Remove(oldContext);
            });

            oldContext.TranslatedWords.ForEach(TranslatedWords =>
            {
                TranslatedWords.Contexts.Remove(oldContext);
            });

            oldContext.Sentences.ForEach(Sentences =>
            {
                Sentences.Contexts.Remove(oldContext);
            });

            Modify(updatedContext);

            dto.TranslatedSentenceIds.ForEach(translatedSentenceId => {
                TranslatedSentence newTranslatedSentence =
                _applicationDbContext
                .TranslatedSentences
                .Include(t => t.Contexts)
                .FirstOrDefault(x => x.ID == translatedSentenceId);

                if (newTranslatedSentence != null)
                {
                    newTranslatedSentence.Contexts.Add(updatedContext);
                    updatedContext.TranslatedSentences.Add(newTranslatedSentence);
                }
            }
               );

            dto.TranslatedWordIds.ForEach(translatedWordId =>
            {
                TranslatedWord newTranslatedWord =
                _applicationDbContext
                .TranslatedWords
                .Include(t => t.Contexts)
                .FirstOrDefault(x => x.ID == translatedWordId);

                if (newTranslatedWord != null)
                {
                    newTranslatedWord.Contexts.Add(updatedContext);
                    updatedContext.TranslatedWords.Add(newTranslatedWord);
                }
            }
           );

            dto.SentenceIds.ForEach(sentenceId =>
            {
                Sentence newSentence =
                _applicationDbContext
                .Sentences
                .Include(t => t.Contexts)
                .FirstOrDefault(x => x.ID == sentenceId);

                if (newSentence != null)
                {
                    newSentence.Contexts.Add(updatedContext);
                    updatedContext.Sentences.Add(newSentence);
                }
            }
           );

            Modify(updatedContext);
        }
    }
}

