using INTI_PP.Models;
using INTI_PP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.ViewComponents
{
    public class LineasViewComponent:ViewComponent
    {

        public IRepositorioConsumir _repositorio { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LineasViewComponent(IRepositorioConsumir repositorio, IHttpContextAccessor httpContextAccessor)
        {
            _repositorio = repositorio;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var CadToken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");
            var Usuario = _httpContextAccessor.HttpContext.Session.GetString("SessionUsuario");
            //List<clsLineaListarResponse> lineas = new List<clsLineaListarResponse>();
            var lineas =await _repositorio.obtenerLineasAsync("40", CadToken);


            return View(lineas);
        }
    }
}
