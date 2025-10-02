using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Sms;
using Parentee_BE.DAL.Data.ResponseDTO.Sms;
using Parentee_BE.DAL.Data.SmsDTO;

namespace Parentee_BE.BLL.Services.Implements;

public class SpeedSmsClient : ISmsSender
{
    private readonly HttpClient _http;
    private readonly SpeedSmsOptions _opts;
    
    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public SpeedSmsClient(HttpClient http, IOptions<SpeedSmsOptions> opts)
    {
        _http = http;
        _opts = opts.Value;
        _http.BaseAddress = new Uri(_opts.BaseUrl.TrimEnd('/') + "/");
        _http.DefaultRequestHeaders.Add("apikey", _opts.ApiToken);
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> SendAsync(SendSmsRequest data, CancellationToken ct = default)
    {
        var payload = new
        {
            data.To,       
            data.Content,
            data.Sender
        };

        using var req = new HttpRequestMessage(HttpMethod.Post, "sms")
        {
            Content = new StringContent(JsonSerializer.Serialize(payload, _jsonOpts), Encoding.UTF8, "application/json")
        };

        using var res = await _http.SendAsync(req, ct);
        var json = await res.Content.ReadAsStringAsync(ct);

        // var dto = JsonSerializer.Deserialize<SpeedSmsSendResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        //           ?? throw new InvalidOperationException("Empty response from SpeedSMS");

        if (!res.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"SpeedSMS error {res.StatusCode}: {json}");
        }

        return json;
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