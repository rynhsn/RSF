using PMT01500Common.DTO._2._Agreement;
using PMT01500Common.Utilities;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMT01500Common.Interface
{
    public interface IPMT01500Agreement : R_IServiceCRUDBase<PMT01500AgreementDetailDTO>
    {
        IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCLeaseMode();
        IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCChargesMode();
    }
    
}
