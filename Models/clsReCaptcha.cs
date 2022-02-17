using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsReCaptcha
    {
        public static string Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string PrivateKey = "6LdJRdkZAAAAALzX66hynkIPS2BpNv8sUTwJaS6U";

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<clsReCaptcha>(GoogleReply);

            return captchaResponse.Success.ToLower();
        }

       
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }


        private List<string> m_ErrorCodes;
    }
}
