using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsArchiveroListarResponse
    {
        public string usuCodigo { get; set; }
         public string ArcCodigo { get; set; }
        public string ArcNombre { get; set; }
        public string ArcDescripcion { get; set; }
        public string AreCodigo { get; set; }
        public DateTime ArcFechaIni { get; set; }
    }
}
