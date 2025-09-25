using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04100COMMON
{
    public class PMT04100JournalListInitialProcessDTO
    {
        public PMT04100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public List<PMT04100PropertyDTO> VAR_GS_PROPERTY_LIST { get; set; }
        public List<PMT04100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
    }
}
