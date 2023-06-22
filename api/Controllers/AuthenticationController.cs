using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/authentication")]
[Authorize]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public AuthenticationController(
        IConfiguration configuration,
        IUserRepository userRepository,
        TokenService tokenService
    )
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<string> Authenticate(UserDTO request) 
    {
        if (request == null) { return BadRequest(); };

        var userEntity = _userRepository.Get(
            request.Username, request.Password
        );

        if (userEntity == null) {return Unauthorized();}

        return Ok(_tokenService.GenerateToken(userEntity));
    }

}
