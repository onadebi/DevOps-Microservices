using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{

    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepo _repo, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._repo = _repo;

        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            System.Console.WriteLine($"--> Requested platform Id is {platformId}");
            if (!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(_repo.GetCommandsForPlatform(platformId)));
        }

        [HttpGet("{command}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            if (!_repo.PlatformExists(platformId))
            {
                return NotFound($"Request with platform Id {platformId} not found!");
            }
            var commandItem = _repo.GetCommand(platformId, commandId);

            return commandItem == null ? NotFound() : Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            if(!_repo.PlatformExists(platformId)) return NotFound($"Platform with id {platformId} not found!");
            var command =  _mapper.Map<Command>(commandDto);
            _repo.CreateCommand(platformId,command);
            _repo.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform), new {platformId = platformId, commandId = commandReadDto.Id}, commandReadDto);
        }

    }
}