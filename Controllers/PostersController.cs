using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosterApi.Models;

namespace PosterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostersController : ControllerBase
    {
        private readonly PosterContext _context;

        public PostersController(PosterContext context)
        {
            _context = context;
        }

        // GET: api/Posters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poster>>> GetPosters()
        {
            return await _context.Posters.ToListAsync();
        }

        // GET: api/Posters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poster>> GetPoster(int id)
        {
            var poster = await _context.Posters.FindAsync(id);

            if (poster == null)
            {
                return NotFound();
            }

            return poster;
        }

        // PUT: api/Posters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoster(int id, Poster poster)
        {
            if (id != poster.Id)
            {
                return BadRequest();
            }

            _context.Entry(poster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosterExists(id))
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

        // POST: api/Posters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Poster>> PostPoster(Poster poster)
        {
            _context.Posters.Add(poster);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPoster), new { id = poster.Id }, poster);
        }

        // DELETE: api/Posters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoster(int id)
        {
            var poster = await _context.Posters.FindAsync(id);
            if (poster == null)
            {
                return NotFound();
            }

            _context.Posters.Remove(poster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PosterExists(int id)
        {
            return _context.Posters.Any(e => e.Id == id);
        }
    }
}
