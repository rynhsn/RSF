using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public interface IPMT01330Popup 
    {
        IAsyncEnumerable<PMT01331DTO> GetLOIChargeRevenueHDListStream();
        PMT01300SingleResult<PMT01331DTO> SaveDeleteLOIChargeRevenueHD(PMT01300SaveDTO<PMT01331DTO> poEntity);
        IAsyncEnumerable<PMT01332DTO> GetLOIChargeRevenueListStream();
        PMT01300SingleResult<PMT01332DTO> SaveDeleteLOIChargeRevenue(PMT01300SaveDTO<PMT01332DTO> poEntity);
        IAsyncEnumerable<PMT01333DTO> GetRevenueMintRentListStream();
        PMT01300SingleResult<PMT01333DTO> SaveRevenueMintRent(PMT01300SaveDTO<PMT01333DTO> poEntity);
    }
}
