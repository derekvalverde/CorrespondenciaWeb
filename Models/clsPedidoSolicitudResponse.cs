using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsPedidoSolicitudResponse
    {
        [Key]
        public int Id { get; set; }
        public int id { get; set; }
        public int pedId { get; set; }
        public string cliCodigo { get; set; }
        public string usuId { get; set; }
        public decimal pedPrecio { get; set; }
        public decimal pedCheque { get; set; }
        public string pedObservacion { get; set; }
        public DateTime pedFechaEntrega { get; set; }
        public string pedPeriodo { get; set; }
        
       public List<clsPedidoSolicitudDetalleResponse> detalleSolicitudCliente { get; set; }

    }
}
