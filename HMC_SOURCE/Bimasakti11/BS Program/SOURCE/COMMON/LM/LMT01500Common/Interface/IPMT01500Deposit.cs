using System.Collections.Generic;
using PMT01500Common.DTO._6._Deposit;
using PMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace PMT01500Common.Interface
{
    public interface IPMT01500Deposit : R_IServiceCRUDBase<PMT01500DepositDetailDTO>
    {
        PMT01500DepositHeaderDTO GetDepositHeader(PMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT01500DepositListDTO> GetDepositList();
        
    }
}