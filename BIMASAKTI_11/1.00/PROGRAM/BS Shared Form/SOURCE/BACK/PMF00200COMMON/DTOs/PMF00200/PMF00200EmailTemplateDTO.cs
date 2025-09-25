using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMF00200COMMON
{
    public class PMF00200EmailTemplateDTO
    {
        public string CTEMPLATE_DESC { get; set; }
        public string CTEMPLATE_BODY { get; set; }
        public string CGENERAL_EMAIL_ADDRESS { get; set; }
        public string CSMTP_PORT { get; set; }
        public string CSMTP_SERVER { get; set; }
    }
}
