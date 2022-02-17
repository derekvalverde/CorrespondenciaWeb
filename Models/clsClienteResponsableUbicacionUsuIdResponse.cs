using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsClienteResponsableUbicacionUsuIdResponse
    {
        public string cliNombreComercial { get; set; }
        public string cliNombreFiscal { get; set; }
        public string cliCodigo { get; set; }
        public Decimal cluLatitud { get; set; }
        public decimal cluLongitud { get; set; }
        public string cliDireccionComercial { get; set; }
    }
}
