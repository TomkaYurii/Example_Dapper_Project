using Dapper_Example_Project.Entities;
using Dapper_Example_Project.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace restapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private IUnitOfWork _unitofWork;
        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            IEnumerable<Product> results = await _unitofWork._productRepository.GetAllAsync();
            _unitofWork.Commit();
            return results;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<Product> GetAsync(int id)
        {
            Product result = await _unitofWork._productRepository.GetAsync(5);
            _unitofWork.Commit();
            return result;
        }

        [HttpGet("category/{category_id}")]
        public async Task<IEnumerable<Product>> GetByCategoryId(int category_id)
        {
            IEnumerable<Product> results = await _unitofWork._productRepository.ProductByCategoryASync(category_id);
            _unitofWork.Commit();
            return results;
        }

        // POST: api/Product
        [HttpPost]
        public async Task Post([FromBody] Product newProduct)
        {
            await _unitofWork._productRepository.AddAsync(newProduct);
            _unitofWork.Commit();
        }

        [HttpPut]
        public async Task Put([FromBody] Product updateProduct)
        {
            await _unitofWork._productRepository.ReplaceAsync(updateProduct);
            _unitofWork.Commit();

        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _unitofWork._productRepository.DeleteAsync(id);
            _unitofWork.Commit();
        }
    }
}

