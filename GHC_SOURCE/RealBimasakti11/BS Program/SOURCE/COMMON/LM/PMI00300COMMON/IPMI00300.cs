using PMI00300COMMON.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON
{
    public interface IPMI00300
    {
        IAsyncEnumerable<PMI00300GetPropertyListDTO> GetPropertyList();
        IAsyncEnumerable<PMI00300DTO> GetUnitInquiryHeaderList();
        IAsyncEnumerable<PMI00300DetailLeftDTO> GetUnitInquiryDetailLeftList();
        IAsyncEnumerable<PMI00300DetailRightDTO> GetUnitInquiryDetailRightList();
        IAsyncEnumerable<PMI00300GetGSBCodeListDTO> GetLeaseStatusList();
        IAsyncEnumerable<PMI00300GetGSBCodeListDTO> GetStrataStatusList();

    }
}
