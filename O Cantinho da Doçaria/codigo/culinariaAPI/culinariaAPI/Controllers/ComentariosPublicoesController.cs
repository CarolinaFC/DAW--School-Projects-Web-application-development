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
    public class ComentariosPublicoesController : ControllerBase
    {
        private readonly culinariaApiDBContext _context;

        public ComentariosPublicoesController(culinariaApiDBContext context)
        {
            _context = context;
        }

        // GET: api/ComentariosPublicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComentariosPublico>>> GetComentariosPublicos()
        {
            return await _context.ComentariosPublicos.ToListAsync();
        }

        // GET: api/ComentariosPublicoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComentariosPublico>> GetComentariosPublico(int id)
        {
            var comentariosPublico =  _context.ComentariosPublicos
                                    .Where(rec => rec.IdReceita == id).ToList();

            if (comentariosPublico == null)
            {
                return NotFound();
            }

            return Ok(comentariosPublico);
        }

        // PUT: api/ComentariosPublicoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentariosPublico(int id, ComentariosPublico comentariosPublico)
        {
            if (id != comentariosPublico.Id)
            {
                return BadRequest();
            }

            _context.Entry(comentariosPublico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentariosPublicoExists(id))
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

        // POST: api/ComentariosPublicoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComentariosPublico>> PostComentariosPublico(ComentariosPublico comentariosPublico)
        {
            _context.ComentariosPublicos.Add(comentariosPublico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentariosPublico", new { id = comentariosPublico.Id }, comentariosPublico);
        }

        // DELETE: api/ComentariosPublicoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentariosPublico(int id)
        {
            var comentariosPublico = await _context.ComentariosPublicos.FindAsync(id);
            if (comentariosPublico == null)
            {
                return NotFound();
            }

            _context.ComentariosPublicos.Remove(comentariosPublico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentariosPublicoExists(int id)
        {
            return _context.ComentariosPublicos.Any(e => e.Id == id);
        }
    }
}
