using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Net;

namespace INTI_PP.Models
{
    public class clsPedido
    {
        public clsLoginResponse LoginResponse { get; set; }
        public List<clsCarrito> ListListadoCarrito { get; set; }
        public List<clsListarProductosResponse> ListListadoBuscador { get; set; }
      //  public List<clsListarProductosResponse> ListListadoOfertas { get; set; }
      //  public List<clsListarProductosResponse> ListListadoPrefCliente { get; set; }
        //public List<clsListarProductosResponse> ListListadoPrefSector { get; set; }
        public List<clsLineaListarResponse> ListListadoLinea { get; set; }
        public List<clsListarProductosResponse> ListListadoProductosLinea { get; set; }

        public List<clsLinkSubgrupoCabeceraResponse> ListListadoLink { get; set; } 




    }
    
}
