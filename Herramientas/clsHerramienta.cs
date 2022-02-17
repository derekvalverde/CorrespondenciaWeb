using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace INTI_PP.Herramientas
{
    public class clsHerramienta
    {
        public string ObtRazon(string cadena)
        {

            string[] campos = cadena.Split("%%%");


            return campos[1];
        }
        public bool URLExists(string url)
        {
            bool result = true;

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 5000; // miliseconds
            webRequest.Method = "HEAD";

            try
            {
                webRequest.GetResponse();
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
