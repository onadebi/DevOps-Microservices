using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _platRepo;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo platRepo, IMapper mapper)
    {
        this._mapper = mapper;
        _platRepo = platRepo;
    }
    [HttpGet(Name = "GetPlatforms")]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var objResp = _mapper.Map<IEnumerable<PlatformReadDto>>(_platRepo.GetAllPlatforms());
        return Ok(objResp);
    }

    [HttpGet("{Id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int Id)
    {
        var platformItem = _mapper.Map<PlatformReadDto>(_platRepo.GetPlatformById(Id));
        if (platformItem != null)
        {
            return Ok(platformItem);
        }
        return NotFound($"Platform with id {Id} not found");
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _platRepo.CreatePlatform(platformModel);
        _platRepo.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }

}