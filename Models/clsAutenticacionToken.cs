using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsAutenticacionToken
    {
        public string Usuario { get; set; }
        public string aplicacion { get; set; }
        public string token { get; set; }
        public DateTime fechaExpiracion { get; set; }

    }
}
