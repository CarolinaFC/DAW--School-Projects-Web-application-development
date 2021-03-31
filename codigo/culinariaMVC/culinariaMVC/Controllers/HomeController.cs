using culinariaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace culinariaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        CulinariaDBContext db = new CulinariaDBContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44315/";

        public async Task<IActionResult> IndexAsync()
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

        public async Task<IActionResult> GetReceitaFilterAsync(int id_cate, String grau, String custo, String tempo)
        {
            List<Receita> ReceitaInfo = new List<Receita>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Receitas/GetReceitaFilters/{id_cate}/{grau}/{custo}/{tempo}");

                if (Res.IsSuccessStatusCode)
                {
                    var RecResponse = Res.Content.ReadAsStringAsync().Result;

                    ReceitaInfo = JsonConvert.DeserializeObject<List<Receita>>(RecResponse);
                }
                return new JsonResult(ReceitaInfo);
            }
        }

        public async Task<IActionResult> GetReceitaCategoriaFilterAsync(int id_cate)
        {
            List<Receita> ReceitaInfo = new List<Receita>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Receitas/GetReceitaCategoriaFilters/{id_cate}");

                if (Res.IsSuccessStatusCode)
                {
                    var RecResponse = Res.Content.ReadAsStringAsync().Result;

                    ReceitaInfo = JsonConvert.DeserializeObject<List<Receita>>(RecResponse);
                }
                return new JsonResult(ReceitaInfo);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
