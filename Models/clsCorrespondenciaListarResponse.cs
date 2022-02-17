using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCorrespondenciaListarResponse
    {
        public string usuCodigo { get; set; }
        public int Corid { get; set; }
        public string CorCodigo { get; set; }

        public string CorNumGuia { get; set; }
        public string CorRemitente { get; set; }

        public string CodNombre { get; set; }
        public string AreCodigo { get; set; }
        public DateTime CorFechaIni { get; set; }

        public string CorEstado { get; set; }
        public string Urgente { get; set; }
        public int NDivisiones { get; set; }

    }
}
