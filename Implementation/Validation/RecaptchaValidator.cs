using EPiServer.Forms.Core.Validation;
using EPiServer.Forms.Helpers.Internal;
using EPiServer.Forms.Implementation.Validation;
using EPiServer.Forms.Samples.Implementation.Elements;
using System;
using System.Net;
namespace EPiServer.Forms.Samples.Implementation.Validation
{
    /// <summary>
    /// Validator for validate Recaptcha element.
    /// </summary>
    public class RecaptchaValidator : InternalElementValidatorBase
    {
        private const string RecaptchaVerifyBaseUrl = "https://www.google.com/recaptcha/api/siteverify";

        /// <inheritdoc />
        public override bool? Validate(IElementValidatable targetElement)
        {
            // NOTE: when run in none-js mode, the recaptcha element value will be null and validation failed.
            var submittedValue = targetElement.GetSubmittedValue() as string;
            if (string.IsNullOrEmpty(submittedValue))
            {
                return false;
            }

            var recaptchaElment = targetElement as RecaptchaElementBlock;
            if (recaptchaElment == null)
            {
                return false;
            }

            var client = new WebClient();
            var verifyUrl = RecaptchaVerifyBaseUrl
                            .AddQueryString("secret", recaptchaElment.SecretKey)
                            .AddQueryString("response", submittedValue);
            var responseString = client.DownloadString(verifyUrl);
            var result = responseString.ToObject<RecaptchaResponse>();

            return result.success;
        }
    }

    /// <summary>
    /// Object to hold Google reCAPTCHA verify result.
    /// </summary>
    public class RecaptchaResponse
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
