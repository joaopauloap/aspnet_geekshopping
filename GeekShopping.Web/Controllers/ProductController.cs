using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(product);
                if (response != null) return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(long id)
        {
            var product = await _productService.FindProductById(id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(product);
                if (response != null) return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Excluir(long id)
        {
            var response = await _productService.DeleteProductById(id);
            return RedirectToAction("Index");
        }

    }
}
