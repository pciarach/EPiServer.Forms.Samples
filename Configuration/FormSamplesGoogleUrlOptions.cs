using EPiServer.ServiceLocation;

namespace EPiServer.Forms.Samples.Configuration
{
    [Options(ConfigurationSection = ConfigurationSectionConstants.Cms)]
    public class FormSamplesGoogleUrlOptions
    {
        public string GoogleMapsApiV3Url { get; set; } = "https://maps.google.com/maps/api/js?libraries=places";
        public string GoogleMapsGeocodingUrl { get; set; } = "https://maps.googleapis.com/maps/api/geocode/json?";
    }
}
