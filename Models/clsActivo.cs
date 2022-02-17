using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsActivo
    {
        public clsLoginResponse LoginResponse { get; set; }

        public List<clsLinkSubgrupoCabeceraResponse> ListListadoLink { get; set; }

        public List<clsActivoUbicacionUsuarioListarResponse> ListListadoActivoUbicacion { get; set; }
        public List<clsActivoBuscarResponse> ListListadoActivoBuscador { get; set; }
        public List<clsActivoUbicacionHistoricoListarResponse> ListListadoRastreoActivo { get; set; }
    }
}
