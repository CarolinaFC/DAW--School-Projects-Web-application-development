using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using culinariaAPI.Models;

namespace culinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacoesController : ControllerBase
    {
        private readonly culinariaApiDBContext _context;

        public AvaliacoesController(culinariaApiDBContext context)
        {
            _context = context;
        }

        // GET: api/Avaliacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avaliaco>>> GetAvaliacoes()
        {
            return await _context.Avaliacoes.ToListAsync();
        }

        // GET: api/Avaliacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliaco>> GetAvaliaco(int id)
        {
            var avaliaco = _context.Avaliacoes.Where(av => av.IdReceita == id).ToList();

            if (avaliaco == null)
            {
                return NotFound();
            }

            return Ok(avaliaco);
        }


        // PUT: api/Avaliacoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvaliaco(int id, Avaliaco avaliaco)
        {
            if (id != avaliaco.Id)
            {
                return BadRequest();
            }

            _context.Entry(avaliaco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvaliacoExists(id))
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

        // POST: api/Avaliacoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Avaliaco>> PostAvaliaco(Avaliaco avaliaco)
        {
            _context.Avaliacoes.Add(avaliaco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvaliaco", new { id = avaliaco.Id }, avaliaco);
        }

        // DELETE: api/Avaliacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvaliaco(int id)
        {
            var avaliaco = await _context.Avaliacoes.FindAsync(id);
            if (avaliaco == null)
            {
                return NotFound();
            }

            _context.Avaliacoes.Remove(avaliaco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AvaliacoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
