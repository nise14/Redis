using RedisAPI.Models;

namespace RedisAPI.Data;

public interface IPlatformRepository
{
    void CreatePlatform(Platform platform);
    Task<Platform?> GetPlatformById(string id);
    Task<IEnumerable<Platform?>?> GetAllPlatforms();
}