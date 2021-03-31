using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using culinariaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Newtonsoft.Json;

namespace culinariaMVC.Controllers
{
    public class MinhasReceitasController : Controller
    {
        private readonly CulinariaDBContext _context;

        public MinhasReceitasController(CulinariaDBContext context)
        {
            _context = context;
        }

        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44315/";
        List<Receita> ReceitaInfo = new List<Receita>();

        // GET: minhasReceitas
        public async Task<IActionResult> Index()
        {
            var culinariaDBContext = _context.MinhasReceitas
                .Include(m => m.id_leitorNavigation);

            return View(await culinariaDBContext.ToListAsync());
        }

        // GET: minhasReceitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Receitas/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var RecResponse = Res.Content.ReadAsStringAsync().Result;

                    ReceitaInfo = JsonConvert.DeserializeObject<List<Receita>>(RecResponse);
                }
                return View(ReceitaInfo[0]);
            }
        }

        public async Task<IActionResult> GuardarReceitaAsync(int id_receitaApi, String name_leitor)
        {
            var user = _context.Users
                .Where(u => u.email == name_leitor)
                .Select(u => u.Id_user)
                .FirstOrDefault();

            var leitor = _context.Leitors
                .Where(l => l.id_user == user)
                .Select(l => l.Id_leitor)
                .FirstOrDefault();

            MinhasReceita receita = new MinhasReceita();
            receita.id_receitaAPI = id_receitaApi;
            receita.id_leitor = leitor;

            _context.Add(receita);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "MinhasReceitas", new { id = receita.id_receitaAPI });
        }

        public async Task<IActionResult> GetCategoriaAsync()
        {
            List<Categoria> CategoriaInfo = new List<Categoria>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Categorias");

                if (Res.IsSuccessStatusCode)
                {
                    var CatResponse = Res.Content.ReadAsStringAsync().Result;

                    CategoriaInfo = JsonConvert.DeserializeObject<List<Categoria>>(CatResponse);
                }
                return new JsonResult(CategoriaInfo);
            }
        }

        public IActionResult GetComentariosPriv(int id_receita)
        {
            
            var minhasReceita = _context.MinhasReceitas
                .Where(m => m.id_receitaAPI == id_receita)
                .FirstOrDefault();

            var coment = _context.Comentarios_privados
                .Where(com => com.id_minhasReceitas == minhasReceita.Id_minhaReceita)
                .ToList();

            List<Comentarios_privado> desc_coment = new List<Comentarios_privado>();
            foreach (var c in coment) 
            {
                var desc = _context.Comentarios_privados
                    .Where(desc => desc.descricao == c.descricao)
                    .FirstOrDefault();

                desc_coment.Add(desc);
            }
            return new JsonResult(desc_coment);
        }

        public async Task<string> PostComentariosPrivAsync(String coment, int id_receita)
        {
            var id_minhaReceita = _context.MinhasReceitas
                .Where(m => m.id_receitaAPI == id_receita)
                .FirstOrDefault();

            Comentarios_privado rec = new Comentarios_privado();
            rec.id_minhasReceitas = id_minhaReceita.Id_minhaReceita;
            rec.descricao = coment;

            _context.Comentarios_privados.Add(rec);
            await _context.SaveChangesAsync();

            return coment;
        }

        // GET: minhasReceitas/Create
        public IActionResult Create()
        {
            ViewData["id_comentariosPrivados"] = new SelectList(_context.Comentarios_privados, "Id_comentariosPrivados", "Id_comentariosPrivados");
            ViewData["id_leitor"] = new SelectList(_context.Leitors, "Id_leitor", "Id_leitor");
            return View();
        }

        // POST: minhasReceitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_minhaReceita,id_receitaAPI,id_comentariosPrivados,id_leitor")] MinhasReceita minhasReceita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(minhasReceita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_leitor"] = new SelectList(_context.Leitors, "Id_leitor", "Id_leitor", minhasReceita.id_leitor);
            return View(minhasReceita);
        }

        // GET: minhasReceitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var minhasReceita = await _context.MinhasReceitas.FindAsync(id);
            if (minhasReceita == null)
            {
                return NotFound();
            }
            ViewData["id_leitor"] = new SelectList(_context.Leitors, "Id_leitor", "Id_leitor", minhasReceita.id_leitor);
            return View(minhasReceita);
        }

        // POST: minhasReceitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_minhaReceita,id_receitaAPI,id_comentariosPrivados,id_leitor")] MinhasReceita minhasReceita)
        {
            if (id != minhasReceita.Id_minhaReceita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(minhasReceita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!minhasReceitaExists(minhasReceita.Id_minhaReceita))
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
            ViewData["id_leitor"] = new SelectList(_context.Leitors, "Id_leitor", "Id_leitor", minhasReceita.id_leitor);
            return View(minhasReceita);
        }

        // GET: minhasReceitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = _context.MinhasReceitas.Where(r => r.id_receitaAPI == id).Select(r => r.Id_minhaReceita).FirstOrDefault();

            var minhasReceita = await _context.MinhasReceitas
                .Include(m => m.id_leitorNavigation)
                .FirstOrDefaultAsync(m => m.Id_minhaReceita == receita);

            if (minhasReceita == null)
            {
                return NotFound();
            }

            return View(minhasReceita);
        }

        // POST: minhasReceitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, String name_leitor)
        {
            var id_minhaReceita = _context.MinhasReceitas
                .Where(r => r.id_receitaAPI == id)
                .Select(r => r.Id_minhaReceita)
                .FirstOrDefault();

            var id_comentarios_priv = _context.Comentarios_privados
                .Where(com => com.id_minhasReceitas == id_minhaReceita)
                .ToList();

            foreach(var com in id_comentarios_priv)
            {                
                _context.Comentarios_privados.Remove(com);
                await _context.SaveChangesAsync();
            }
            
            var minhasReceita = await _context.MinhasReceitas.FindAsync(id_minhaReceita);
            _context.MinhasReceitas.Remove(minhasReceita);
            await _context.SaveChangesAsync();
            return RedirectToAction("Arquivos_Leitor", "Leitors", new { name_leitor = name_leitor });
        }

        private bool minhasReceitaExists(int id)
        {
            return _context.MinhasReceitas.Any(e => e.Id_minhaReceita == id);
        }
    }
}
