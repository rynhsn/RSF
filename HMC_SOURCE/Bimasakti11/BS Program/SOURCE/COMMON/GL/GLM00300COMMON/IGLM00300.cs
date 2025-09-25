using System;
using System.Collections.Generic;
using System.Text;
using GLM00300Common.DTOs;
using R_CommonFrontBackAPI;

namespace GLM00300Common
{
    public interface IGLM00300 : R_IServiceCRUDBase<GLM00300DTO>
    {
        IAsyncEnumerable<GLM00300DTO> GetBudgetWeightingNameList();
        CurrencyCodeListDTO GetCurrencyCodeList();
    }
}
