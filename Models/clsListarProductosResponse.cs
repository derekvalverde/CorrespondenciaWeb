using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsListarProductosResponse
    {
       
        public int Id { get; set; }
        public string usuId { get; set; }
        public string origen { get; set; }
        //Ofertas
        //PrefCliente
        //PrefSector
        //Buscador   
        //ProductosLinea
        public string desLinea { get; set; }
        public string matCodigo { get; set; }
        public string matNombre { get; set; }
        public string secCodigo { get; set; }
        public string mavPrecio { get; set; }
        public string mavLinea { get; set; }
        public string mavFamilia { get; set; }      
        public string mavOrigen { get; set; }
        public int mavExistencia { get; set; }
        public decimal madDescuento { get; set; }
        public string matImagen { get; set; }
        public int mprCantidad { get; set; }
        public int cantidadComprada { get; set; }
    }
}

