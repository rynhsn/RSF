using PMT03000COMMON.DTO_s;
using PMT03000COMMON.DTO_s.Helper;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT03000COMMON
{
    public interface IPMT03001 : R_IServiceCRUDBase<TenantUnitFacilityDTO>
    {
        IAsyncEnumerable<UnitTypeCtgFacilityDTO> GetList_UnitTypeCtgFacility();
        IAsyncEnumerable<TenantUnitFacilityDTO> GetList_TenantUnitFacility();
        GeneralAPIResultDTO<TenantUnitFacilityDTO> ActiveInactive_TenantUnitFacility(TenantUnitFacilityDTO poParam);
        public GeneralAPIResultDTO<FacilityQtyDTO> GetRecord_FacilityQty(TenantUnitFacilityDTO poParam);
    }
}
