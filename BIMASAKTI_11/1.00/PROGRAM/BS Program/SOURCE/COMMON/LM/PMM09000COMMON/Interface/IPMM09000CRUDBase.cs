using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.UtiliyDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.Interface
{
    public interface IPMM09000CRUDBase : R_IServiceCRUDBase<PMM09000EntryHeaderDetailDTO>
    {
        UpdateStatusDTO UpdateAmortizationStatus(PMM09000DbParameterDTO poParameter);
    }
}
