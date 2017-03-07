using EPiServer.Configuration;
using EPiServer.Forms.Core.Validation;
using EPiServer.Forms.Helpers.Internal;
using EPiServer.Forms.Implementation.Validation;
using EPiServer.Forms.Samples.Business;
using EPiServer.Forms.Samples.Implementation.Elements;
using System.Net;
using System;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Web;
using EPiServer.Forms.Core.Models;
using EPiServer.Framework.Localization;

namespace EPiServer.Forms.Samples.Implementation.Validation
{
    public class AddressValidator: InternalElementValidatorBase
    {        
        private Injected<LocalizationService> _localizationService;
        protected LocalizationService LocalizationService { get { return _localizationService.Service; } }

        public override bool? Validate(IElementValidatable targetElement)
        {
            // if in js mode, the validation is done by an extenal service
            // if in non-js mode, just accept user input
            // so here, always return true
            return true;
        }

        /// <inheritdoc />
        /// we don't want to show this specific-validators to Editor. This validators always work programatically for AddressElement.
        public override bool AvailableInEditView
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public override IValidationModel BuildValidationModel(IElementValidatable targetElement)
        {
            var model = base.BuildValidationModel(targetElement);
            if (model != null)
            {
                model.Message = this.LocalizationService.GetString("/episerver/forms/validators/elementselfvalidator/unexpectedvalueisnotaccepted");
            }

            return model;
        }
    }

    [ServiceConfiguration(ServiceType = typeof(IAddressValidateService))]
    public class GoogleAddressValidateService : IAddressValidateService
    {
        private readonly string GoogleApiKey = GetApiKey();
        private const string GoogleMapsGeocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?";
        
        private readonly string currentPageLanguage = FormsExtensions.GetCurrentPageLanguage() ?? "en";

        public bool Validate(string address, string street, string city, string state, string postalCode, string country, bool ignoreDetails = false)
        {            
           
            if (string.IsNullOrWhiteSpace(GoogleApiKey))
            {
                return false;
            }
          
            var verifyUrl = GoogleMapsGeocodingAPI;

            if (!ignoreDetails && !string.IsNullOrWhiteSpace(address))
            {
                verifyUrl = verifyUrl.AddQueryString("address", address);
            }

            // build components filter
            List<string> componentFilter = new List<string>();
            if (!string.IsNullOrWhiteSpace(street))
            {
                componentFilter.Add("route:" + street);
            }
            if (!string.IsNullOrWhiteSpace(city))
            {
                componentFilter.Add("locality:" + city);
            }
            if (!string.IsNullOrWhiteSpace(state))
            {
                componentFilter.Add("administrative_area:" + state);
            }
            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                componentFilter.Add("postal_code:" + postalCode);
            }
            if (!string.IsNullOrWhiteSpace(country))
            {
                componentFilter.Add("country:" + country);
            }
            if (componentFilter.Count > 0)
            {
                verifyUrl = verifyUrl.AddQueryString("components", string.Join("|", componentFilter));
            }
            verifyUrl = verifyUrl.AddQueryString("key", GoogleApiKey);
            verifyUrl = verifyUrl.AddQueryString("language", currentPageLanguage);

            // delegate the validation for google place
            try
            {
                var client = new WebClient();
                var responseString = client.DownloadString(verifyUrl);
                var result = responseString.ToObject<GooglePlaceValidateResponse>();
                return result.status == GeocodingResponse.OK;
            }
            catch
            {
                return false;
            }
        }

        private static string GetApiKey()
        {
            if (string.IsNullOrEmpty(Settings.Instance.GoogleMapsApiV3Url))
            {
                return null;
            }
            Uri googleMapUri = new Uri(Settings.Instance.GoogleMapsApiV3Url);
            string apiKey = HttpUtility.ParseQueryString(googleMapUri.Query).Get("key");
            return apiKey;
        }
    }

    public class GooglePlaceValidateResponse
    {
        public string status { get; set; }
    }

    public class GeocodingResponse
    {
        public const string OK = "OK";
        public const string ZERO_RESULTS = "ZERO_RESULTS";
        public const string OVER_QUERY_LIMIT = "OVER_QUERY_LIMIT";
        public const string REQUEST_DENIED = "REQUEST_DENIED";
        public const string INVALID_REQUEST = "INVALID_REQUEST";
        public const string UNKNOWN_ERROR = "UNKNOWN_ERROR";
    }
}
