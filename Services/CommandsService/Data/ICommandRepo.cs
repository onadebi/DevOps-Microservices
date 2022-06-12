using System.Collections.Generic;
using CommandsService.Models;
namespace CommandsService.Data
{
    public interface ICommandRepo
    {
         bool SaveChanges();

         //Platforms
         Task<IEnumerable<Platform>> GetAllPlatforms(CancellationToken ct = default(CancellationToken));
         void CreatePlatform(Platform plat);
         bool PlatformExists(int platformId);

         //Commands
         IEnumerable<Command> GetCommandsForPlatform(int platformId);
         Command GetCommand(int platformId, int commandId);
         void CreateCommand(int platformId, Command command);

    }
}