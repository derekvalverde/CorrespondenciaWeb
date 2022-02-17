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
    public class RegistroCorrespondenciaViewComponent: ViewComponent
    {
        public IRepositorioConsumir _repositorio { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public RegistroCorrespondenciaViewComponent(IRepositorioConsumir repositorio, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _repositorio = repositorio;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

           
            var CadToken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");

               
                var correspondencia = new clsRegistrarCorrespondencia();
                var ListadoDescripcion = await _repositorio.obtenerDescripcionListarAsync(CadToken);
                var ListadoAreaDestino = await _repositorio.obtenerAreaDestinoListarAsync(CadToken);
                correspondencia.ListListarDescripcion = ListadoDescripcion;
                correspondencia.ListListadoAreaDestino = ListadoAreaDestino;

                
                return View(correspondencia);
            
            
        }
       
    }
}
