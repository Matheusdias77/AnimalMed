using AnimalMed.Application.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AnimalMed.WebApi.Controllers
{
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILogger<AnimalController> _logger;
        public AnimalController(IAnimalRepository animalRepository, ILogger<AnimalController> logger)
        {
            _logger = logger;
            _animalRepository = animalRepository;
        }

    }
}
