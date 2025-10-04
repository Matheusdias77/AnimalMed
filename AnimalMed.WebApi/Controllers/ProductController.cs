using Microsoft.AspNetCore.Mvc;
using AnimalMed.Application.Data.Repositories;
namespace AnimalMed.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _produtoRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository produtoRepository, ILogger<ProductController> logger)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }
    }
}
