using APF00100COMMON.DTOs.APF00100;
using APF00100COMMON.DTOs.APF00110;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON
{
    public interface IAPF00110 : R_IServiceCRUDBase<APF00110ParameterDTO>
    {
        SubmitAllocationResultDTO SubmitAllocationProcess(SubmitAllocationParameterDTO poParam);
        RedraftAllocationResultDTO RedraftAllocationProcess(RedraftAllocationParameterDTO poParam);
        APF00110ResultDTO GetAllocationDetail(GetAllocationDetailParameterDTO poParam);
        IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeList();
    }
}
