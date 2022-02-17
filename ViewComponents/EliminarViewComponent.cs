using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.ViewComponents
{
    public class EliminarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var eliminar = new Models.clsCarrito();

            return View(eliminar);
        }
    }
}
