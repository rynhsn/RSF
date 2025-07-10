using PMR02000COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON
{
    public interface IPMR02000 
    {
        GeneralResultDTO<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        GeneralResultDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
        IAsyncEnumerable<CategoryTypeDTO> GetCategoryTypeList();
        IAsyncEnumerable<TransactionTypeDTO> GetTransTypeList();

    }
}
