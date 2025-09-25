using PMF00100COMMON.DTOs.PMF00100;
using PMF00100COMMON.DTOs.PMF00110;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON
{
    public interface IPMF00110 : R_IServiceCRUDBase<PMF00110ParameterDTO>
    {
        SubmitAllocationResultDTO SubmitAllocationProcess(SubmitAllocationParameterDTO poParam);
        RedraftAllocationResultDTO RedraftAllocationProcess(RedraftAllocationParameterDTO poParam);
        PMF00110ResultDTO GetAllocationDetail(GetAllocationDetailParameterDTO poParam);
        IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeList();
    }
}
