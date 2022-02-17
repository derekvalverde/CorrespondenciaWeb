using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsQrAdicionarRequest
    {
        public int qrCodigo { get; set; }
        public string qrImagen { get; set; }
        public int usuId { get; set; }
    }
}
