using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Core.Validation;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using System;

namespace EPiServer.Forms.Samples.Implementation.Validation
{
    /// <summary>
    ///     validate date time string in format "2015-12-25 10:30 AM"
    /// </summary>
    public class DateTimeValidatorBase : ElementValidatorBase
    {
        protected Injected<ServiceAccessor<LocalizationService>> _localization;
        protected LocalizationService LocalizationService { get { return _localization.Service(); } }

        /// <inheritdoc />
        public override bool? Validate(IElementValidatable targetElement)
        {
            var submittedValue = targetElement.GetSubmittedValue() as string;
            if (string.IsNullOrEmpty(submittedValue))
            {
                return true;
            }

            DateTime dateTime;
            if (DateTime.TryParse(submittedValue, out dateTime))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc />
        /// we don't want to show this specific-validators to Editor. This validators always work programatically for DateTimeElement.
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
                model.Message = this.LocalizationService.GetString("/episerver/forms/validators/elementselfvalidator/unexpectedvalueisnotaccepted"); ;
            }

            return model;
        }
    }

    public class DateTimeValidator : DateTimeValidatorBase {}

    public class DateValidator : DateTimeValidatorBase {}

    public class TimeValidator : DateTimeValidatorBase {}
}