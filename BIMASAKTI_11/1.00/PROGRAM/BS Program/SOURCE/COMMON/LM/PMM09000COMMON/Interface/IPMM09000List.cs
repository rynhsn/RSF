using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.Interface
{
    public interface IPMM09000List
    {
        IAsyncEnumerable<PMM09000BuildingDTO> GetBuldingList();
        IAsyncEnumerable<PMM09000AmortizationDTO> GetAmortizationList();
        IAsyncEnumerable<PMM09000AmortizationSheduleDetailDTO> GetAmortizationScheduleList();
        IAsyncEnumerable<PMM09000AmortizationChargesDTO> GetAmortizationChargesList();
        IAsyncEnumerable<ChargesDTO> GetChargesTypeList();
    }
}
