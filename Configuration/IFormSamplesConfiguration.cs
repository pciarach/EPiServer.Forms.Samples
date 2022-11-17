using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Forms.Samples.Configuration
{
    public interface IFormSamplesConfiguration
    {
        string GoogleMapsApiV3Url { get; }
        string GoogleMapsGeocodingUrl { get; }
        string ClientApiKey { get; }
        string ServerApiKey { get; }
        string SiteKey { get; }
        string SecretKey { get; }
    }
}
