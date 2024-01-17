using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _repository;

    public PlatformsController(IPlatformRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public async Task<ActionResult<Platform>> GetPlatformById(string id)
    {
        var platform = await _repository.GetPlatformById(id);

        if (platform is not null)
        {
            return Ok(platform);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<Platform> CreatePlatform(Platform platform)
    {
        _repository.CreatePlatform(platform);

        return CreatedAtRoute(nameof(GetPlatformById), new { platform.Id }, platform);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Platform>>> GetAllPlatforms()
    {
        var platforms = await _repository.GetAllPlatforms();

        if (platforms is null)
        {
            return NotFound();
        }

        return Ok(platforms);
    }
}