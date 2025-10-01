namespace Parentee_BE.DAL.Data.SmsDTO;

public class SpeedSmsStatusItem
{
    public string Phone { get; set; } = default!;
    public int Status { get; set; } // 0=pending, -1=sending, 1=success, 2=error :contentReference[oaicite:12]{index=12}
}