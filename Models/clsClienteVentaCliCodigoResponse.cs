using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsClienteVentaCliCodigoResponse
    {
        public int Id { get; set; }
        public int cliId { get; set; }
        public string clvOficina { get; set; }
        public string cliCiudad { get; set; }
        public string cliCodigo { get; set; }
        public string cloInterlocutor { get; set; }
        public string cliNombreComercial { get; set; }
        public string cliNombreFiscal { get; set; }
        public string cliRol { get; set; }
        public string cliIdentificacionNumero { get; set; }
        public string cliDireccionComercial { get; set; }
        public string cliTelefono { get; set; }
        public string cliEmail { get; set; }
        public DateTime cliFechaModificacion { get; set; }
        public string cadCodigo { get; set; }
        public string secCodigo { get; set; }
        public string grpCodigo { get; set; }
        public string clvFormaPago { get; set; }
        public string clvZona { get; set; }
        public string clvGrupoVendedores { get; set; }
        public string clvEstado { get; set; }
        public string prgCodigo { get; set; }
        public Decimal cheMonto { get; set; }
        public Decimal perComprado { get; set; }

        public Decimal perLimite { get; set; }
    }
}
