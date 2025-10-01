using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.ResponseDTO.Sms;
using Parentee_BE.DAL.Data.SmsDTO;

namespace Parentee_BE.BLL.Services.Implements;

public class SpeedSmsClient : ISmsSender
{
    private readonly HttpClient _http;
    private readonly SpeedSmsOptions _opts;

    public SpeedSmsClient(HttpClient http, IOptions<SpeedSmsOptions> opts)
    {
        _http = http;
        _opts = opts.Value;

        var token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_opts.ApiToken}:x"));
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        _http.BaseAddress = new Uri(_opts.BaseUrl.TrimEnd('/') + "/");
    }

    public async Task<SpeedSmsSendResult> SendAsync(IEnumerable<string> to, string content, int? smsType = null, string? sender = null, CancellationToken ct = default)
    {
        var payload = new
        {
            to = to.ToArray(),         
            content,                   
            sms_type = smsType ?? _opts.DefaultSmsType,
            sender = sender ?? _opts.Sender 
        };

        using var req = new HttpRequestMessage(HttpMethod.Post, "sms/send")
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };

        using var res = await _http.SendAsync(req, ct);
        var json = await res.Content.ReadAsStringAsync(ct);

        var dto = JsonSerializer.Deserialize<SpeedSmsSendResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                  ?? throw new InvalidOperationException("Empty response from SpeedSMS");

        if (!res.IsSuccessStatusCode || dto.Status != "success")
        {
            throw new InvalidOperationException($"SpeedSMS error {dto.Code}: {dto.Message}");
        }

        var data = dto.Data.InvalidPhone == null ? new List<string>() : dto.Data.InvalidPhone;
        return new SpeedSmsSendResult(dto.Data!.TranId, dto.Data.TotalSMS, dto.Data.TotalPrice, data);
    }

    public async Task<IReadOnlyList<SpeedSmsStatusItem>> GetStatusAsync(long tranId, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"sms/status/{tranId}");
        using var res = await _http.SendAsync(req, ct);
        var json = await res.Content.ReadAsStringAsync(ct);

        var dto = JsonSerializer.Deserialize<SpeedSmsStatusResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                  ?? throw new InvalidOperationException("Empty response from SpeedSMS");

        if (!res.IsSuccessStatusCode || dto.Status != "success")
            throw new InvalidOperationException($"SpeedSMS error {dto.Code}: {dto.Message}");

        return dto.Data ?? new List<SpeedSmsStatusItem>();
    }
}