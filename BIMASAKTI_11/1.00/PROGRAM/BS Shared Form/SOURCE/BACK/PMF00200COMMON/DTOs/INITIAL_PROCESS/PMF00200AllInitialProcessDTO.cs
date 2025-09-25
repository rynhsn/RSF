using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00200COMMON
{
    public class PMF00200AllInitialProcessDTO
    {
        public PMF00200GSCompanyInfoDTO VAR_GSM_COMPANY  { get; set; }
        public List<PMF00200ReportTemplateDTO> VAR_REPORT_TEMPLATE_LIST { get; set; }
    }
}
