using api.Entities;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/models")]
public class ModelController : ControllerBase
{
    private readonly IModelRepository _modelRepository;

    public ModelController(IModelRepository modelRepository) 
    {
        _modelRepository = modelRepository;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<IEnumerable<ModelEntity>>> GetAll() 
    {
        return Ok(await _modelRepository.GetAllAsync());
    }


    [HttpGet("{modelId}")]
    public async Task<ActionResult<ModelEntity>> GetById(string modelId)
    {
        return Ok(await _modelRepository.GetByIdAsync(modelId));
    }

    [HttpPost]
    public async Task<ActionResult<ModelEntity>> Create(
        ModelEntity modelFromRequest) 
    {
        await _modelRepository.CreateAsync(modelFromRequest);

        return CreatedAtRoute(
            nameof(GetAll),
            new {modelId = modelFromRequest.Id},
            modelFromRequest
        );
    }

    [HttpPut("{modelId}")]
    public async Task<IActionResult> Update(
        string modelId,
        ModelEntity modelFromRequest) 
    {
        if (modelId != modelFromRequest.Id) {
            return BadRequest();
        }

        await _modelRepository.UpdateAsync(modelFromRequest);

        return Ok();
    }

    [HttpDelete("{modelId}")]
    public async Task<IActionResult> Delete(string modelId)
    {
        await _modelRepository.DeleteAsync(modelId);
        
        return NoContent();
    }
}