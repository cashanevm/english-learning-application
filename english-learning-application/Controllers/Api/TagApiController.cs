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
    [Route("api/tags")]
    [ApiController]
    public class TagApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TagApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tag
        [HttpGet]
        public IEnumerable<TagResponseDto> GetTags()
        {
            return _context.Tags
                .Include(l => l.Words)
                .ToList().Select(x => new TagResponseDto(x));
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagResponseDto>> GetTag(int id)
        {
            var tag = _context.Tags
                .Include(l => l.Words)
                .FirstOrDefault(x => x.ID == id); ;

            if (tag == null)
            {
                return NotFound();
            }

            return new TagResponseDto(tag);
        }

        // PUT: api/Tag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, TagRequestDto dto)
        {
            Tag tag = new Tag();

            tag.Name = dto.Name;
            tag.ID = id;

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tag
        [HttpPost]
        public async Task<ActionResult<TagResponseDto>> PostTag(TagRequestDto dto)
        {

            Tag tag = new Tag();

            tag.Name = dto.Name;
       
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return  CreatedAtAction("GetTag", new { id = tag.ID }, new TagResponseDto(tag));
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.ID == id);
        }
    }
}

