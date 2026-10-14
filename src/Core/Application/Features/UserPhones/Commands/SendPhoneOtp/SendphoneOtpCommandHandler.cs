using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace Core.Application.Features.UserPhones.Commands.SendPhoneOtp
{
    public sealed class SendphoneOtpCommandHandler(IMemoryCache cache, ILogger<SendphoneOtpCommandHandler> logger) : IRequestHandler<SendphoneOtpCommand, string>
    {
        public Task<string> Handle(SendphoneOtpCommand request, CancellationToken cts)
        {
            var otpCode = Random.Shared.Next(100000, 999999).ToString();

            var cachKey = $"OTP:{request.PhoneNumber}";
            cache.Set(cachKey, otpCode, TimeSpan.FromMinutes(10));

            logger.LogInformation("==========================================");
            logger.LogInformation(">>> OTP for {Phone} is [{Code}] <<<", request.PhoneNumber, otpCode);
            logger.LogInformation("==========================================");

            return Task.FromResult("Code sent Successfully");
        }
    }
}
