using ICT00900COMMON.DTO;
using ICT00900COMMON.Param;
using ICT00900COMMON.Utility_DTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICT00900COMMON.Interface
{
    public interface IICT00900 : R_IServiceCRUDBase<ICT00900AjustmentDetailDTO>
    {
        VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE();
        VarGsmCompanyInfoDTO GetVAR_GSM_COMPANY_INFO();
        IAsyncEnumerable<PropertyDTO> PropertyList();
        IAsyncEnumerable<CurrencyDTO> CurrencyList();
        IAsyncEnumerable<ICT00900AdjustmentDTO> GetAdjustmentList();
        ICT00900AdjustmentDTO ChangeStatusAdjustment(ICT00900ParameterChangeStatusDTO poEntity);

    }
}
