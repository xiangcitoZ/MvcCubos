using Microsoft.AspNetCore.Mvc;
using MvcCubos.Models;
using MvcCubos.Services;

namespace MvcCubos.Controllers
{
    public class CubosController : Controller
    {
        private ServiceApiCubos service;
        private ServiceStorageBlobs serviceblobs;

        public CubosController(ServiceApiCubos service, ServiceStorageBlobs serviceblobs)
        {
            this.service = service;
            this.serviceblobs = serviceblobs;
        }

        public async Task<IActionResult> Index()
        {
            List<BlobModel> listBlobs = await this.serviceblobs.GetBlobsAsync("imagencubos");
            ViewData["IMAGEN_CUBOS"] = listBlobs;
            List<Cubos> cubos =
               await this.service.GetCubosAsync();
            return View(cubos);
        }

       

        public async Task<IActionResult> Marca()
        {
            List<string> marca =
               await this.service.GetMarcas();
            ViewData["Marcas"] = marca;
            return View();
        }

        public async Task<IActionResult> MarcaList(string marca)
        {
            List<Cubos> cubos =
               await this.service.GetMarcasList(marca);
            return View(cubos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cubos cubo)
        {
            await this.service.InsertCuboAsync
                (cubo.IdCubo, cubo.Nombre,
                cubo.Marca, cubo.Imagen, cubo.Precio);
            return RedirectToAction("Index");
        }

    }
}
