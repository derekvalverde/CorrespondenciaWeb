using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsDatosCliente
    {
        public string usuId { get; set; }
        public string usuNombre { get; set; }
        public string sgrTipoNombre { get; set; }
        public string sgrId { get; set; }
        public string ageId { get; set; }
        public string uscId { get; set; }
        public string ageOficina { get; set; }
        public string cliCodigo { get; set; }
        public string usuCorreo { get; set; }
        public string uclImei { get; set; }
        public string appVersion { get; set; }
        public string Token { get; set; }

        public string ObtRazon(string cadena)
        {

            string[] campos = cadena.Split("%%%");


            return campos[2];
        }
        public string ObtUsuId(string cadena)
        {

            string[] campos = cadena.Split("%%%");


            return campos[1];
        }
    }
}
