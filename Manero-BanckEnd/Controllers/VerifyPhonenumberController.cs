using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Manero_BanckEnd.Controllers
{
    [Route("api/PhoneVerification")]
    [ApiController]
    [UseApiKey]
    [Authorize]
    public class PhoneVerificationController : ControllerBase
    {
        private readonly PhoneVerification _twilioSettings;

        public PhoneVerificationController(IOptions<PhoneVerification> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
        }

        [HttpPost("SendVerificationCode")]
        public IActionResult SendVerificationCode(string phoneNumber)
        {
            try
            {
                TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

                var verificationCode = GenerateRandomCode();

                var message = MessageResource.Create(
                    body: $"Din verifieringskod är: {verificationCode}",
                    from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );

                SaveVerificationCodeInDatabase(phoneNumber, verificationCode);
                SendVerificationConfirmation(phoneNumber);

                return Ok("Verifieringskod skickad!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Något gick fel: " + ex.Message);
            }
        }

        private void SaveVerificationCodeInDatabase(string phoneNumber, string verificationCode)
        {
            //Lägger inte till i Databasen , men vi ponerar att det gör det! Under uppbyggnad!
        }

        private void SendVerificationConfirmation(string phoneNumber)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            var message = MessageResource.Create(
                body: "Din verifiering har lyckats! Tack för att du verifierade ditt nummer.",
                from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            
        }

        private string GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
