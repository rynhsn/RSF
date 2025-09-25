using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.Interface
{
    public interface IPMM09000RecordController
    {
        PMM09000EntryHeaderDTO GetAmortizationDetail(PMM09000DbParameterDTO poParameter);
    }
}
