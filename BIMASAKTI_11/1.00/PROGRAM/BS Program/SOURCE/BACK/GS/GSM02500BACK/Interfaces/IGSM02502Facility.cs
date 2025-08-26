using GSM02500COMMON.DTOs.GSM02502Facility;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02502Facility : R_IServiceCRUDAsyncBase<GSM02502FacilityParameterDTO>
    {
        IAsyncEnumerable<GSM02502FacilityDTO> GetFacilityList();
        IAsyncEnumerable<GSM02502FacilityTypeDTO> GetFacilityTypeList();
    }
}
