using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INTI_PP.Models;
using INTI_PP.Services;
using Microsoft.AspNetCore.Mvc;

namespace INTI_PP.ViewComponents
{
    public class AgregarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var carrito = new clsCarrito();
            
            return View(carrito);
        }
    }
}
