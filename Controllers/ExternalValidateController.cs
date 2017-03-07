using EPiServer.Forms.Samples.Business;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPiServer.Forms.Samples.Controllers
{
    public class ExternalValidateController: Controller
    {
        Injected<IAddressValidateService> _addressService;

        /// <summary>
        /// Validate form address element.
        /// </summary>
        /// <param name="address">The address to validate.</param>
        [HttpPost, ValidateInput(false)]
        public JsonResult ValidateAddress(string address, string street, string city, string state, string postalCode, string country, bool ignoreDetail = false)
        {
            var validateResult = _addressService.Service.Validate(address, street, city, state, postalCode, country, ignoreDetail);

            return Json(validateResult, JsonRequestBehavior.AllowGet);
        }
    }
}
