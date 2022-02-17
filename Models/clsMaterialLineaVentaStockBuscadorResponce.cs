using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsMaterialLineaVentaStockBuscadorResponce
    {
        public string matCodigo { get; set; }
        public string matNombre { get; set; }
        public string secCodigo { get; set; }
        public decimal mavPrecio { get; set; }
        public string mavLinea { get; set; }
        public string mavFamilia { get; set; }
        public string mavOrigen { get; set; }
        public decimal madDescuento { get; set; }
        public string matImagen { get; set; }
    }
}
