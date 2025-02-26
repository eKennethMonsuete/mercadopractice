using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVICE.ProductService;
using SERVICE.ProductService.DTO.Request;

namespace API.Controllers
{
    
    [ApiController]
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMeasures([FromBody] ProductCreateDTO input) => Ok(await _productService.Create(input));


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _productService.FindAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) => Ok(await _productService.FindByIdAsync(id));
    }
}
