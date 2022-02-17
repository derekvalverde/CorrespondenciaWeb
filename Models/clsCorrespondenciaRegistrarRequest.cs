using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCorrespondenciaRegistrarRequest
    {
        public string UsuCodigo { get; set; }
        public string CorCodigo { get; set; }
        public string CorNumGuia { get; set; }
        public int CodId { get; set; }
        public int AreId { get; set; }

        public string CorRemitente { get; set; }
        public string CorUrgente { get; set; }
    }
}
