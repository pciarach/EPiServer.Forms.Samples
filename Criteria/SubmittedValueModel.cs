using EPiServer.DataAnnotations;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.Web.Mvc.VisitorGroups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Forms.Samples.Criteria
{
    /// <summary>
    /// Criteria model for SubmittedValue.
    /// </summary>
    public class SubmittedValueModel : CriterionModelBase
    {
        [Ignore]
        public override EPiServer.Data.Identity Id { get; set; }

        [Required]
        [DojoWidget(SelectionFactoryType = typeof(FormSelectionFactory), LabelTranslationKey = "/episerver/forms/samples/criteria/submittedvaluecriterion/selectedform", AdditionalOptions = "{ selectOnClick: true }")]
        public string SelectedForm { get; set; }

        [Required]
        [DojoWidget(WidgetType = "dijit.form.FilteringSelect", LabelTranslationKey = "/episerver/forms/samples/criteria/submittedvaluecriterion/selectedfield", AdditionalOptions = "{ selectOnClick: true }")]
        public string SelectedField { get; set; }

        [DojoWidget(
            LabelTranslationKey = "/episerver/forms/samples/criteria/submittedvaluecriterion/condition",
            SelectionFactoryType = typeof(EnumSelectionFactory),
            AdditionalOptions = "{ selectOnClick: true }"),
            Required]
        public FieldValueCompareCondition Condition { get; set; }

        /// <summary>
        /// Gets or sets the value to be used for comparisons
        /// </summary>
        [DojoWidget(LabelTranslationKey = "/episerver/forms/samples/criteria/submittedvaluecriterion/value")]
        public string Value { get; set; }

        public override ICriterionModel Copy()
        {
            // We can use the ShallowCopy method as this class
            // does not have any members that need to be deep copied
            return base.ShallowCopy();
        }
    }
}
