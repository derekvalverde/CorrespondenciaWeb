using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Net;
namespace INTI_PP.Models
{
    public class clsCarrito
    {
        public int Id { get; set; }
        public string usuId { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }


        public string origen { get; set; }
        //Ofertas
        //PrefCliente
        //PrefSector
        //Buscador            
        public string secCodigo { get; set; }
        public string mavLinea { get; set; }
        public string mavFamilia { get; set; }
        public string mavOrigen { get; set; }

        public int mavExistencia { get; set; }
        public decimal madDescuento { get; set; }
        public string matImagen { get; set; }
        public int cantidadMaxima { get; set; }
        public int cantidadComprada { get; set; }

    }
}
