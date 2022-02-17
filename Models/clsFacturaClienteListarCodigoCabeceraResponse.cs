﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsFacturaClienteListarCodigoCabeceraResponse
    {
        public int Id { get; set; }
        public string usuId { get; set; }
        public string facCodigo { get; set; }
        public string facNumero { get; set; }
        public decimal facMonto { get; set; }
        public DateTime facFecha { get; set; }
        public DateTime facFechaPago { get; set; }
        public string facEmpresa { get; set; }

        public string facCategoria { get; set; }
        public List<clsFacturaClienteListarCodigoDetalleResponse> detalleFactura { get; set; }
        public string color { get; set; }
    }
}
