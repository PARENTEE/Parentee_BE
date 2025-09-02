using System.Text.Json;
using Parentee_BE.BLL.Services.Interfaces.OtpInterface;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.OtpDtos;
using StackExchange.Redis;

namespace Parentee_BE.BLL.Services.Implements.OtpImplement;

public class RedisOtpStore : IOtpStore
{
    private readonly IDatabase _db;
    public RedisOtpStore(IConnectionMultiplexer mux)
    {
        _db = mux.GetDatabase();
    }
    
    public async Task<OtpRecord?> GetAsync(string key)
    {
        var v = await _db.StringGetAsync(key);
        if (v.IsNullOrEmpty) return null;
        return JsonSerializer.Deserialize<OtpRecord>(v!);
    }

    public async Task PutAsync(string key, OtpRecord value, TimeSpan ttl)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, ttl);
    }

    public async Task DeleteAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}