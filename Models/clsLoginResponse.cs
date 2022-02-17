using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsLoginResponse
    {
        public int Id { get; set; }

        public int usuId { get; set; }
        public int ageId { get; set; }
        public string usuCodigo { get; set; }
        public string usuNombre { get; set; }
        public string sgrTipoNombre { get; set; }
        public string grpInicio { get; set; }
        public int sgrId { get; set; }
        public int uscId { get; set; }
        public string ageOficina { get; set; }        
        public string Token { get; set; }

        


        //

        

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
