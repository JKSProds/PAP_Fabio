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
    public class Bar : Controller
    {

        public IActionResult ListaBar(string filter, int? page, int ID)
        {

            DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;

            if (filter == null) { filter = ""; }

            ViewData["filter"] = filter;

            ViewData["nome"] = context.ObterUtilizador(ID).Nome;

            return View(context.ObterBar(ID).Where(c => c.NomeProd.Contains(filter)));

        }

        public ActionResult Apagar(string id)
        {
            try
            {
                DB_Context context = HttpContext.RequestServices.GetService(typeof(DB_Context)) as DB_Context;
                context.ApagarBar(id);
                return RedirectToAction("ListaBar");
            }
            catch
            {
                return RedirectToAction("ListaBar");
            }
        }
    }
   
}   
