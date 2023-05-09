using MvcCubos.Services;
using Microsoft.AspNetCore.Mvc;


namespace MvcCubos.ViewComponents
{
    public class MenuMarcasViewComponent : ViewComponent
    {
        private ServiceApiCubos service;

        public MenuMarcasViewComponent(ServiceApiCubos service)
        {
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<string> marcas = await this.service.GetMarcas();
            return View(marcas);
        }


    }
}
