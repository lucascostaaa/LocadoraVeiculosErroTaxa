using AutoMapper;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.WebApplication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApplication.Controllers
{
    public class CupomController : Controller
    {
        private readonly ICupomRepository cupomRepository;
        private readonly IParceiroRepository parceiroReporitoy;
        private readonly ICupomAppService cupomAppService;
        private readonly IMapper mapper;

        public CupomController(ICupomRepository cupomRepository,
            IParceiroRepository parceiroReporitoy,
            ICupomAppService cupomAppService,
            IMapper mapper)
        {
            this.cupomRepository = cupomRepository;
            this.parceiroReporitoy = parceiroReporitoy;
            this.cupomAppService = cupomAppService;
            this.mapper = mapper;
        }

        // GET: CupomController
        public ActionResult Index()
        {
            var cupons = cupomRepository.SelecionarTodos();

            var cupomIndexVM = mapper.Map<CupomIndexViewModel>(cupons);

            return View(cupomIndexVM);
        }       

        // GET: CupomController/Create
        public ActionResult Create()
        {
            var parceiros = parceiroReporitoy.SelecionarTodos();

            var cupomCreateVM = new CupomCreateViewModel();

            cupomCreateVM.Parceiros = mapper.Map<List<SelectListItem>>(parceiros);

            return View(cupomCreateVM);
        }

        // POST: CupomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CupomCreateViewModel cupomCreateVM)
        {
            try
            {
                var cupom = mapper.Map<Cupom>(cupomCreateVM);

                cupomAppService.RegistrarNovoCupom(cupom);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CupomController/Edit/5
        public ActionResult Edit(int id)
        {
            var cupom = cupomRepository.SelecionarPorId(id);

            var parceiros = parceiroReporitoy.SelecionarTodos();

            var cupomEditVM = mapper.Map<CupomEditViewModel>(cupom);

            cupomEditVM.Parceiros = mapper.Map<List<SelectListItem>>(parceiros);

            return View(cupomEditVM);
        }

        // POST: CupomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CupomEditViewModel cupomEditVM)
        {
            try
            {
                var cupom = mapper.Map<Cupom>(cupomEditVM);

                cupomAppService.EditarCupom(id, cupom);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CupomController/Details/5
        public ActionResult Details(int id)
        {
            var cupom = cupomRepository.SelecionarPorId(id);
            
            var cupomDetailsVM = mapper.Map<CupomDetailsViewModel>(cupom);

            return View(cupomDetailsVM);
        }

        // GET: CupomController/Delete/5
        public ActionResult Delete(int id)
        {
            var cupom = cupomRepository.SelecionarPorId(id);

            var cupomDeleteVM = mapper.Map<CupomDeleteViewModel>(cupom);

            return View(cupomDeleteVM);
        }

        // POST: CupomController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                cupomAppService.ExcluirCupom(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
