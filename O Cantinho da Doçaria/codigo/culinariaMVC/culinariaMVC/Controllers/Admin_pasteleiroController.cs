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
    public class Admin_pasteleiroController : Controller
    {
        private readonly CulinariaDBContext _context;

        public Admin_pasteleiroController(CulinariaDBContext context)
        {
            _context = context;
        }

        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44315/";

        // GET: admin_pasteleiro
        public async Task<IActionResult> Index()
        {
            var culinariaDBContext = _context.Admin_pasteleiros.Include(a => a.id_userNavigation);
            return View(await culinariaDBContext.ToListAsync());
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

        [Authorize]
        public async Task<IActionResult> Arquivos_AdminAsync(String name_admin)
        {
            List<Receita> ReceitaInfo = new List<Receita>();
            // vai buscar todas as receitas à api
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
            }

            var id_user = _context.Users
                .Where(u => u.email == name_admin)
                .FirstOrDefault();

            var id_admin = _context.Admin_pasteleiros
                .Where(a => a.id_user == id_user.Id_user)
                .FirstOrDefault();

            List<Receita> receitas = new List<Receita>();

            //Todas as receitas do admin especifico 
            foreach (var rec in ReceitaInfo)
            {
                if (rec.IdAdmin == id_admin.Id_admin)
                {
                    receitas.Add(rec);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Fazer um for para percorrer o id_receitaAPi
                for (int i = 0; i < receitas.Count(); i++)
                {
                    HttpResponseMessage Res = await client.GetAsync($"api/Receitas/{receitas[i].Id}");
                    if (Res.IsSuccessStatusCode)
                    {
                        var RecResponse = Res.Content.ReadAsStringAsync().Result;

                        ReceitaInfo.Add(JsonConvert.DeserializeObject<List<Receita>>(RecResponse)[0]);
                    }
                }
                return View(receitas);
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

        public async Task<IActionResult> GetReceitaCategoriaFilterAsync(int id_cate, String name_admin)
        {
            List<Receita> ReceitaInfo = new List<Receita>();
            List<Receita> ReceitasCategoriaInfo = new List<Receita>();

            //Buscar Todas as Receitas da API
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
            }

            var id_user = _context.Users
                .Where(u => u.email == name_admin)
                .FirstOrDefault();

            var id_admin = _context.Admin_pasteleiros
                .Where(a => a.id_user == id_user.Id_user)
                .FirstOrDefault();

            List<Receita> receitas = new List<Receita>();

            //Todas as receitas do admin especifico 
            foreach(var rec in ReceitaInfo)
            {
                if(rec.IdAdmin == id_admin.Id_admin)
                {
                    receitas.Add(rec);
                }
            }
            

            //Vai Buscar todas as receitas de acordo com a categoria
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                List<Receita> rec = new List<Receita>();

                //Fazer um for para percorrer as minhas receitas
                for (int i = 0; i < receitas.Count(); i++)
                {
                    HttpResponseMessage Resp = await client.GetAsync($"api/Receitas/GetReceitaCategoriaFilters/{id_cate}");
                   
                    if (Resp.IsSuccessStatusCode)
                    {
                        var RecResponseCategories = Resp.Content.ReadAsStringAsync().Result;

                        ReceitasCategoriaInfo = JsonConvert.DeserializeObject<List<Receita>>(RecResponseCategories);

                        for (int j = 0; j < ReceitasCategoriaInfo.Count(); j++)
                        {
                            if (receitas[i].Id == ReceitasCategoriaInfo[j].Id)
                            {
                                //Todas as receitas guardadas de acordo com a categoria
                                rec.Add(ReceitasCategoriaInfo.Where(r => r.Id == receitas[i].Id).FirstOrDefault());
                            }
                        }
                    }
                }
                return new JsonResult(rec);
            }
        }

        public async Task<IActionResult> GetReceitaCategoryFilter_ArquivosAsync(String name_admin)
        {
            List<Receita> ReceitaInfo = new List<Receita>();
            // vai buscar todas as receitas à api
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
            }

            var id_user = _context.Users
                .Where(u => u.email == name_admin)
                .FirstOrDefault();

            var id_admin = _context.Admin_pasteleiros
                .Where(a => a.id_user == id_user.Id_user)
                .FirstOrDefault();

            List<Receita> receitas = new List<Receita>();

            //Todas as receitas do admin especifico 
            foreach (var rec in ReceitaInfo)
            {
                if (rec.IdAdmin == id_admin.Id_admin)
                {
                    receitas.Add(rec);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Fazer um for para percorrer o id_receitaAPi
                for (int i = 0; i < receitas.Count(); i++)
                {
                    HttpResponseMessage Res = await client.GetAsync($"api/Receitas/{receitas[i].Id}");
                    if (Res.IsSuccessStatusCode)
                    {
                        var RecResponse = Res.Content.ReadAsStringAsync().Result;

                        ReceitaInfo.Add(JsonConvert.DeserializeObject<List<Receita>>(RecResponse)[0]);
                    }
                }
                return new JsonResult(receitas);
            }
        }


        // GET: admin_pasteleiro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin_pasteleiro = await _context.Admin_pasteleiros
                .Include(a => a.id_userNavigation)
                .FirstOrDefaultAsync(m => m.Id_admin == id);
            if (admin_pasteleiro == null)
            {
                return NotFound();
            }

            return View(admin_pasteleiro);
        }

        // GET: admin_pasteleiro/Create
        public IActionResult Create()
        {
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user");
            return View();
        }

        // POST: admin_pasteleiro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_admin,id_user")] Admin_pasteleiro admin_pasteleiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin_pasteleiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user", admin_pasteleiro.id_user);
            
            return View(admin_pasteleiro);
        }

        // GET: admin_pasteleiro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin_pasteleiro = await _context.Admin_pasteleiros.FindAsync(id);
            if (admin_pasteleiro == null)
            {
                return NotFound();
            }
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user", admin_pasteleiro.id_user);
            return View(admin_pasteleiro);
        }

        // POST: admin_pasteleiro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_admin,id_user")] Admin_pasteleiro admin_pasteleiro)
        {
            if (id != admin_pasteleiro.Id_admin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin_pasteleiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!admin_pasteleiroExists(admin_pasteleiro.Id_admin))
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
            ViewData["id_user"] = new SelectList(_context.Users, "Id_user", "Id_user", admin_pasteleiro.id_user);
            return View(admin_pasteleiro);
        }

        // GET: admin_pasteleiro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin_pasteleiro = await _context.Admin_pasteleiros
                .Include(a => a.id_userNavigation)
                .FirstOrDefaultAsync(m => m.Id_admin == id);
            if (admin_pasteleiro == null)
            {
                return NotFound();
            }

            return View(admin_pasteleiro);
        }

        // POST: admin_pasteleiro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin_pasteleiro = await _context.Admin_pasteleiros.FindAsync(id);
            _context.Admin_pasteleiros.Remove(admin_pasteleiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool admin_pasteleiroExists(int id)
        {
            return _context.Admin_pasteleiros.Any(e => e.Id_admin == id);
        }
    }
}
