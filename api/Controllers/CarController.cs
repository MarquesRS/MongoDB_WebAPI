using api.Entities;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    public CarController(ICarRepository carRepository) 
    {
        _carRepository = carRepository;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<IEnumerable<CarEntity>>> GetAll() 
    {
        return Ok(await _carRepository.GetAllAsync());
    }


    [HttpGet("{carId}")]
    public async Task<ActionResult<CarEntity>> GetById(string carId)
    {
        return Ok(await _carRepository.GetByIdAsync(carId));
    }

    [HttpPost]
    public async Task<ActionResult<CarEntity>> Create(
        CarEntity carFromRequest) 
    {
        await _carRepository.CreateAsync(carFromRequest);

        return CreatedAtRoute(
            nameof(GetAll),
            new {carId = carFromRequest.Id},
            carFromRequest
        );
    }

    [HttpPut("{carId}")]
    public async Task<IActionResult> Update(
        string carId,
        CarEntity carFromRequest) 
    {
        if (carId != carFromRequest.Id) {
            return BadRequest();
        }

        await _carRepository.UpdateAsync(carFromRequest);

        return Ok();
    }

    [HttpDelete("{carId}")]
    public async Task<IActionResult> Delete(string carId)
    {
        await _carRepository.DeleteAsync(carId);
        
        return NoContent();
    }
}