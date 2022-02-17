using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsFacturaClienteListarCodigoDetalleResponse
    {
        public int Id { get; set; }
        public string matCodigo { get; set; }
        public decimal fadCantidad { get; set; }
        public decimal fadPrecio { get; set; }
        public decimal fadCheque { get; set; }
    }
}
