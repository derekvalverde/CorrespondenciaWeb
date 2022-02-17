using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsHistorialPedido
    {
        public List<clsPedidoSolicitudResponse> ListHistorialPedido { get; set; }
        public List<clsLinkSubgrupoCabeceraResponse> ListListadoLink { get; set; }
    }
}
