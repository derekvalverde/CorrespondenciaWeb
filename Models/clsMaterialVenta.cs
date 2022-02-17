using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsMaterialVenta
    {
        public int Id { get; set; }
        public int matId { get; set; }
        public string secCodigo { get; set; }
        public string matCodigo { get; set; }
        public string matNombre { get; set; }
        public string matEstado { get; set; }
        public decimal masCantidad { get; set; }
        public decimal marValor { get; set; }
        public string masEstado { get; set; }
        public string mavLinea { get; set; }
        public string mavFamilia { get; set; }
        public string mavOrigen { get; set; }
        public DateTime masFechaVencimiento { get; set; }
        public string mavCentro { get; set; }
        public string secDetalle { get; set; }
        public string cadCodigo { get; set; }
        public string cadDetalle { get; set; }
        public string linDescripcion { get; set; }
        public string mafDetalle { get; set; }
    }
}
