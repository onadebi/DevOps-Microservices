using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        public AppDbContext _context { get; }
        public CommandRepo(AppDbContext _context)
        {
            this._context = _context;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));
            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform plat)
        {
            if(plat == null) throw new ArgumentNullException(nameof(plat));
            _context.Platforms.Add(plat);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms(CancellationToken ct)
        {
           return await _context.Platforms.ToListAsync(ct);
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c=> c.Id == commandId && c.PlatformId== platformId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
            .Where(c=> c.PlatformId == platformId)
            .OrderBy(o=> o.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(p=> p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}