using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using culinariaMVC.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace culinariaMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly CulinariaDBContext _context;

        public UsersController(CulinariaDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public IActionResult LoginPage(User user)
        {

            var _user = _context.Users.Where(s => s.email == user.email);

           if (_user.Any())
             {
                 if (_user.Where(s => s.password == user.password).Any())
                 {
                     if (_user.Where(s => s.tipo_user == "Pasteleiro").Any())
                     {

                        LoginPast(user);
                        return RedirectToAction("Home", "Admin_pasteleiro", new { area = "Admin" });
                        /*return Json(new { status = true, message = "Login Successfull Pasteleiro" });*/
                     }
                     else
                     {
                        LoginLeitor(user);
                        return RedirectToAction("Home", "Leitors", new { area = "Leitor" });
                     /*return Json(new { status = true, message = "Login Successfull Leitor" }); ;*/
                     }
                 }
                 else
                 {
                    //return Json(new { status = false, message = "Invalid Password!" });

                    ViewBag.ErrorMessage = "Password Inválida";

                    return View("Create");
                 }
            }
            else
            {
                //return Json(new { status = false, message = "Invalid Email!", user.email, user.password });

                ViewBag.ErrorMessage = "Endereço de Email Inválido";

                return View("Create");
            }


        }

        public async void LoginPast(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role, "Pasteleiro"),
            };

            var identidadeDeUtilizador = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUtilizador);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }

        public async void LoginLeitor(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role, "Leitor")
            };

            var identidadeDeUtilizador = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUtilizador);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id_user == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_user,username,email,password,tipo_user")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                Leitor leitor = new Leitor();
                leitor.id_user = user.Id_user;

                Admin_pasteleiro admin = new Admin_pasteleiro();
                admin.id_user = user.Id_user;

                if (user.tipo_user == "Pasteleiro")
                {
                    var ad =_context.Admin_pasteleiros.Add(admin);
                    await _context.SaveChangesAsync();
                    LoginPast(user);
                }
                else
                {
                    _context.Leitors.Add(leitor);
                    await _context.SaveChangesAsync();
                    LoginLeitor(user);
                }

                return RedirectToAction("Index", "Home");
            }
            return View("Create");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_user,username,email,password,tipo_user")] User user)
        {
            if (id != user.Id_user)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id_user))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id_user == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            var leitor = await _context.Leitors.FindAsync(user.Id_user);

            var admin = await _context.Admin_pasteleiros.FindAsync(user.Id_user);
        
            if (user.tipo_user == "Pasteleiro")
            {
                _context.Admin_pasteleiros.Remove(admin);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Leitors.Remove(leitor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id_user == id);
        }
    }
}
