using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsSeguimiento
    {
        public clsLoginResponse LoginResponse { get; set; }
        public List<clsCarrito> ListListadoCarrito { get; set; }  
        public List<clsLineaListarResponse> ListListadoLinea { get; set; }
        public List<clsListarProductosResponse> ListListadoProductosLinea { get; set; }
        public List<clsLinkSubgrupoCabeceraResponse> ListListadoLink { get; set; }
        public List<clsClienteResponsableCategoriaGrupoResponse> ListListadoVendedor { get; set; }
        public List<clsAgenciaListarResponse> ListListadoAgencias { get; set; }
        public List<clsGrupoEquipoListarResponse> ListListadoGrupoEquipo { get; set; }
        public List<clsClienteResponsableUsuarioVendedorListarResponse> ListListadoVendedorCliente { get; set; }

    }
}
