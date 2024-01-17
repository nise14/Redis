using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data;

public class PlatformRepository : IPlatformRepository
{
    private readonly IConnectionMultiplexer _redis;

    public PlatformRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform is null)
        {
            throw new ArgumentOutOfRangeException(nameof(platform));
        }

        var db = _redis.GetDatabase();

        var serialPlatform = JsonSerializer.Serialize(platform);

        // db.StringSet(platform.Id, serialPlatform);
        // db.SetAddAsync("PlatformsSet", serialPlatform);

        db.HashSet("hashplatform", new HashEntry[]{
            new HashEntry(platform.Id,serialPlatform)
        });
    }

    public async Task<IEnumerable<Platform?>?> GetAllPlatforms()
    {
        var db = _redis.GetDatabase();

        // var completeSet = await db.SetMembersAsync("PlatformsSet");

        //  if (completeSet.Length > 0)
        // {
        //     var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Platform>(val!)).ToList();

        //     return obj;
        // }

        var completeHash = await db.HashGetAllAsync("hashplatform");

        if (completeHash.Length > 0)
        {
            var obj = Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val.Value!)).ToList();

            return obj;
        }

        return null;
    }

    public async Task<Platform?> GetPlatformById(string id)
    {
        var db = _redis.GetDatabase();

        // var platform = db.StringGet(id);
        var platform = await db.HashGetAsync("hashplatform", id);

        if (!string.IsNullOrWhiteSpace(platform))
        {
            return JsonSerializer.Deserialize<Platform>(platform!);
        }

        return null;
    }
}