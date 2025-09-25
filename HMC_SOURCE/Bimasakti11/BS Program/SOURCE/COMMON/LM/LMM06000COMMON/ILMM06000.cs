using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM06000COMMON
{
    public interface ILMM06000 : R_IServiceCRUDBase<LMM06000BillingRuleDetailDTO>
    {
        IAsyncEnumerable<LMM06000BillingRuleDTO> BillingRuleListStream();
        LMM06000PropertyListDTO GetAllPropertyList();
        IAsyncEnumerable <LMM06000UnitTypeDTO> GetAllUnitTypeList();
        LMM06000PeriodListDTO GetAllPeriodList();
        LMM06000ActiveInactiveDTO SetActiveInactive(LMM06000ActiveInactiveDTO loParameter);
    }
}
