using PMI00300COMMON.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON
{
    public interface IPMI00300AgreementForm
    {
        IAsyncEnumerable<PMI00300GetAgreementFormListDTO> GetAgreementFormList();
        PMI00300GetPeriodYearRangeResultDTO GetPeriodYearRange();
        IAsyncEnumerable<PMI00300HandOverChecklistDTO> GetList_HandOverChecklist(PMI00300HandOverChecklistParamDTO poParam);

    }
}
