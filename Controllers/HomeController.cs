using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {
        List<Info> InfoList = new List<Info>();

        public void PersonalData()
        {
            Info MyInfo = new Info()
            {
                Id = 1,
                Name = "Cezar",
                Surname = "Lantos",
            };
            InfoList.Add(MyInfo);
        }

        public IActionResult Index()
        {
            PersonalData();
            return View(InfoList);
        }
    }
}