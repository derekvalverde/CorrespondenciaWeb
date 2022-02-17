using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCorrespondencia
    {
        public clsLoginResponse LoginResponse { get; set; }
       

        
        

        public List<clsLinkSubgrupoCabeceraResponse> ListListadoLink { get; set; }

      
        public List<clsCorrespondenciaDetalleListarResponse> ListListadoCorrespondencia { get; set; }
        public List<clsCorrespondenciaListarResponse> ListListadoPaquetes { get; set; }


    }
}
