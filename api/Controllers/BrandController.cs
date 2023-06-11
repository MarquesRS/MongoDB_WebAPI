using api.Entities;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/brands")]
public class BrandController : ControllerBase
{
    private readonly IBrandRepository _brandRepository;

    public BrandController(IBrandRepository brandRepository) 
    {
        _brandRepository = brandRepository;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<IEnumerable<BrandEntity>>> GetAll() 
    {
        return Ok(await _brandRepository.GetAllAsync());
    }


    [HttpGet("{brandId}")]
    public async Task<ActionResult<BrandEntity>> GetById(string brandId)
    {
        return Ok(await _brandRepository.GetByIdAsync(brandId));
    }

    [HttpPost]
    public async Task<ActionResult<BrandEntity>> Create(
        BrandEntity brandFromRequest) 
    {
        await _brandRepository.CreateAsync(brandFromRequest);

        return CreatedAtRoute(
            nameof(GetAll),
            new {brandId = brandFromRequest.Id},
            brandFromRequest
        );
    }

    [HttpPut("{brandId}")]
    public async Task<IActionResult> Update(
        string brandId,
        BrandEntity brandFromRequest) 
    {
        if (brandId != brandFromRequest.Id) {
            return BadRequest();
        }

        await _brandRepository.UpdateAsync(brandFromRequest);

        return Ok();
    }

    [HttpDelete("{brandId}")]
    public async Task<IActionResult> Delete(string brandId)
    {
        await _brandRepository.DeleteAsync(brandId);
        
        return NoContent();
    }
}