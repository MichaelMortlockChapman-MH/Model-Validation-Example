using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            var model = new Models.TestModel();

            model.Applicants = new();
            model.Applicants.Add(new ApplicantModel
            {
                Name = "Test1",
            });
            model.Applicants.Add(new ApplicantModel
            {
                Name = "Test2",
            });
            model.Applicants.Add(new ApplicantModel
            {
                Name = "Test3",
            });
            model.Applicants.Add(new ApplicantModel
            {
                Name = "Test4",
            });

            model.CheckboxList = [false, false, false, false];

            model.RadioList = [1, 6, 20, 52];

            return View("~/Views/Home/Application.cshtml", model);
        }

        [HttpPost("[action]")]
        public IActionResult Create(Models.TestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Application.cshtml", model);
            }

            // save or process the model
            return RedirectToAction("Success");
        }
    }
}
