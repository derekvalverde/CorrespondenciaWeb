using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTI_PP.ViewComponents
{
    public class VisitaViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {


            return View();
        }
    }
}
