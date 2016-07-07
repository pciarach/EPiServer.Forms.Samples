using EPiServer.Core;
using EPiServer.Forms.Core;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPiServer.Forms.Core.Models.Internal;

namespace EPiServer.Forms.Samples.Criteria
{
    /// <summary>
    /// Return list of published forms.
    /// </summary>
    public class FormSelectionFactory : ISelectionFactory
    {
        private Injected<IFormRepository> _formRepository;
        private Injected<IContentRepository> _contentRepository;

        #region ISelectionFactory Members

        public IEnumerable<SelectListItem> GetSelectListItems(Type property)
        {
            var formsInfo = _formRepository.Service.GetFormsInfo(null);
            var publishedFormsInfo = new List<FormInfo>();
            foreach (var info in formsInfo)
            {
                IContent content;
                if (!_contentRepository.Service.TryGet<IContent>(info.FormGuid, out content))
                {
                    continue;
                }
                
                var versionable = content as IVersionable;
                if (versionable == null || versionable.Status != VersionStatus.Published)
                {
                    continue;
                }

                publishedFormsInfo.Add(info);
            }

            return publishedFormsInfo.Select(f =>
            {
                return new SelectListItem()
                {
                    Text = f.Name,
                    Value = f.FormGuid.ToString()
                };
            }).OrderBy(f => f.Text);
        }

        #endregion
    }
}
