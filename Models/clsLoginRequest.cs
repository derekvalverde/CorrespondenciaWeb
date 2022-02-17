using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsLoginRequest
    {
        public string Usuario { get; set; }
        public string Contra { get; set; }
        public int GrpId { get; set; }
    }
}
