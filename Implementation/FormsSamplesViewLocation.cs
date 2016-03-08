using EPiServer.Forms.Core;
using EPiServer.Forms.Helpers;
using EPiServer.Forms.Implementation;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Forms.Samples.Implementation
{
    /// <summary>
    /// This tell EpiForm that template of Samples (cshtml files) should be scanned from Samples addon folder.
    /// </summary>
    [ServiceConfiguration(typeof(ICustomViewLocation))]
    public class FormsSamplesViewLocation : CustomViewLocationBase
    {
        /// <summary>
        /// This will be loaded before path of Form.Core
        /// </summary>
        public override int Order { get { return 500; } set { } }
        public override string[] Paths
        {
            get
            {
                return new string[] { GetDefaultViewLocation() };
            }
        }
    }
}
