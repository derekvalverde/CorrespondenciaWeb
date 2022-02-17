using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsArchivero
    {
        public clsLoginResponse LoginResponse { get; set; }

        public List<clsLinkSubgrupoCabeceraResponse> ListListadoLink { get; set; }

        public List<clsArchiveroListarResponse> ListListadoArchivero { get; set; }

    }
}
