namespace RedisAPI.Models;

public class Platform
{
    public string Id { get; set; } = $"platform:{Guid.NewGuid()}";
    public string Name { get; set; } = null!;
}