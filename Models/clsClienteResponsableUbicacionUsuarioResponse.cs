using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsClienteResponsableUbicacionUsuarioResponse
    {
        public string cliNombreComercial { get; set; }
        public string cliNombreFiscal { get; set; }
        public string cliCodigo { get; set; }
        public decimal cluLatitud { get; set; }
        public decimal cluLongitud { get; set; }
        public string cliDireccionComercial { get; set; }
        public string imagen { get; set; }
        public string usuNombre { get; set; }

    }
}
