using Microsoft.AspNetCore.Mvc;
using AnimalMed.Domain.Records;
using AnimalMed.Application.Data.Repositories;

namespace AnimalMed.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalMedController : ControllerBase
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly ILogger<AnimalMedController> _logger;

        public AnimalMedController(IEstoqueRepository estoqueRepository, ILogger<AnimalMedController> logger)
        {
            _estoqueRepository = estoqueRepository;
            _logger = logger;
        }

        [HttpPost("estoque")]
        public async Task<IActionResult> InsertEstoque([FromBody] EstoqueRecord record)
        {
            if (record == null)
                return BadRequest(ApiMessages.RegistroInvalido);

            try
            {
                var success = await _estoqueRepository.SaveEstoque(record);

                _logger.LogInformation(success
                    ? "Estoque inserido com sucesso: {Nome}"
                    : "Falha ao inserir estoque: {Nome}",
                    record.Nome);

                return success
                    ? Ok(new { message = ApiMessages.Sucesso })
                    : StatusCode(500, ApiMessages.FalhaInserirEstoque);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir estoque: {Nome}", record.Nome);
                return StatusCode(500, ApiMessages.ErroGenerico);
            }
        }

    }
}
