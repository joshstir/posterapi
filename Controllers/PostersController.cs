using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Licenses;
using PosterApi.DTO;
using PosterApi.Models;

namespace PosterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> PutPoster(int id, PosterDTO posterDTO)
        {
            if (id != posterDTO.Id)
            {
                return BadRequest();
            }

            //get the poster by Id
            var poster = await _context.Posters.FindAsync(id);

            // if the poster isn't found throw a 404
            if (poster == null)
            {
                return NotFound();
            }

            // update the poster with the new values
            poster.Title = posterDTO.Title;
            poster.Description = posterDTO.Description;            
            poster.ImageUrl = posterDTO.ImageUrl;

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
        public async Task<ActionResult<Poster>> PostPoster(PosterDTO posterDTO)
        {
            var poster = new Poster
            {
                Title = posterDTO.Title,
                Description = posterDTO.Description,
                ImageUrl = posterDTO.ImageUrl
            };

            _context.Posters.Add(poster);
            await _context.SaveChangesAsync();

            var returnPoster = posterDTO with {Id = poster.Id};        

            return CreatedAtAction(nameof(GetPoster), new { id = poster.Id }, returnPoster);
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

         // This is a helper action. It allows you to easily view all the claims of the token.
        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }

        private bool PosterExists(int id)
        {
            return _context.Posters.Any(e => e.Id == id);
        }
    }
}
