using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
    {
    public class clsCorrespondenciaDetalleInformaciónResponse
    {
        public int cdeId { get; set; }

        public string codPadre { get; set; }
        public string cdeCodigo { get; set; }
        public string corRemitente { get; set; }
        public string cdeDetalles { get; set; }
        public DateTime corFechaIni { get; set; }
        public string cdeEstado { get; set; }

    }
}
