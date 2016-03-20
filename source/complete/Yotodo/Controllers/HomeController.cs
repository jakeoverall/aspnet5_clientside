using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Yotodo.Data;

namespace Yotodo.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDataContext DataContext;
        
        public HomeController(ApplicationDataContext dataContext){
                DataContext = dataContext;
        }
        public IActionResult Index()
        {
           return RedirectToAction("Index", "Todos");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
