using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsArchiveroAgregarRequest
    {
        public string UsuCodigo { get; set; }
        public string Codigo { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int AreId { get; set; }

    }
}
