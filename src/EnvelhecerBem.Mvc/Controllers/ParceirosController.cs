using System;
using System.Linq;
using System.Threading.Tasks;
using EnvelhecerBem.Core.Services;
using EnvelhecerBem.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvelhecerBem.Mvc.Controllers
{
    public class ParceirosController : Controller
    {
        private readonly ParceiroApplicationService _appService;

        public ParceirosController(ParceiroApplicationService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        public async Task<IActionResult> Index()
        {
            var coll = await _appService.ListarParceiros();
            var list = coll.Select(p => p.ToViewModel()).ToList();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(ViewModels.Parceiro parceiro)
        {
            if (!ModelState.IsValid) return View();
            await _appService.RegistrarParceiro(parceiro.ToDomain());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var parceiro = await _appService.ObterParceiro(id);
            return View(parceiro.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ViewModels.Parceiro parceiro)
        {
            if (!ModelState.IsValid) return View();
            await _appService.AlterarParceiro(parceiro.ToDomain());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(Guid id)
        {
            await _appService.RemoverParceiro(id);
            return RedirectToAction("Index");
        }
    }
}