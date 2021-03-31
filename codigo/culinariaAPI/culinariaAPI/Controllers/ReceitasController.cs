using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using culinariaAPI.Models;
using System.IO;

namespace culinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        private readonly culinariaApiDBContext _context;

        public ReceitasController(culinariaApiDBContext context)
        {
            _context = context;
        }

        // GET: api/Receitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitas()
        {
            return await _context.Receitas.ToListAsync();
        }

        // GET: api/Receitas/5
        /*[HttpGet("getReceitaDetails/{id}")]  
        public async Task<ActionResult<Receita>> GetReceitaDetails(int id)
        {
            var receita = await _context.Receitas
                    .Include(rec => rec.Avaliacos)
                    .Include(rec => rec.ComentariosPublicos)
                    .Where(rec => rec.Id == id).FirstOrDefaultAsync();

            if (receita == null)
            {
                return NotFound();
            }

            return receita;
        }*/

        // GET: api/Receitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> GetReceita(int id)
        {
            var receita =  _context.Receitas
                    .Include(rec => rec.Avaliacos)
                    .Include(rec => rec.ComentariosPublicos)
                    .Where(rec => rec.Id == id)
                    .ToList();

            if (receita == null)
            {
                return NotFound();
            }

            return Ok(receita);
        }

        // GET: api/Receitas/GetReceitaFilter/ filtar Receitas por categoria, grau, custo, tempo
        [HttpGet("GetReceitaFilters/{id_cate}/{grau}/{custo}/{tempo}")]
        public async Task<IActionResult> GetReceitaFiltersAsync(int id_cate, String grau, String custo, String tempo)
        {
            var receita = _context.Receitas
                .Where(rec => rec.IdCategoria == id_cate)
                .Where(rec => rec.GrauDificuldade == grau)
                .Where(rec => rec.CustoRefeicao == custo)
                .Where(rec => rec.TempoPreparacao == tempo)
                .ToList();

            if (receita == null)
            {
                return NotFound();
            }

            return Ok(receita);
            
        }

        // GET: api/Receitas/GetReceitaCategoriaFilter/ filtar Receitas por categoria
        [HttpGet("GetReceitaCategoriaFilters/{id_cate}")]
        public async Task<IActionResult> GetReceitaCategoriaFiltersAsync(int id_cate)
        {
            var receita = _context.Receitas
                .Where(rec => rec.IdCategoria == id_cate)
                .ToList();

            if (receita == null)
            {
                return NotFound();
            }

            return Ok(receita);
        }

        // PUT: api/Receitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceita(int id, Receita receita)
        {
            if (id != receita.Id)
            {
                return BadRequest();
            }

            /*var rece = _context.Receitas
                    .Where(rec => rec.ImgReceita == receita.ImgReceita)
                    .ToList();*/

            _context.Entry(receita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceitaExists(id))
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

        // POST: api/Receitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receita>> PostReceita(Receita receita)
        {
            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceita", new { id = receita.Id }, receita);
        }

        // DELETE: api/Receitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceita(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null)
            {
                return NotFound();
            }

            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceitaExists(int id)
        {
            return _context.Receitas.Any(e => e.Id == id);
        }
    }
}
