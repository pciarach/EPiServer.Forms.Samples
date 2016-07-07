using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Helpers.Internal;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.VisitorGroups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Forms.Samples.Criteria
{
    public class FormModel : CriterionModelBase
    {
        [Ignore]
        public override EPiServer.Data.Identity Id { get; set; }

        [Required]
        [DojoWidget(SelectionFactoryType = typeof(EnumSelectionFactory), LabelTranslationKey = "/episerver/forms/samples/criteria/formcriterion/submissionstatus", AdditionalOptions = "{ selectOnClick: true }")]
        public SubmissionStatus SubmissionStatus { get; set; }

        [Required]
        [DojoWidget(SelectionFactoryType = typeof(FormSelectionFactory), LabelTranslationKey = "/episerver/forms/samples/criteria/formcriterion/selectedform", AdditionalOptions = "{ selectOnClick: true }")]
        public string SelectedForm { get; set; }

        public override ICriterionModel Copy()
        {
            // We can use the ShallowCopy method as this class
            // does not have any members that need to be deep copied
            return base.ShallowCopy();
        }
    }

    public enum SubmissionStatus
    {
        HasSubmitted,
        HasNotSubmitted
    }

    [VisitorGroupCriterion(
        Category = "EPiServer Forms",
        DisplayName = "Submitted Form",
        Description = "Criterion that checks if the visitor has submitted a specific Form in this session",
        LanguagePath = "/episerver/forms/samples/criteria/formcriterion")]
    public class FormCriterion : CriterionBase<FormModel>
    {
        /// <summary>
        /// Determines whether current user already submitted the form or not.
        /// </summary>
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            var formGuid = Guid.Parse(Model.SelectedForm);
            var hasAlreadyPosted = FormsExtensions.HasAlreadyPosted(formGuid, httpContext);
            if (Model.SubmissionStatus == SubmissionStatus.HasSubmitted)
            {
                return hasAlreadyPosted;
            }
            else
            {
                return !hasAlreadyPosted;
            }
        }
    }
}
