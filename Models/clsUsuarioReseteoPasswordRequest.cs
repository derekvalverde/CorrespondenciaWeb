using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsUsuarioReseteoPasswordRequest
    {
        public int usuId { get; set; }
        public string usuPassword { get; set; }
        public string usuPasswordNuevo { get; set; }
        public string aplicacion { get; set; }
    }
}
