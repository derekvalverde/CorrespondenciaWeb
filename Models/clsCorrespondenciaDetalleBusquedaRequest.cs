using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCorrespondenciaDetalleBusquedaRequest
    {

        public string UsuCodigo { get; set; }
        public string codigo { get; set; }
        public string detalle { get; set; }
        public string FamTipo { get; set; }
        public string remitente { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFin { get; set; }
    }
}
