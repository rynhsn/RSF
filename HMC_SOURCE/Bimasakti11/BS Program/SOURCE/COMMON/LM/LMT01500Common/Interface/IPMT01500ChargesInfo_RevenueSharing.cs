using System.Collections.Generic;
using PMT01500Common.DTO._4._Charges_Info;
using R_CommonFrontBackAPI;

namespace PMT01500Common.Interface
{
    public interface IPMT01500ChargesInfo_RevenueSharing : R_IServiceCRUDBase<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>
    {
        IAsyncEnumerable<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO> GetRevenueSharingSchemeList();
        IAsyncEnumerable<PMT01500ChargesInfo_RevenueMinimumRentDTO> GetRevenueMinimumRentList();
    }
}