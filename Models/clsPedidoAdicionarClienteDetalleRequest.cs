using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsPedidoAdicionarClienteDetalleRequest
    {
        public string matCodigo { get; set; }
        public int pddCantidad { get; set; }
        public decimal pddPrecio { get; set; }
        public decimal pddPrecioVenta { get; set; }
    }
}
