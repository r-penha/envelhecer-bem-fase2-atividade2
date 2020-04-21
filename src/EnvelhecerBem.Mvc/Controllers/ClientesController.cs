using System;
using System.Linq;
using System.Threading.Tasks;
using EnvelhecerBem.Core.Services;
using EnvelhecerBem.Mvc.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvelhecerBem.Mvc.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteApplicationService _appService;

        public ClientesController(ClienteApplicationService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        public async Task<IActionResult> Index()
        {
            var coll = await _appService.ListarClientes();
            var list = coll.Select(c => c.ToViewModel()).ToList();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(ViewModels.Cliente cliente)
        {
            if (!ModelState.IsValid) return View();
            await _appService.RegistrarCliente(cliente.ToDomain());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var cliente = await _appService.ObterCliente(id);
            return View(cliente.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ViewModels.Cliente cliente)
        {
            if (!ModelState.IsValid) return View();
            await _appService.AlterarCliente(cliente.ToDomain());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(Guid id)
        {
            await _appService.RemoverCliente(id);
            return RedirectToAction("Index");
        }
    }

    public static class HtmlHelpers
    {
        public static IHtmlContent MenuLink(this IHtmlHelper htmlHelper,
                                            string linkText,
                                            string actionName,
                                            string controllerName
        )
        {

            //var currentAction = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();

            return controllerName == currentController
                       ? htmlHelper.ActionLink(linkText, actionName, controllerName, null, null, null, null, new {@class = "nav-link active"})
                       : htmlHelper.ActionLink(linkText, actionName, controllerName, null, null, null, null, new {@class = "nav-link"});
        }
    } 
}