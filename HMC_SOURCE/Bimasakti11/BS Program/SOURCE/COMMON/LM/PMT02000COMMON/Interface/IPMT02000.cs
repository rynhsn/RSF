using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Upload;
using PMT02000COMMON.Utility;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Interface
{
    public interface IPMT02000 : R_IServiceCRUDBase<PMT02000LOIHeader_DetailDTO>
    {
        IAsyncEnumerable<PMT02000PropertyDTO> GetPropertyListStream();
        IAsyncEnumerable<PMT02000LOIDTO> GetLOIListStream();
        IAsyncEnumerable<PMT02000LOIHandoverUtilityDTO> GetLOIDetailListStream();
        PMT02000LOIHeader ProcessSubmitRedraft(PMT02000DBParameter poParam);
        PMT02000VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE();
        PMT0200MultiListDataDTO GetHOTemplate(PMT02000DBParameter poParam);

    }
}
