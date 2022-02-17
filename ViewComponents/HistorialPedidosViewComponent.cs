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
    public class HistorialPedidosViewComponent : ViewComponent
    {
        public IRepositorioConsumir _repositorio { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public HistorialPedidosViewComponent(IRepositorioConsumir repositorio, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _repositorio = repositorio;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        
       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var detalle = await _context.ListadoPedidosDetalle.ToListAsync();
            var CadToken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");
            
            var usuId = HttpContext.Session.GetString("SessionUsuId");
            //var pedidos = await _context.ListadoPedidos.Where(elemento => elemento.usuId.Equals(usuId, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
            //Buscamos en base de datos
            


            var ListaPedidos = _context.ListadoPedidos.Where(elemento => elemento.usuId == usuId).OrderByDescending(x => x.pedId).ToList();



            if (ListaPedidos.Count == 0)
            {
                var historialPedidos = await _repositorio.obtenerHistorialPedidosAsync(usuId, CadToken);
                _context.ListadoPedidos.AddRange(historialPedidos);
                _context.SaveChanges();
                return View(historialPedidos);
            }
            else
            {
                
                return View(ListaPedidos);
            }
            
            //List<clsPedidoSolicitudResponse> ListaPedidos = new List<clsPedidoSolicitudResponse>();
            //return View(ListaPedidos);


        }

    }
}
