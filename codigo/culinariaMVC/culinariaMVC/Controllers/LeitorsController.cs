using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using culinariaMVC.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace culinariaMVC.Controllers
{
    public class LeitorsController : Controller
    {
        private readonly CulinariaDBContext _context;

        public LeitorsController(CulinariaDBContext context)
        {
            _context = context;
        }

        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44315/";

        // GET: leitors
        public async Task<IActionResult> Index()
        {
            var culinariaDBContext = _context.Leitors.Include(l => l.id_userNavigation);
            return View(await culinariaDBContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Arquivos_LeitorAsync(String name_leitor)
        {
            List<Receita> ReceitaInfo = new List<Receita>();

            var id_user = _context.Users
                .Where(u => u.email == name_leitor)
                .Select(u => u.Id_user)
                .FirstOrDefault();

            var id_leitor = _context.Leitors
                .Where(l => l.id_user == id_user)
                .Select(l => l.Id_leitor)
                .FirstOrDefault();

            var minhasReceitas = _context.MinhasReceitas.Where(i => i.id_leitor == id_leitor).ToList();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Fazer um for para percorrer o id_receitaAPi
                for (int i = 0; i < minhasReceitas.Count(); i++)
                {                    
                    HttpResponseMessage Res = await client.GetAsync($"api/Receitas/{minhasReceitas[i].id_receitaAPI}");
                    if (Res.IsSuccessStatusCode)
                    {
                        var RecResponse = Res.Content.ReadAsStringAsync().Result;

                        ReceitaInfo.Add(JsonConvert.DeserializeObject<List<Receita>>(RecResponse)[0]);
                    }
                }
                return View(ReceitaInfo);
            }
        }

        [Authorize]
        public async Task<IActionResult> HomeAsync()
        {
            List<Receita> ReceitaInfo = new List<Receita>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Receitas");

                if (Res.IsSuccessStatusCode)
                {
                    var RecResponse = Res.Content.ReadAsStringAsync().Result;
                    ReceitaInfo = JsonConvert.DeserializeObject<List<Receita>>(RecResponse);
                }
                return View(ReceitaInfo);
            }
        }

        public async Task<IActionResult> GetReceitaAsync()
        {
            List<Receita> ReceitaInfo = new List<Receita>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Receitas");

                if (Res.IsSuccessStatusCode)
                {
                    var RecResponse = Res.Content.ReadAsStringAsync().Result;
                    ReceitaInfo = JsonConvert.DeserializeObject<List<Receita>>(RecResponse);
                }
                return new JsonResult(ReceitaInfo);
            }
        }

        public async Task<IActionResult> GetCategoriaAsync()
        {
            List<Categoria> CategoriaInfo = new List<Categoria>();

            using (var client = new HttpClient())
            {
                // Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                // Define request data format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Sending request to find webapi REST service resource GetReceita using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Categorias");

                // Cheking the response is successful or not wich is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    // Storing the response details recieved from web api
                    var CatResponse = Res.Content.ReadAsStringAsync().Result;

                    // Deserializing the response received from web api and storing into the Receita list
                    CategoriaInfo = JsonConvert.DeserializeObject<List<Categoria>>(CatResponse);

                }
                // returning the receita list to view

                ViewBag.Categoria = CategoriaInfo;

                return new JsonResult(CategoriaInfo);
            }
        }

        public async Task<IActionResult> GetReceitaCategoriaFilterAsync(int id_cate, String name_leitor)
        {
            List<Receita> ReceitaInfo = new List<Receita>();

            var id_user = _context.Users
                .Where(u => u.email == name_leitor)
                .Select(u => u.Id_user)
                .FirstOrDefault();

            var id_leitor = _context.Leitors
                .Where(l => l.id_user == id_user)
                .Select(l => l.Id_leitor)
                .FirstOrDefault();

            //Todas as receitas guardadas do leitor 
            var minhasReceitas = _context.MinhasReceitas.Where(i => i.id_leitor == id_leitor).ToList();

            //Vai Buscar todas as receitas de acordo com a categoria
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Receitas/GetReceitaCategoriaFilters/{id_cate}");

                List<Receita> rec = new List<Receita>();

                //Fazer um for para percorrer as minhas receitas
                for (int i = 0; i < minhasReceitas.Count(); i++)
                {
                    if (Res.IsSuccessStatusCode)
                    {
                        var RecResponse = Res.Content.ReadAsStringAsync().Result;

                        ReceitaInfo = JsonConvert.DeserializeObject<List<Receita>>(RecResponse);

                        for (int j = 0; j < ReceitaInfo.Count(); j++)
                        {
                            if (minhasReceitas[i].id_receitaAPI ==  ReceitaInfo[j].Id)
                            {
                                //Todas as receitas guardadas de acordo com a categoria
                                rec.Add(ReceitaInfo.Where(r => r.Id == minhasReceitas[i].id_receitaAPI).FirstOrDefault());
                            }
                        }                        
                    }
                }                
                return new JsonResult(rec);
            }
        }

        public async Task<IActionResult> GetReceitaCategoryFilter_ArquivosAsync(String name_leitor)
        {
            List<Receita> ReceitaInfo = new List<Receita>();

            var id_user = _context.Users
                .Where(u => u.email == name_leitor)
                .Select(u => u.Id_user)
                .FirstOrDefault();

            var id_leitor = _context.Leitors
                .Where(l => l.id_user == id_user)
                .Select(l => l.Id_leitor)
                .FirstOrDefault();

            var minhasReceitas = _context.MinhasReceitas.Where(i => i.id_leitor == id_leitor).ToList();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Fazer um for para percorrer o id_receitaAPi
                for (int i = 0; i < minhasReceitas.Count(); i++)
                {
                    HttpResponseMessage Res = await client.GetAsync($"api/Receitas/{minhasReceitas[i].id_receitaAPI}");
                    if (Res.IsSuccessStatusCode)
                    {
                        var RecResponse = Res.Content.ReadAsStringAsync().Result;

                        ReceitaInfo.Add(JsonConvert.DeserializeObject<List<Receita>>(RecResponse)[0]);
                    }
                }
                return new JsonResult(ReceitaInfo);
            }
        }

        // GET: leitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leitor = await _context.Leitors
                .Include(l => l.id_userNavigation)
                .FirstOrDefaultAsync(m => m.Id_leitor == id);
            if (leitor == null)
            {
                return NotFound();
            }

            return View(leitor);
        }

        // GET: leitors/Create
        public IActionResult Create()
        {
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user");
            return View();
        }

        // POST: leitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_leitor,nome,id_user")] Leitor leitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user", leitor.id_user);
            return View(leitor);
        }

        // GET: leitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leitor = await _context.Leitors.FindAsync(id);
            if (leitor == null)
            {
                return NotFound();
            }
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user", leitor.id_user);
            return View(leitor);
        }

        // POST: leitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_leitor,nome,id_user")] Leitor leitor)
        {
            if (id != leitor.Id_leitor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!leitorExists(leitor.Id_leitor))
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
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user", leitor.id_user);
            return View(leitor);
        }

        // GET: leitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leitor = await _context.Leitors
                .Include(l => l.id_userNavigation)
                .FirstOrDefaultAsync(m => m.Id_leitor == id);
            if (leitor == null)
            {
                return NotFound();
            }

            return View(leitor);
        }

        // POST: leitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leitor = await _context.Leitors.FindAsync(id);
            _context.Leitors.Remove(leitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool leitorExists(int id)
        {
            return _context.Leitors.Any(e => e.Id_leitor == id);
        }
    }
}
