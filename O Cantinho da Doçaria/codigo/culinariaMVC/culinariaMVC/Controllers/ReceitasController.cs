using culinariaMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace culinariaMVC.Controllers
{
    public class ReceitasController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly CulinariaDBContext _context;

        // Ambiente Host para aceder à pasta wwwroot
        public ReceitasController(CulinariaDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        string Baseurl = "https://localhost:44315/";

        List<Receita> ReceitaInfo = new List<Receita>();

        // GET: ReceitasController
        public async Task<ActionResult> IndexAsync()
        {
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

        // GET: ReceitasController/Details/5
        /**
          * Vai buscar todos os dados presentes na Tabela Receitas da API
          * 
          * @param id, corresponde ao id da receita
          **/
        public async Task<ActionResult> DetailsAsync(int id)
        {
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

        public async Task<IActionResult> GetAvaliacaoAsync(int id_rec)
        {
            List<Avaliaco> AvaliacaoInfo = new List<Avaliaco>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Avaliacoes/{id_rec}");
                Res.EnsureSuccessStatusCode();
                if (Res.IsSuccessStatusCode)
                {
                    var AvaliacaoResponse = Res.Content.ReadAsStringAsync().Result;

                    AvaliacaoInfo = JsonConvert.DeserializeObject<List<Avaliaco>>(AvaliacaoResponse);
                }
                return new JsonResult(AvaliacaoInfo);
            }
        }

        public int PostAvaliacao(int id_receita, int rating)
        {
            Avaliaco avaliacao = new Avaliaco();
            avaliacao.IdReceita = id_receita;
            avaliacao.QuantidadeEstrelas = rating;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var postTask = client.PostAsJsonAsync<Avaliaco>("api/Avaliacoes", avaliacao);
                postTask.Wait();

                var result = postTask.Result;
                result.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                {
                    return rating;
                }
                return rating;
            }
        }

        // GET: ReceitasController/GetComentarios/{id}
        /**
          * Vai buscar à Tabela ComentariosPublicos os comentários referentes Receita visualizada
          * 
          * @param id, corresponde ao id dos comentarios
          **/
        public async Task<IActionResult> GetComentariosPubAsync(int id)
        {
            List<ComentariosPublico> ComentInfo = new List<ComentariosPublico>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/ComentariosPublicoes/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var ComentResponse = Res.Content.ReadAsStringAsync().Result;

                    ComentInfo = JsonConvert.DeserializeObject<List<ComentariosPublico>>(ComentResponse);
                }

                List<ComentariosPublico> semComent = new List<ComentariosPublico>();

                foreach(var coment in ComentInfo)
                {
                    if(coment.DescricaoComentariosPub != "Sem Comentário")
                    {
                        semComent.Add(coment);
                    }
                }

                return new JsonResult(semComent);
            }
        }

        // POST: ReceitasController/PostComentPub/{coment}/{id_receita}
        /**
          * Vai adicionar à Tabela ComentariosPublicos os comentários inseridos na view Details da Receita visualizada
          * 
          * @param coment, corresponde ao comentário obtido através do ajax
          * @param id_receita, corresponde ao id da receita
          **/
        public string PostComentPub(String coment, int id_receita)
        {
            ComentariosPublico comentario = new ComentariosPublico();
            comentario.IdReceita = id_receita;
            comentario.DescricaoComentariosPub = coment;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var postTask = client.PostAsJsonAsync<ComentariosPublico>("api/ComentariosPublicoes", comentario);
                postTask.Wait();

                var result = postTask.Result;
                result.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                {
                    return coment;
                }
                return coment;
            }
        }

        // GET: ReceitasController/GetCategoriasNames/{id}
        /**
         * Vai buscar todas as categorias à API para depois os dados sere tratados em ajax e obter assim o nome 
         * 
         * @param id, corresponde ao id da categoria
         **/
        public async Task<IActionResult> GetCategoriasNamesAsync(int id)
        {
            List<Categoria> CatNames = new List<Categoria>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/Categorias/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var CatResponse = Res.Content.ReadAsStringAsync().Result;

                    CatNames = JsonConvert.DeserializeObject<List<Categoria>>(CatResponse);
                }
                return new JsonResult(CatNames);
            }
        }

        // GET: ReceitasController/Create
        /**
         * Apresenta a View da Criação de uma Receita
         **/
        public ActionResult Create()
        {
            return View();
        }


        // Upload das Imagens das Receitas
        /**
         * Vai guardar a Imagem que foi selecionada pelo pasteleiro fazendo assim o upload da mesma e passar
         * o nome obtido pela variavel ImageFile.FileName do tipo IFormFile para a string que representa o caminho 
         * da imagem na base de dados da API, ImgReceita
         * 
         * @param receita, corresponde ao objecto do Modelo Receita
         **/
        public async Task<IActionResult> UploadImageAsync(Receita receita)
        {
            // Save image to wwwroot/Photos
            string wwwRootPath = hostEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(receita.ImageFile.FileName);
            string extension = Path.GetExtension(receita.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Photos/", filename);

            receita.ImgReceita = filename;
            string img = receita.ImgReceita;

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await receita.ImageFile.CopyToAsync(fileStream);
            }

            return new JsonResult(img);
        }


        // POST: ReceitasController/Create
        /**
         * Faz o upload da imagem através do Método criado UploadImage e envia os novos dados do formulário para a base de dados
         * da API
         * 
         * @param receita, corresponde ao objecto do Modelo Receita
         **/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Receita receita, String name_admin)
        {
            var id_user = _context.Users
                .Where(u => u.email == name_admin)
                .FirstOrDefault();

            var id_admin = _context.Admin_pasteleiros
                .Where(a => a.id_user == id_user.Id_user)
                .FirstOrDefault();

            receita.IdAdmin = id_admin.Id_admin;

            if(receita.ImageFile != null) 
            {
                UploadImageAsync(receita);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var postTask = client.PostAsJsonAsync<Receita>("api/Receitas", receita);
                    postTask.Wait();

                    var result = postTask.Result;
                    result.EnsureSuccessStatusCode();
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Arquivos_admin", "Admin_pasteleiro", new { name_admin = name_admin });
                    }
                }
            }
            ViewBag.ErrorMessage = "Preencha todos os campos";

            return View("Create");
        }


        // GET: ReceitasController/Edit/5
        /**
         * Vai buscar todos os dados da API e apresentar na View EDIT
         * 
         * @param id, corresponde ao id da receita
         * @param receita, corresponde ao objecto do Modelo Receita
         **/
        public async Task<ActionResult> EditAsync(int id)
        {
            List<Receita> ReceitaInfo = new List<Receita>();

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


        // PUT: ReceitasController/Edit/5
        /**
         * Verifica se recebe uma imagem a partir do input na view se SIM, apaga a imagem que está guardada e faz o upload da nova
         * enviando de seguida os novos dados para a API
         * 
         * Se não encontrar nenhuma imagem no input referente ao upload da mesma então 
         * vai Buscar a Receita à API para ir buscar a string ImgReceita pois a imagem não se alterou
         * 
         * Após obter a string da imagem que é para manter este envia os novos dados para a API
         * 
         * @param id, corresponde ao id da receita
         * @param receita, corresponde ao objecto do Modelo Receita
         **/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Receita receita, String name_admin)
        {
            var id_user = _context.Users
                .Where(u => u.email == name_admin)
                .FirstOrDefault();

            var id_admin = _context.Admin_pasteleiros
                .Where(a => a.id_user == id_user.Id_user)
                .FirstOrDefault();

            receita.IdAdmin = id_admin.Id_admin;

            List<Receita> ReceitaInfo = new List<Receita>();

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
            }


            if (receita.ImageFile != null)
            {

                var rec = ReceitaInfo.Where(r => r.Id == id).FirstOrDefault();

                var rec_img = rec.ImgReceita;
                string wwwRootPath = hostEnvironment.WebRootPath;
                var imgPath = Path.Combine(wwwRootPath + "/Photos/", rec_img);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
                
                UploadImageAsync(receita);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var putTask = client.PutAsJsonAsync<Receita>($"api/Receitas/{id}", receita);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Arquivos_admin", "Admin_pasteleiro", new { name_admin = name_admin });
                    }
                    return View();
                }
            }
            else
            {
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
                }

                var rec = ReceitaInfo[0].ImgReceita;
                receita.ImgReceita = rec;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var putTask = client.PutAsJsonAsync<Receita>($"api/Receitas/{id}", receita);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Arquivos_admin", "Admin_pasteleiro", new { name_admin = name_admin });
                    }
                    return View();
                }
            }
        }


        // GET: ReceitasController/Delete/5
        /**
         * Vai buscar todos os dados da API e apresentar na View DELETE
         * 
         * @param id, corresponde ao id da receita
         **/
        public async Task<ActionResult> DeleteAsync(int id)
        {
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


        // POST: ReceitasController/DeleteReceitas/5
        /**
        * Vai buscar a receita a partir do id para depois obter a string referente ao caminho da imagem
        * 
        * De seguida vai apagar a imagem do caminho wwwroot/Photos
        * 
        * Envia para o Metodo DELETE da API os dados da receita referente ao id a apagar
        * 
        * @param id, corresponde ao id da receita
        * @param receita, corresponde ao objecto do Modelo Receita
        **/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteReceitasAsync(int id, String name_admin)
        {
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


                var rec_img = ReceitaInfo[0].ImgReceita;
                string wwwRootPath = hostEnvironment.WebRootPath;
                var imgPath = Path.Combine(wwwRootPath + "/Photos/", rec_img);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }

                foreach (var coment in ReceitaInfo[0].ComentariosPublicos)
                {
                    var id_coment = coment.Id;
                    var deleteComentTask = await client.DeleteAsync($"api/ComentariosPublicoes/{id_coment}");
                }

                foreach (var ava in ReceitaInfo[0].Avaliacos)
                {
                    var id_ava = ava.Id;
                    var deleteAvaTask = await client.DeleteAsync($"api/Avaliacoes/{id_ava}");
                }
          
                var deleteTask = await client.DeleteAsync($"api/Receitas/{id}");

                if (deleteTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Arquivos_admin", "Admin_pasteleiro", new { name_admin = name_admin });
                }
                return View();
            }
        }
    }
}
