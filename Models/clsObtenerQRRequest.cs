using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.Models
{
    public class clsObtenerQRRequest
    {

       public string currency { set; get; }
       public string gloss { get; set; }
       public Decimal amount { get; set; }
       public Boolean singleUse { get; set; }
       public string expirationDate { get; set; }
    }
}
