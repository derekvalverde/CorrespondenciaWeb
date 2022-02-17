using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsCorrespondenciaDetalle
    {
              
        public List<clsDestinatarioListarResponse> ListListadoDestinatarioListar { get; set; }
        public List<clsCorrespondenciaListarResponse> ListListadoPaquetes { get; set; }
    }
}
