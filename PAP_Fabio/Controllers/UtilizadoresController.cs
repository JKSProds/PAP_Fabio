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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAP_Fabio.Controllers
{
    public class UtilizadoresController : Controller
    {
        public IActionResult QR()
        {
            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;
            Utilizador u = context.ObterUtilizador(int.Parse(this.User.Claims.First().Value));
            u.codigoQR = context.ObterCodigoQR(u);
            return View(u);
        }

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
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

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

    }
}
