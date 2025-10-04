using AnimalMed.Application.Data.Repositories;
using AnimalMed.Domain.Records;
using Microsoft.AspNetCore.Mvc;

namespace AnimalMed.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILogger<AnimalController> _logger;
        public AnimalController(IAnimalRepository animalRepository, ILogger<AnimalController> logger)
        {
            _logger = logger;
            _animalRepository = animalRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAnimal([FromBody] AnimalRecord record)
        {
            if (record == null)
                return BadRequest(ApiMessages.RegistroInvalido);

            try
            {
                var success = await _animalRepository.SaveAnimal(record);

                _logger.LogInformation(success
                    ? "Animal inserido com sucesso"
                    : "Falha ao inserir Animal");

                return success
                    ? Ok(new { message = ApiMessages.Sucesso })
                    : StatusCode(500, ApiMessages.FalhaInserirEstoque);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao Salvar um Animal");
                return StatusCode(500, ApiMessages.ErroGenerico);
            }
        }
    }
}
