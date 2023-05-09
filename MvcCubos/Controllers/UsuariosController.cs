using Microsoft.AspNetCore.Mvc;
using MvcCubos.Filters;
using MvcCubos.Models;
using MvcCubos.Services;

namespace MvcCubos.Controllers
{
    public class UsuariosController : Controller
    {
        private ServiceApiCubos service;
        private ServiceStorageBlobs serviceblob;

        public UsuariosController(ServiceApiCubos service, ServiceStorageBlobs serviceblob)
        {
            this.service = service;
            this.serviceblob = serviceblob;
        }



        [AuthorizeUsuarios]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> Perfil()
        {
            string token =
                HttpContext.Session.GetString("TOKEN");
            UsuariosCubo usuario = await
               this.service.GetPerfilUsuarioAsync(token);
            BlobModel blobPerfil = await this.serviceblob.FindBlobPerfil("imagenperfil", usuario.Imagen, usuario.Nombre);
            ViewData["IMAGEN_PERFIL"] = blobPerfil;
            return View(usuario);
        }

        //COMEN

    }
}
