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
[Route("api/models")]
public class ModelController : MainController
{
    private readonly IModelRepository _modelRepository;

    public ModelController(IModelRepository modelRepository) 
    {
        _modelRepository = modelRepository;
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    [HttpGet(Name = "ModelControllerGetAll")]
    public async Task<ActionResult<IEnumerable<object>>> GetAll()
    {
        var entities = await _modelRepository.GetAllAsync();

        if (entities.Count == 0) { return NoContent(); }

        return Ok(
            entities
            .Select(b => MapEntity(b))
            .ToList()
        );
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    [HttpGet("{id}", Name = "ModelControllerGetById")]
    public async Task<ActionResult<object>> GetById(string id)
    {
        var entity = await _modelRepository.GetByIdAsync(id);

        if (entity == null) {
            return NotFound();
        }

        return Ok( 
            MapEntity(entity) 
        );
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ModelEntity>> Create(
        ModelEntity request) 
    {
        if (request.Id != null) {
            return BadRequest(
                "Não é possível informar um ID para essa operação."
            );
        }

        var response = await _modelRepository
            .CreateAsync(request);

        return CreatedAtRoute(
            "ModelControllerGetById",
            new {id = response.Id},
            MapEntity(response)
        );
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        ModelEntity request) 
    {
        if (request.Id == null || request.Id != id) {
            return BadRequest(
                "ID não informado no corpo da requisição"
                + " ou divergente com a rota informada."
            );
        }

        if( await _modelRepository.UpdateAsync(request) ){
            return Ok();
        }

        return NotFound();
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if( await _modelRepository.DeleteAsync(id) ) {
            return NoContent();
        }
        
        return NotFound("Nenhum registro com o ID informado.");
    }
}