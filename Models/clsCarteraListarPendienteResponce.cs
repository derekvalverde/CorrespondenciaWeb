using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCarteraListarPendienteResponce
    {
        public int clcId { get; set; }
        public string cliCodigo { get; set; }
        public string clcDocumento { get; set; }
        public string facCodigo { get; set; }
        public string facOrigen { get; set; }
        public string clcReferencia { get; set; }
        public DateTime clcFechaBase { get; set; }
        public DateTime clcFechaPago { get; set; }
        public decimal clcMonto { get; set; }
        public decimal clcMontoPago { get; set; }
        public int clcCampana { get; set; }
        public DateTime clcFechaCategoriaA { get; set; }
        public DateTime clcFechaCategoriaB { get; set; }
        public string color { get; set; }
    }
}
