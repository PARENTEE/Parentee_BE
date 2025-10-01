namespace Parentee_BE.DAL.Data.SmsDTO;

public class SpeedSmsSendData
{
    public long TranId { get; set; }       // dùng tra cứu trạng thái :contentReference[oaicite:11]{index=11}
    public int TotalSMS { get; set; }
    public decimal TotalPrice { get; set; }
    public List<string>? InvalidPhone { get; set; }
}