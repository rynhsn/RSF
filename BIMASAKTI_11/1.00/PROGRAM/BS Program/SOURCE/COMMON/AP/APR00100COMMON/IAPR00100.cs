using System;
using System.Collections.Generic;
using System.Text;
using APR00100COMMON.DTO_s;

namespace APR00100COMMON
{
    public interface IAPR00100 
    {
        APR00100ResultDTO<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        APR00100ResultDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
        IAsyncEnumerable<CustomerTypeDTO> GetCustomerTypeList();
        IAsyncEnumerable<TransTypeSuppCattDTO> GetTransTypeList();
        IAsyncEnumerable<TransTypeSuppCattDTO> GetSuppCattegoryList();

    }
}
