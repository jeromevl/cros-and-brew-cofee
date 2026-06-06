using Microsoft.AspNetCore.Mvc;

namespace Cros.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("new")]
        public IActionResult Add()
        {
            return View("AddEdit");
        }

        [Route("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            return View("AddEdit");
        }
    }
}