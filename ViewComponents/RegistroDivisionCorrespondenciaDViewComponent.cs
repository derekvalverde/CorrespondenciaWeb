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
    public class RegistroDivisionCorrespondenciaDViewComponent: ViewComponent
    {

        public IRepositorioConsumir _repositorio { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public RegistroDivisionCorrespondenciaDViewComponent(IRepositorioConsumir repositorio, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _repositorio = repositorio;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            var CadToken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");
            var empCodigo = _httpContextAccessor.HttpContext.Session.GetString("SessionEmpCodigo");

            var correspondenciaDetalle = new clsCorrespondenciaDivisionD();
            var ListadoDestinatarioListar = await _repositorio.obtenerDestinatarioListarAsync(empCodigo, CadToken);

            correspondenciaDetalle.ListListadoDestinatarioListar = ListadoDestinatarioListar;



            return View(correspondenciaDetalle);


        }


    }
}
