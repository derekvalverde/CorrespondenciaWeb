using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsLinkSubgrupoCabeceraResponse
    {
        public int likId { get; set; }
        public string likCabecera { get; set; }
        public string likAlias { get; set; }
        public string likDireccion { get; set; }
        public string likNombre { get; set; }
        public List<clsLinkSubgruposubMenuResponse> subMenu { get; set; }
    }
}
