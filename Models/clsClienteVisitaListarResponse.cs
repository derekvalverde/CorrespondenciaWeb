using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsClienteVisitaListarResponse
    {

        public string cliNombreComercial { get; set; }
        public string cliNombreFiscal { get; set; }
        public decimal clgLatitud { get; set; }
        public decimal clgLongitud { get; set; }
        public DateTime clgFechaGPS { get; set; }
        public decimal cluLatitud { get; set; }
        public decimal cluLongitud { get; set; }
        public decimal distancia { get; set; }
    }
}
