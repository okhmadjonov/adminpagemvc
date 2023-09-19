using AdminPageMVC.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdminPageMVC.Controllers
{
    public class DemosController : Controller
    {
        public IActionResult Index()
        {
            var result = GetDetails();
            return View(result);
        }

        public List<Demo> GetDetails()
        {

            var result = new List<Demo>();

            result.Add(new Demo { Id = 1, Name = "Alex", Address = "Mumbay" });
            result.Add(new Demo { Id = 2, Name = "John", Address = "England" });
            result.Add(new Demo { Id = 3, Name = "Messi", Address = "Argentina" });
            result.Add(new Demo { Id = 4, Name = "Ronaldo", Address = "Portugal" });
            result.Add(new Demo { Id = 5, Name = "Neymar", Address = "Brazil" });


            return result;
        }


    }
}
