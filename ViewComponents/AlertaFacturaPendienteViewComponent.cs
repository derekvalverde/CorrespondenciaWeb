using INTI_PP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.ViewComponents
{
    public class AlertaFacturaPendienteViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //var confirmar = new clsCarteraListarPendienteResponce();

            return View();
        }
    }
}
