using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsPedidoSolicitudDetalleResponse
    {
        public int Id { get; set; }        
        public int pedId { get; set; }
        public string matCodigo { get; set; }
        public decimal pddCantidad { get; set; }
        public decimal pddCantidadAtendida { get; set; }
    }
}
