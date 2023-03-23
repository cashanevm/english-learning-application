﻿using System;
using System.Linq;
using english_learning_application.Controllers.Dto;
using english_learning_application.Data;
using english_learning_application.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers
{
    [Route("api/sentences")]
    [ApiController]
    public class SentenceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SentenceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sentence
        [HttpGet]
        public  IEnumerable<SentenceResponseDto> GetSentences()
        {
            return  _context.Sentences
                .Include(l => l.Contexts)
                .Include(l => l.DisplaySentences)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.Word)
                .ToList()
                .Select(x => new SentenceResponseDto(x));
        }

        // GET: api/Sentence/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SentenceResponseDto>> GetSentence(int id)
        {
            var sentence =  _context.Sentences.Include(l => l.Contexts)
                .Include(l => l.DisplaySentences)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.Word)
                .FirstOrDefault(x => x.ID == id);

            if (sentence == null)
            {
                return NotFound();
            }

            return new SentenceResponseDto(sentence);
        }

        // PUT: api/Sentence/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSentence(int id, SentenceRequestDto dto)
        {
            Sentence sentence = new Sentence();
            sentence.OwnerId = dto.OwnerId;
            sentence.WordId = dto.WordId;
            sentence.OriginalSentence = dto.OriginalSentence;
            sentence.ID = id;

            _context.Entry(sentence).State = EntityState.Modified;

            try
            {
               
                 await _context.SaveChangesAsync();

                UpdateRelation(dto, sentence);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SentenceExists(id))
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

        // POST: api/Sentence
        [HttpPost]
        public async Task<ActionResult<Sentence>> PostSentence(SentenceRequestDto dto)
        {
            Sentence sentence = new Sentence();
            sentence.OwnerId = dto.OwnerId;
            sentence.WordId = dto.WordId;
            sentence.OriginalSentence = dto.OriginalSentence;

            _context.Sentences.Add(sentence);

            await _context.SaveChangesAsync();

            UpdateRelation(dto, sentence);

            return CreatedAtAction("GetSentence", new { id = sentence.ID }, sentence);
        }

        // DELETE: api/Sentence/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSentence(int id)
        {
            var sentence = await _context.Sentences.FindAsync(id);
            if (sentence == null)
            {
                return NotFound();
            }

            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(e => e.ID == id);
        }


        private void Modify(Sentence updatedSentence)
        {
            _context.Entry(updatedSentence).State = EntityState.Modified;
            _context.Sentences.Update(updatedSentence);

            _context.SaveChanges();
        }

        private void UpdateRelation(SentenceRequestDto dto, Sentence updatedSentence)
        {
            Sentence oldSentence = _context.Sentences.Include(l => l.Contexts)
                .Include(l => l.DisplaySentences)
                .Include(l => l.TranslatedSentences)
                .Include(l => l.Word)
                .FirstOrDefault(x => x.ID == updatedSentence.ID);

            oldSentence.Contexts.ForEach(translatedSentences =>
            {
                translatedSentences.Sentences.Remove(oldSentence);
            });

            Modify(updatedSentence);

            dto.ContextIds.ForEach(translatedSentenceId =>
            {
                Context newTranslatedSentence =
                _context
                .Contexts
                .Include(t => t.Sentences)
                .FirstOrDefault(x => x.ID == translatedSentenceId);

                if (newTranslatedSentence != null)
                {
                    newTranslatedSentence.Sentences.Add(updatedSentence);
                    updatedSentence.Contexts.Add(newTranslatedSentence);
                }
            }
               );

            Modify(updatedSentence);
        }
    }

}

