using api.Entities;
using api.Models;
using api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
 using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api.Controllers;

[ApiController]
[Route("api/brands")]
public class BrandController : MainController
{
    private readonly IBrandRepository _brandRepository;

    public BrandController(IBrandRepository brandRepository, IMapper mapper) 
    {
        _brandRepository = brandRepository;
    }

    [HttpGet(Name = "BrandControllerGetAll")]
    public async Task<ActionResult<IEnumerable<object>>> GetAll()
    {
        var brandsEntities = await _brandRepository.GetAllAsync();

        if (brandsEntities.Count == 0) { return NoContent(); }

        return Ok(
            brandsEntities
            .Select(b => MapEntity(b))
            .ToList()
        );
    }

    [HttpGet("{brandId}", Name = "BrandControllerGetById")]
    public async Task<ActionResult<object>> GetById(string brandId)
    {
        var brandEntity = await _brandRepository.GetByIdAsync(brandId);

        if (brandEntity == null) {
            return NotFound();
        }

        return Ok( 
            MapEntity(brandEntity) 
        );
    }

    [HttpPost]
    public async Task<ActionResult<BrandEntity>> Create(
        BrandEntity brandRequest) 
    {
        if (brandRequest.Id != null) {
            return BadRequest(
                "Não é possível informar um ID para essa operação."
            );
        }

        var brandResponse = await _brandRepository
            .CreateAsync(brandRequest);

        return CreatedAtRoute(
            "BrandControllerGetById",
            new {brandId = brandResponse.Id},
            MapEntity(brandResponse)
        );
    }

    [HttpPut("{brandId}")]
    public async Task<IActionResult> Update(
        string brandId,
        BrandEntity brandRequest) 
    {
        if (brandRequest.Id == null || brandRequest.Id != brandId) {
            return BadRequest(
                "ID não informado no corpo da requisição"
                + " ou divergente com a rota informada."
            );
        }

        if( await _brandRepository.UpdateAsync(brandRequest) ){
            return Ok();
        }

        return NotFound();
    }

    [HttpDelete("{brandId}")]
    public async Task<IActionResult> Delete(string brandId)
    {
        if( await _brandRepository.DeleteAsync(brandId) ) {
            return NoContent();
        }
        
        return NotFound("Nenhum registro com o ID informado.");
    }
}