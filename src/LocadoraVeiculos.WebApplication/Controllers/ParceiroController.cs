using AutoMapper;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.WebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraVeiculos.WebApplication.Controllers
{
    public class ParceiroController : Controller
    {
        private readonly IParceiroAppService parceiroAppService;
        private readonly IParceiroRepository parceiroRepository;
        private readonly IMapper mapper;

        public ParceiroController(IParceiroAppService parceiroAppService,
            IParceiroRepository parceiroRepository,
            IMapper mapper)
        {
            this.parceiroAppService = parceiroAppService;
            this.parceiroRepository = parceiroRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var parceiros = parceiroRepository.SelecionarTodos();

            var parceiroIndexVM =
                mapper.Map<ParceiroIndexViewModel>(parceiros);

            return View(parceiroIndexVM);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var parceiro = parceiroRepository.SelecionarPorId(id);

            var parceiroDetailsVM =
                mapper.Map<ParceiroDetailsViewModel>(parceiro);

            if (parceiroDetailsVM == null)
            {
                return NotFound();
            }

            return View(parceiroDetailsVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var parceiroCreateVM = new ParceiroCreateViewModel();

            return View(parceiroCreateVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParceiroCreateViewModel parceiroCreateVM)
        {
            Parceiro parceiro = mapper.Map<Parceiro>(parceiroCreateVM);

            parceiroAppService.RegistrarNovoParceiro(parceiro);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var parceiro = parceiroRepository.SelecionarPorId(id);

            var parceiroEditVM =
                mapper.Map<ParceiroEditViewModel>(parceiro);

            if (parceiroEditVM == null)
            {
                return NotFound();
            }

            return View(parceiroEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParceiroEditViewModel parceiroEditVM)
        {
            var parceiro = mapper.Map<Parceiro>(parceiroEditVM);

            parceiroAppService.EditarParceiro(id, parceiro);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var parceiro = parceiroRepository.SelecionarPorId(id);

            var parceiroDeleteVM =
                mapper.Map<ParceiroDeleteViewModel>(parceiro);

            if (parceiroDeleteVM == null)
            {
                return NotFound();
            }

            return View(parceiroDeleteVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var parceiro = parceiroRepository.SelecionarPorId(id);

            if (parceiro == null)
                return NotFound();

            parceiroAppService.ExcluirParceiro(id);

            return RedirectToAction("Index");
        }
    }
}
