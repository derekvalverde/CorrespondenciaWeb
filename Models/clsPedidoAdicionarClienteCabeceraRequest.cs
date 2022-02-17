using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsPedidoAdicionarClienteCabeceraRequest
    {
        public DateTime pedFechaEntrega { get; set; }
        public decimal pedPrecio { get; set; }
        public DateTime pedFechaPedido { get; set; }
        public int usuId { get; set; }
        public int uclId { get; set; }
        public string cliCodigo { get; set; }
        public string aplicacion { get; set; }
        public List<clsPedidoAdicionarClienteDetalleRequest> detallePedidoAdicionar { get; set; }
    }
}
