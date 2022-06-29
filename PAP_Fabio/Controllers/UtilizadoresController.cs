using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAP_Fabio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAP_Fabio.Controllers
{
    public class UtilizadoresController : Controller
    {

        public IActionResult Login(string email, string password)
        { 
            ViewData["ReturnUrl"] = Request.Query["ReturnURL"];
            Utilizador utilizador_req = new Utilizador { Email = email, Pass = password };
            if (email != null && password != null)
            {
                DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;

                Utilizador utilizador = context.ObterUtilizador(email);

                if (utilizador != new Utilizador()) ModelState.AddModelError("", "Não foram encontrados utlizadores com esse nome!");

                var passwordHasher = new PasswordHasher<string>();
                if (passwordHasher.VerifyHashedPassword(null, utilizador.Pass, utilizador_req.Pass) == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, utilizador.ID.ToString()),
                        new Claim(ClaimTypes.GivenName, utilizador.Nome),
                        new Claim(ClaimTypes.Role, utilizador.admin ? "Admin" : "User"),
                        new Claim(ClaimTypes.Role, utilizador.tipo == 1 ? "Professor" : utilizador.tipo == 2 ? "Funcionario" : "Aluno")

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if (ViewData["ReturnUrl"].ToString() != "" && ViewData["ReturnUrl"].ToString() != null)
                    {
                        Response.Redirect(ViewData["ReturnUrl"].ToString(), true);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "Password errada!");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Utilizador utilizador_req, string ReturnUrl)
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;

            Utilizador utilizador = context.ObterUtilizador(utilizador_req.Email);

            if (utilizador.Email == null)
            {
                ModelState.AddModelError("", "Não foram encontrados utlizadores com esse email!");
                return View();
            }

            var passwordHasher = new PasswordHasher<string>();
            if (passwordHasher.VerifyHashedPassword(null, utilizador.Pass, utilizador_req.Pass) == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, utilizador.ID.ToString()),
                        new Claim(ClaimTypes.GivenName, utilizador.Nome),
                        new Claim(ClaimTypes.Role, utilizador.admin ? "Admin" : "User"),
                        new Claim(ClaimTypes.Role, utilizador.tipo == 1 ? "Professor" : utilizador.tipo == 2 ? "Funcionario" : "Aluno")
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (ReturnUrl != "" && ReturnUrl != null)
                {
                    Response.Redirect(ViewData["ReturnUrl"].ToString(), true);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            else
            {
                ModelState.AddModelError("", "Password errada!");
            }
            return View();

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]

        public IActionResult Index(string filter, string tipo, int? page)
        {
            
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;
            int tipoUser = 3;
            //int pageSize = 50;
            var pageNumber = page ?? 1;

            List<String> LstTipo = new List<string>() { "Aluno", "Professor", "Funcionário" };

            ViewBag.TipoUtilizador = LstTipo.Select(l => new SelectListItem() { Value = l, Text = l });

            if (filter == null) { filter = ""; }
            if (tipo == null) { tipo = "Aluno"; }

                ViewData["filter"] = filter;
                ViewData["tipo"] = tipo;

            if (tipo == "Aluno") tipoUser = 3;
            if (tipo == "Professor") tipoUser = 1;
            if (tipo == "Funcionário") tipoUser = 2;

            return View(context.ObterUtilizadores(tipoUser).Where(c => c.Nome.Contains(filter)));
        }
        
        public IActionResult Editar(string id)
        {
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;
            int id_user = 0;
            int.TryParse(id, out id_user);

            return View(context.ObterUtil(id_user));
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Editar(Utilizador utilizador)
        {
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;

            context.EditarUtil(utilizador);

            return RedirectToAction("Index");
        }

        public IActionResult Novo(string id)
        {
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;
            int id_user = 0;
            int.TryParse(id, out id_user);
            return View(context.ObterUtil(id_user));
        }

        [HttpPost]
        public IActionResult Novo(Utilizador utilizador)
        {
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;

            if(context.ExisteEmailDuplicado(utilizador.Email))
            {
                ModelState.AddModelError("", "Já existe um utilizador com esse email!");
            }
            else { context.NovoUtil(utilizador); }

            return RedirectToAction("Index");

        }
      
        [Authorize(Roles = "Admin")]

        /*public IActionResult Apagar(string id)
        {
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;
            int id_user = 0;
            int.TryParse(id, out id_user);

            return View(context.ObterUtil(id_user));
        }

        [HttpPost]
        public IActionResult Apagar(Utilizador utilizador)
        {
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;

            context.ApagarUser(utilizador);

            return RedirectToAction("Index");
        }
        */

        public ActionResult Apagar(string id)
        {
            try
            {
                DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;
                context.ApagarUser(id);

                return RedirectToAction("Index");
            }

            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}   
