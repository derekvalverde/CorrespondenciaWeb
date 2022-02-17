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
    public class MiCuentaViewComponent:ViewComponent

    {
        public IRepositorioConsumir _repositorio { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public MiCuentaViewComponent(IRepositorioConsumir repositorio, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _repositorio = repositorio;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()

        {
            var CadToken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");
            var Usuario = _httpContextAccessor.HttpContext.Session.GetString("SessionUsuario");
            var usuId = Convert.ToInt32(HttpContext.Session.GetString("SessionUsuId"));
            var aplicacion = _httpContextAccessor.HttpContext.Session.GetString("SessionAplicacion");
            
            var cliCodigo = _httpContextAccessor.HttpContext.Session.GetString("SessionCliCodigo");
            
            
            //Buscamos en base de datos
            var ListaMiCuenta =  _context.MiCuenta.Where(elemento => elemento.cliCodigo == cliCodigo).ToList();


            //si NO existe invocamos el API
            if (ListaMiCuenta.Count == 0)
            {
                var miCuenta = await _repositorio.obtenerMiCuentaAsync(usuId, CadToken );
                _context.MiCuenta.Add(miCuenta);
                _context.SaveChanges();
                //Guardamos el clicodigo en variable de sesion 
                HttpContext.Session.SetString("SessionCliCodigo", miCuenta.cliCodigo);
                
                return View(miCuenta);
            }
            else {
                var miCuenta = ListaMiCuenta.ElementAt(0);
                //Guardamos el clicodigo en variable de sesion 
                HttpContext.Session.SetString("SessionCliCodigo", miCuenta.cliCodigo);
                return View(miCuenta);
            }                       

                
        }
    }
}
