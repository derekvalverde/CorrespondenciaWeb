using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.ViewComponents
{
    public class ReglasCompraViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //var confirmar = new clsCarteraListarPendienteResponce();

            return View();
        }
    }
}
