using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100PDF
{
    public class PMT02100InvitationParameterReportDTO
    {
        public PMT02100InvitationParameterDTO loParameter { get; set; }
        public List<PMT02100InvitationDTO> DataReport { get; set; }
    }
}
