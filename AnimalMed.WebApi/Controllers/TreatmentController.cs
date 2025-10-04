using Microsoft.AspNetCore.Mvc;
using AnimalMed.Application.Data.Repositories;
namespace AnimalMed.WebApi.Controllers
{
    public class TreatmentController : ControllerBase
    {
        private readonly ILogger<TreatmentController> _logger;
        private readonly ITreatmentRepository _treatmentRepository;
        public TreatmentController(ITreatmentRepository treatmentRepository, ILogger<TreatmentController> logger) 
        { 
            _logger = logger;
            _treatmentRepository = treatmentRepository;
        }

    }
}
