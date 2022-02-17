using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INTI_PP.Models;
using INTI_PP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INTI_PP.ViewComponents
{
    public class RegistroArchiveroViewComponent: ViewComponent
    {
        public IRepositorioConsumir _repositorio { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public RegistroArchiveroViewComponent(IRepositorioConsumir repositorio, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _repositorio = repositorio;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            var CadToken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");


            var archivero = new clsRegistrarArchivero();
            
            var ListadoAreaDestino = await _repositorio.obtenerAreaDestinoListarAsync(CadToken);

            archivero.ListListadoAreaDestino = ListadoAreaDestino;


            return View(archivero);


        }


    }
}
