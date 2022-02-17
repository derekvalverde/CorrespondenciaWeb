using INTI_PP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.ViewComponents
{
    public class QRViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var qr = new clsCarteraListarPendienteResponce();

            return View(qr);
        }
    }
}
