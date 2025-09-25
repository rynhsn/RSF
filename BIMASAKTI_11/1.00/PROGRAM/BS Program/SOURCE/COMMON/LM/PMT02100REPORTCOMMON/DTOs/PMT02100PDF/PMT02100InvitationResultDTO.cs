using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100PDF
{
    public class PMT02100InvitationResultDTO
    {
        public PMT02100InvitationColumnDTO Column { get; set; }
        public PMT02100InvitationDTO Data { get; set; }
    }

    public class PMT02100InvitationResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMT02100InvitationResultDTO ReportData { get; set; }
    }
}
