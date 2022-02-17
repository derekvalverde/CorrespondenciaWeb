using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCorrespondenciaDetalleBusquedaResponse
    {

        public int cdeId { get; set; }
        public string cdeCodigo { get; set; }
        public string cdeDestinatario { get; set; }
        public string cdeDetalles { get; set; }
        public string corRemitente { get; set; }
        public string corCodigo { get; set; }
        public DateTime cdeFechaIni { get; set; }

        public string usunombre { get; set; }
        public string couActual { get; set; }
        public string cdeEstado { get; set; }

    }
}
