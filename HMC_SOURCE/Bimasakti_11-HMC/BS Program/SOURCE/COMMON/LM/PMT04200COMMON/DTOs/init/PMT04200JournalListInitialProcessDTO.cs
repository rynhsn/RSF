using System.Collections.Generic;

namespace PMT04200Common.DTOs;

public class PMT04200JournalListInitialProcessDTO
{
    public PMT04200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; }
    public PMT04200PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; }
    public List<PropertyListDTO> VAR_GS_PROPERTY_LIST { get; set; }
    public List<PMT04200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; }
}