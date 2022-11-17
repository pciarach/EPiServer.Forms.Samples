using EPiServer.ServiceLocation;

namespace EPiServer.Forms.Samples.Configuration
{
    [ServiceConfiguration(typeof(IFormSamplesConfiguration))]
    public class FormSamplesConfiguration : IFormSamplesConfiguration
    {
        private readonly FormSamplesGoogleUrlOptions _formSamplesGoogleUrlOptions;
        private readonly FormSamplesApiKeyOptions _formSamplesApiKeyOptions;

        public FormSamplesConfiguration(FormSamplesGoogleUrlOptions formSamplesGoogleUrlOptions,
            FormSamplesApiKeyOptions formSamplesApiKeyOptions)
        {
            _formSamplesGoogleUrlOptions = formSamplesGoogleUrlOptions;
            _formSamplesApiKeyOptions = formSamplesApiKeyOptions;
        }

        public string GoogleMapsApiV3Url
        {
            get
            {
                return _formSamplesGoogleUrlOptions.GoogleMapsApiV3Url;
                
            }
        }

        public string GoogleMapsGeocodingUrl
        {
            get
            {
                return _formSamplesGoogleUrlOptions.GoogleMapsGeocodingUrl;
            }
        }

        public string ClientApiKey
        {
            get
            {
                if(_formSamplesApiKeyOptions.AddressKey is not null)
                {
                    return _formSamplesApiKeyOptions.AddressKey.ClientApiKey;
                } else
                {
                    return null;
                }
            }
        }

        public string ServerApiKey
        {
            get
            {
                if (_formSamplesApiKeyOptions.AddressKey is not null)
                {
                    return _formSamplesApiKeyOptions.AddressKey.ServerApiKey;
                }
                else
                {
                    return null;
                }
            }
        }

        public string SiteKey
        {
            get
            {
                if (_formSamplesApiKeyOptions.RecapchaKey is not null)
                {
                    return _formSamplesApiKeyOptions.RecapchaKey.SiteKey;
                }
                else
                {
                    return null;
                }
            }
        }

        public string SecretKey
        {
            get
            {
                if (_formSamplesApiKeyOptions.RecapchaKey is not null)
                {
                    return _formSamplesApiKeyOptions.RecapchaKey.SecretKey;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

// [0,0]
// [0,1]
// [1,0]
// [1,2]
// [2,1]
// [2,2]

// [0,0]
// [0,2]
// [1,1]
// [2,0]
// [2,2]