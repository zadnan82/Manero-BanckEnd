using Manero_BanckEnd.Controllers;
using Manero_BanckEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Manero.Test.Verificationtests
{
    public class PhoneVerificationControllerTests
    {
        [Fact] //FredrikSpanien Test
        public void SendVerificationCode_Returns_Status()
        {
            // Arrange
            var mockTwilioSettings = new Mock<IOptions<PhoneVerification>>();
            var twilioSettings = new PhoneVerification
            {
                AccountSid = "AC7be2beda0f29e329017cb17a89c95354",
                AuthToken = "048979e5b8d8e179f5a82fdcb73993bf",
                PhoneNumber = "+46730330855"
            };

            mockTwilioSettings.Setup(opt => opt.Value).Returns(twilioSettings);

            var controller = new PhoneVerificationController(mockTwilioSettings.Object);
            var phoneNumber = "+46730330855";

            // Act
            var result = controller.SendVerificationCode(phoneNumber);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }
    }
}
