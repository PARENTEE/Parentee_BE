using Parentee_BE.BLL.Services.Interfaces.OtpInterface;
using Parentee_BE.DAL.Data.OtpDtos.Options;
using Twilio;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account;

namespace Parentee_BE.BLL.Services.Implements.OtpImplement;

public class TwilioSmsSender : ISmsSender
{
    private readonly TwilioOptions _opt;
    public TwilioSmsSender(Microsoft.Extensions.Options.IOptions<TwilioOptions> opt)
    {
        _opt = opt.Value;
        TwilioClient.Init(_opt.AccountSid, _opt.AuthToken);
    }

    public async Task SendAsync(string to, string body)
    {
        await MessageResource.CreateAsync(
            to: new Twilio.Types.PhoneNumber(to),
            from: new Twilio.Types.PhoneNumber(_opt.FromNumber),
            body: body
        );
    }
}