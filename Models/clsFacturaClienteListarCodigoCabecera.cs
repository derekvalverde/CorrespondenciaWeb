using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsFacturaClienteListarCodigoCabecera
    {
        public int Id { get; set; }
        public string cliCodigo { get; set; }
        public string facCodigo { get; set; }
        public string facNumero { get; set; }
        public decimal facMonto { get; set; }
        public DateTime facFecha { get; set; }
        public DateTime facFechaPago { get; set; }
        public string facEmpresa { get; set; }
        public string facCategoria { get; set; }        
    }
}
