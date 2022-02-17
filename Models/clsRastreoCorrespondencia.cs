using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsRastreoCorrespondencia
    {

        public List<clsCorrespondenciaDetalleInformaciónResponse> ListListadoDivisionInformacion { get; set; }

        public List<clsCorrespondenciaDetalleInformacionCelularResponse> ListListadoDivisionUbicacion { get; set; }
    }
}
