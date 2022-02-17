using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCorrespondenciaDetalleRegistrarRequest
    {

        public string usuCodigo { get; set; }
        public int corId { get; set; }
        public string cdeCodigo { get; set; }
        public int usuFinalId { get; set; }
        public string cdeDetalle { get; set; }
        public string cdeUrgente { get; set; }
    }
}
