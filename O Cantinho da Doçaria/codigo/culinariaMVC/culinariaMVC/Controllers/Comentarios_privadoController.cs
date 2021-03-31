using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using culinariaMVC.Models;

namespace culinariaMVC.Controllers
{
    public class Comentarios_privadoController : Controller
    {
        private readonly CulinariaDBContext _context;

        public Comentarios_privadoController(CulinariaDBContext context)
        {
            _context = context;
        }

        // GET: comentarios_privado
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comentarios_privados.ToListAsync());
        }

        // GET: comentarios_privado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentarios_privado = await _context.Comentarios_privados
                .FirstOrDefaultAsync(m => m.Id_comentariosPrivados == id);
            if (comentarios_privado == null)
            {
                return NotFound();
            }

            return View(comentarios_privado);
        }

        // GET: comentarios_privado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: comentarios_privado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_comentariosPrivados,descricao,id_minhasReceitas")] Comentarios_privado comentarios_privado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentarios_privado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_minhasReceitas"] = new SelectList(_context.MinhasReceitas, "Id_minhaReceita", "Id_minhaReceita", comentarios_privado.id_minhasReceitas);
            return View(comentarios_privado);
        }

        // GET: comentarios_privado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentarios_privado = await _context.Comentarios_privados.FindAsync(id);
            if (comentarios_privado == null)
            {
                return NotFound();
            }
            return View(comentarios_privado);
        }

        // POST: comentarios_privado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_comentariosPrivados,descricao,id_minhasReceitas")] Comentarios_privado comentarios_privado)
        {
            if (id != comentarios_privado.Id_comentariosPrivados)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentarios_privado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!comentarios_privadoExists(comentarios_privado.Id_comentariosPrivados))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_minhasReceitas"] = new SelectList(_context.MinhasReceitas, "Id_minhaReceita", "Id_minhaReceita", comentarios_privado.id_minhasReceitas);
            return View(comentarios_privado);
        }

        // GET: comentarios_privado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentarios_privado = await _context.Comentarios_privados
                .FirstOrDefaultAsync(m => m.Id_comentariosPrivados == id);
            if (comentarios_privado == null)
            {
                return NotFound();
            }

            return View(comentarios_privado);
        }

        // POST: comentarios_privado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comentarios_privado = await _context.Comentarios_privados.FindAsync(id);
            _context.Comentarios_privados.Remove(comentarios_privado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool comentarios_privadoExists(int id)
        {
            return _context.Comentarios_privados.Any(e => e.Id_comentariosPrivados == id);
        }
    }
}
