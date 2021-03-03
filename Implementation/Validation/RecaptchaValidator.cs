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
            if (string.IsNullOrWhiteSpace(submittedValue))
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

            return result.success && result.score >= recaptchaElment.ScoreThreshold;
        }
    }

    /// <summary>
    /// Object to hold Google reCAPTCHA verify result.
    /// </summary>
    public class RecaptchaResponse
    {
        /// <summary>
        /// Whether this request was a valid reCAPTCHA token for your site.
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// Timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ).
        /// </summary>
        public DateTime challenge_ts { get; set; }

        /// <summary>
        /// The hostname of the site where the reCAPTCHA was solved.
        /// </summary>
        public string hostname { get; set; }

        /// <summary>
        /// The score for this request (0.0 - 1.0).
        /// </summary>
        public double score { get; set; }
    }
}
