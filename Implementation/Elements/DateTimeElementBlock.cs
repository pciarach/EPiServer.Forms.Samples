using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Models.Internal;
using EPiServer.Forms.EditView;
using EPiServer.Forms.EditView.Models.Internal;
using EPiServer.Forms.Implementation.Elements.BaseClasses;
using EPiServer.Forms.Implementation.Validation;
using EPiServer.Forms.Samples.EditView;
using EPiServer.Forms.Samples.EditView.SelectionFactory;
using EPiServer.Forms.Samples.Implementation.Models;
using EPiServer.Forms.Samples.Implementation.Validation;
using EPiServer.Shell.ObjectEditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EPiServer.Forms.Samples.Implementation.Elements
{
    /// <summary>
    /// DateTime element for EpiForm (support Visitor picking Time, Date, or both Date and Time)
    /// </summary>
    [ContentType(GUID = "{3CC0755E-E50B-4AF4-92DF-C0F7625D526F}", GroupName = ConstantsFormsUI.FormElementGroup, Order = 2230)]
    public class DateTimeElementBlock : InputElementBlockBase, IElementCustomFormatValue
    {
        /// <summary>
        /// Email validator does not make sense for DateTime.
        /// So we don't want these Validators available for this Element.
        /// </summary>
        [Ignore]    // CMS Shell, please don't show this property to Editor
        public override Type[] ValidatorTypesToBeExcluded
        {
            get
            {
                return new Type[] 
                {
                    typeof(EmailValidator),
                    typeof(DateDDMMYYYYValidator),
                    typeof(DateMMDDYYYYValidator),
                    typeof(DateYYYYMMDDValidator),
                    typeof(RegularExpressionValidator),
                    typeof(IntegerValidator),
                    typeof(PositiveIntegerValidator)
                };
            }
            set { }
        }

        [SelectOne(SelectionFactoryType = typeof(DateTimePickerTypeSelectionFactory))]
        [Display(GroupName = SystemTabNames.Content, Order = -6000)]
        public virtual int PickerType { get; set; }

        /// <summary>
        /// Always use a custom Validator to validate this datetime element (along with builtin Validator like RequiredValidator)
        /// <remarks>The datetime custom Validator is not visible to Editor (in EditView), but it still works to validate element value</remarks>
        /// </summary>
        [Display(GroupName = SystemTabNames.Content, Order = -5000)]
        public override string Validators
        {
            get
            {
                var pickerValidator = GetValidatorTypeForPicker((DateTimePickerType)PickerType).FullName;
                var validators = this.GetPropertyValue(content => content.Validators);
                if (string.IsNullOrEmpty(validators))
                {
                    return pickerValidator;
                }
                else
                {
                    return string.Concat(validators, EPiServer.Forms.Constants.RecordSeparator, pickerValidator);
                }
            }
            set
            {
                this.SetPropertyValue(content => content.Validators, value);
            }
        }


        /// <summary>
        /// Base on DateTimePicker type, we use appropriate Validator.
        /// </summary>
        private Type GetValidatorTypeForPicker(DateTimePickerType pickerType)
        {
            switch (pickerType)
            {
                case DateTimePickerType.DatePicker:
                    return typeof(DateValidator);
                case DateTimePickerType.TimePicker:
                    return typeof(TimeValidator);
                case DateTimePickerType.DateTimePicker:
                    return typeof(DateTimeValidator);

                default:
                    return null;
            }
        }

        /// <inheritdoc />
        public virtual object GetFormattedValue()
        {
            // NOTE: submittedValue always is yyyy-MM-dd hh:mm:ss, might need to transform to date only (yyyy/MM/dd) or time only (hh:mm)
            
            var submittedValue = this.GetSubmittedValue();
            if (submittedValue == null)
            {
                return null;
            }

            var valueString = submittedValue.ToString();
            DateTime dateTimeValue;
            if (!DateTime.TryParse(valueString, out dateTimeValue))
            {
                return valueString;
            }
            var dateTimeSegments = dateTimeValue.ToString("s", CultureInfo.InvariantCulture).Split(new char[] { 'T' }, StringSplitOptions.RemoveEmptyEntries);
            var pickerType = (DateTimePickerType)this.PickerType;
            switch (pickerType)
            {
                case DateTimePickerType.TimePicker:
                    return dateTimeSegments[1];
                case DateTimePickerType.DatePicker:
                    return dateTimeSegments[0];
                case DateTimePickerType.DateTimePicker:
                    return string.Join(" ", dateTimeSegments);
                default:
                    return valueString;
            }
        }

        /// <inheritdoc />
        public override ElementInfo GetElementInfo()
        {
            var baseInfo = base.GetElementInfo();

            var dateTimeElementInfo = new DateTimeElementInfo
            {
                Type = baseInfo.Type,
                FriendlyName = baseInfo.FriendlyName,
                CustomBinding = true,
                PickerType = ((DateTimePickerType)this.PickerType).ToString().ToLower()
            };

            return dateTimeElementInfo;
        }
    }
}
