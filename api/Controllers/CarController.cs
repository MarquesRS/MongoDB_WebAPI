using api.Entities;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
 using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api.Controllers;

[ApiController]
[Route("api/cars")]
public class CarController : MainController
{
    private readonly ICarRepository _carRepository;

    public CarController(ICarRepository carRepository) 
    {
        _carRepository = carRepository;
    }

    [Authorize]
    [HttpGet(Name = "CarControllerGetAll")]
    public async Task<ActionResult<IEnumerable<object>>> GetAll()
    {
        var entities = await _carRepository.GetAllAsync();

        if (entities.Count == 0) { return NoContent(); }

        return Ok(
            entities
            .Select(b => MapEntity(b))
            .ToList()
        );
    }

    [Authorize]
    [HttpGet("{id}", Name = "CarControllerGetById")]
    public async Task<ActionResult<object>> GetById(string id)
    {
        var entity = await _carRepository.GetByIdAsync(id);

        if (entity == null) {
            return NotFound();
        }

        return Ok( 
            MapEntity(entity) 
        );
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CarEntity>> Create(
        CarEntity request) 
    {
        if (request.Id != null) {
            return BadRequest(
                "Não é possível informar um ID para essa operação."
            );
        }

        var response = await _carRepository
            .CreateAsync(request);

        return CreatedAtRoute(
            "CarControllerGetById",
            new {id = response.Id},
            MapEntity(response)
        );
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        CarEntity request) 
    {
        if (request.Id == null || request.Id != id) {
            return BadRequest(
                "ID não informado no corpo da requisição"
                + " ou divergente com a rota informada."
            );
        }

        if( await _carRepository.UpdateAsync(request) ){
            return Ok();
        }

        return NotFound();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if( await _carRepository.DeleteAsync(id) ) {
            return NoContent();
        }
        
        return NotFound("Nenhum registro com o ID informado.");
    }
}