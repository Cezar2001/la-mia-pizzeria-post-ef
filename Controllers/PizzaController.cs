using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using System.Diagnostics;
using la_mia_pizzeria_static;


namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
    public static PizzaContext db = new PizzaContext();

    public static ListaPizze Pizze = null;
        public IActionResult Index()
        {
            if (Pizze == null)
            {
                //Pizza quattroFormaggi = new Pizza("Pizza Quattro Formaggi", "pomodoro , mozzarella campana , basilico", "/Img/pizza-quattro-formaggi.jfif", 3);
                //Pizza capricciosa = new Pizza("Pizza Capriciosa", "Che soddisfa ogni capriccio", "/Img/pizza-quattro-stagioni.jfif", 4);
                //Pizza salsicciaPatate = new Pizza("Pizza Margherita", "salsiccia , patate , mozzarella capana", "/Img/pizza-margherita.jfif", 5);
                //Pizza marinara = new Pizza("Pizza Marinara", "Grande classico", "/Img/pizza-margherita.jfif", 3);
                //Pizza quattroStagioni = new Pizza("Pizza Quattro Stagioni", "La quattro Stagioni", "/Img/pizza-quattro-stagioni.jfif", 4);

                //Pizze.ListaDiPizze.Add(quattroFormaggi);
                //Pizze.ListaDiPizze.Add(capricciosa);
                //Pizze.ListaDiPizze.Add(salsicciaPatate);
                //Pizze.ListaDiPizze.Add(marinara);
                //Pizze.ListaDiPizze.Add(quattroStagioni);

                //db.Add(quattroFormaggi);
                //db.Add(capricciosa);
                //db.Add(salsicciaPatate);
                //db.Add(marinara);
                //db.Add(quattroStagioni);
                //db.SaveChanges();
            }

            return View(db);
        }

        public IActionResult Show(int Id)
        {
            var PizzaId = db.Pizze.Where(x => x.Id == Id).FirstOrDefault();
            return View("Show", PizzaId);
        }

        public IActionResult CreaFormPizza()
        {
            Pizza NuovaPizza = new Pizza()
            {
                Nome = "",
                Descrizione = "",
                sFoto = "",
                Prezzo = 1,
            };
            return View(NuovaPizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreaSchedaPizza(Pizza DatiPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("CreaFormPizza", DatiPizza);
            }

            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File");
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileInfo fileInfo = new FileInfo(DatiPizza.Foto.FileName);
            string fileName = DatiPizza.Nome + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                DatiPizza.Foto.CopyTo(stream);
            }

            Pizza nuovaPizza = new Pizza()
            {
                Nome = DatiPizza.Nome,
                Descrizione = DatiPizza.Descrizione,
                sFoto = "/File/" + fileName,
                Prezzo = DatiPizza.Prezzo,
            };

            db.Add(nuovaPizza);
            db.SaveChanges();
            return View(nuovaPizza);
        }

        public IActionResult Edit(int Id)
        {
            var PizzaId = db.Pizze.Where(x => x.Id == Id).FirstOrDefault();
            return View("Edit", PizzaId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificaPizza(Pizza DatiPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("CreaFormPizza", DatiPizza);
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File");
            //crea folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //get file extension

            FileInfo fileInfo = new FileInfo(DatiPizza.Foto.FileName);
            string fileName = DatiPizza.Nome + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                DatiPizza.Foto.CopyTo(stream);
            }



            var updatePizza = db.Pizze.Where(pizza => pizza.Id == DatiPizza.Id).FirstOrDefault();

            updatePizza.Nome = DatiPizza.Nome;
            updatePizza.Descrizione = DatiPizza.Descrizione;
            updatePizza.sFoto = "/File/" + fileName;
            updatePizza.Prezzo = DatiPizza.Prezzo;
            db.Pizze.UpdateRange(updatePizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var PizzaId = db.Pizze.Where(x => x.Id == Id).FirstOrDefault();
            db.Pizze.Remove(PizzaId);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
