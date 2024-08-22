using Microsoft.AspNetCore.Mvc;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogisticController : ControllerBase
    {
        private readonly ILogisticService _logisticService;

        public LogisticController(ILogisticService logisticService)
        {
            _logisticService = logisticService;
        }

        [HttpPost]
        public IActionResult RegisterMovement(Logistic logistic)
        {
            _logisticService.RegisterMovement(logistic);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetMovement(int id)
        {
            var movement = _logisticService.GetMovement(id);
            if (movement == null)
            {
                return NotFound();
            }
            return Ok(movement);
        }

        // Add other endpoints for managing logistics movements

        // Example endpoint for getting all incoming movements
        [HttpGet("incoming")]
        public IActionResult GetIncomingMovements()
        {
            var incomingMovements = _logisticService.GetIncomingMovements();
            return Ok(incomingMovements);
        }

        // Example endpoint for getting all outgoing movements
        [HttpGet("outgoing")]
        public IActionResult GetOutgoingMovements()
        {
            var outgoingMovements = _logisticService.GetOutgoingMovements();
            return Ok(outgoingMovements);
        }

        // Example endpoint for getting all movements between warehouses
        [HttpGet("warehouse")]
        public IActionResult GetWarehouseMovements()
        {
            var warehouseMovements = _logisticService.GetWarehouseMovements();
            return Ok(warehouseMovements);
        }
    }
}