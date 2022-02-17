using INTI_PP.Models;
using INTI_PP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.ViewComponents
{
    public class GeolocalizacionViewComponent:ViewComponent
    {
        public IRepositorioConsumir _repositorio { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public GeolocalizacionViewComponent(IRepositorioConsumir repositorio, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _repositorio = repositorio;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        public IViewComponentResult Invoke()
        {
            var CadToken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");
            var usuId =Convert.ToInt32(HttpContext.Session.GetString("SessionUsuId"));
            // var ListaClienteUbicacion = _repositorio.obtenerUbicacionClientes(usuId, CadToken);
            var ListaClienteUbicacion = new List<clsClienteResponsableUbicacionUsuIdResponse>();
            return View(ListaClienteUbicacion);
           
        }
    }
}
