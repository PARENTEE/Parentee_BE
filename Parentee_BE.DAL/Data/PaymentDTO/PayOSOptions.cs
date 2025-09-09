namespace Parentee_BE.DAL.Data.PaymentDTO;

public record PayOSOptions()
{
    public string ClientId { get; init; } = default!;
    public string ApiKey { get; init; } = default!;
    public string ChecksumKey { get; init; } = default!;
    public string ReturnUrl { get; init; } = default!;
    public string CancelUrl { get; init; } = default!;
}