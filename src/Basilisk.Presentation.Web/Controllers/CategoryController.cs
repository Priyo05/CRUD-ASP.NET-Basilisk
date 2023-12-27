using Basilisk.Business.Repositories;
using Basilisk.Presentation.Web.Service;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("Category")]
    /*[Route("cat")]*/
    public class CategoryController : Controller
    {
        // jika dalam route diberi "/" didpean /front-page
        // maka front-page seperti menjadi rootnya
        /*[Route("/front-page")]*/
        /*[HttpGet("test")]*/

        private readonly ILogger<CategoryController> _logger;

        private readonly CategoryService _services;

        //Depedency injection untuk si ILogger
        public CategoryController(ILogger<CategoryController> logger, CategoryService categoryService)
        {
            _logger = logger;
            _services = categoryService;
        }



        // langsung ke repository
        /*        [HttpGet("Index")]
                public IActionResult Index()
                {
                    _categoryRepository.GetAllCategory(50_000)
                        .ForEach(c => _logger.LogInformation(c.ToString()));
                    return View();
                }*/

        //==================================================


        // code ke categoryservice terlebih dahulu untul logicnya 
        // setelah itu dia akan ambil data ke category repository
        /*        [HttpGet("")]
                //pada browser ?name=a&description=b
                public IActionResult Index(string? name = "",
                                   string? description = "")
                { //jika null, akan ditampilkan kosong
                    var vm = _services.GetAll(name, description); //Category view model dalam bentuk list
                                                                  //_service.GetAll(name, description)                                      
                                                                  //    .ForEach(c => _logger.LogInformation($"Id: {c.Id} \n " +
                                                                  //                                         $"Name: {c.Name}\n " +
                                                                  //                                         $"Description: {c.Description}"));
                                                                  //tampilan console

                    return View(vm);
                }*/

        [HttpGet("Index")]
        public IActionResult Index(string? name, int pageSize = 7, int pageNumber = 1)
        {

            var vm = _services.GetAll(pageNumber, pageSize, name);
            return View(vm);
        }

        [HttpGet("Insert")]
        public IActionResult Insert()
        {

            return View("Upsert");
        }

        [HttpPost("Insert")]
        public IActionResult Insert(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Upsert");
            }
            _services.InsertCategory(vm);
            return RedirectToAction("Index");
        }

        /*        [HttpGet("Update/{id}")]
                public IActionResult Update(long id)
                {
                    CategoryViewModel vm =  _services.GetByID(id);
                    return View("Upsert",vm);
                }

                [HttpPost("Update/{id}")]
                public IActionResult Update(CategoryViewModel vm)
                {
                    _services.UpdateCategory(vm);
                    return RedirectToAction("Index");
                }*/

        [HttpGet("Update/{id}")]
        public IActionResult Update(long id)
        {
            CategoryViewModel vm = _services.GetByID(id);
            return View("Update", vm);
        }

        [HttpPost("Update/{id}")]
        public IActionResult Update(CategoryViewModel vm)
        {
            _services.UpdateCategory(vm);
            return RedirectToAction("Index");
        }


        [HttpGet("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            int result = _services.DeleteCategory(id);
            if (result > 0)
            {
                return View("Delete",result);
            }
            else
            {
                return RedirectToAction("Index"); 
            }
        }


        [HttpGet("Relationship/{id}")]
        public IActionResult Relationship(long id)
        {
          /*  var category = _services.GetByID(id);*/
/*            if (category == null)
            {
                return NotFound();
            }*/


            var products = _services.GetAllProduct(id);
            return View(products);
        }



    }
}
