using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options){

        }
        public DbSet<clsLoginResponse> LoginResponse { get; set; }
        public DbSet<clsClienteVentaCliCodigoResponse> MiCuenta { get; set; }
        
        public DbSet<clsListarProductosResponse> LitadoGeneral { get; set; }
        
        public DbSet<clsCarrito> ListadoCarrito { get; set; }

        public DbSet<clsLineaListarResponse> ListadoLineas { get; set; }
        public DbSet<clsFacturaClienteListarCodigoCabeceraResponse> ListadoFacturas{ get; set; }
        public DbSet<clsFacturaClienteListarCodigoDetalleResponse> ListadoFacturaDetalle { get; set; }
        public DbSet<clsPedidoSolicitudResponse> ListadoPedidos { get; set; }
        public DbSet<clsPedidoSolicitudDetalleResponse> ListadoPedidosDetalle { get; set; }

    }
}
