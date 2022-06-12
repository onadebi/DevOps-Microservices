using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controller;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;
    public PlatformsController(ICommandRepo _repo, IMapper _mapper)
    {
        this._mapper = _mapper;
        this._repo = _repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms(CancellationToken ct)
    {
        System.Console.WriteLine("-->Get platforms from CommandService");
        var platformItems = await _repo.GetAllPlatforms(ct);
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inboud Post # Command");
        return Ok("Inbound test of from platforms controller");
    }


}