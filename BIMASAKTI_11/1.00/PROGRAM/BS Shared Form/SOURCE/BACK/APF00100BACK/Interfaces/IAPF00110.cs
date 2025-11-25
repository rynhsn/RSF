using APF00100COMMON.DTOs.APF00100;
using APF00100COMMON.DTOs.APF00110;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100BACK
{
    public interface IAPF00110 : R_IServiceCRUDAsyncBase<APF00110ParameterDTO>
    {
        Task<SubmitAllocationResultDTO> SubmitAllocationProcess(SubmitAllocationParameterDTO poParam);
        Task<RedraftAllocationResultDTO> RedraftAllocationProcess(RedraftAllocationParameterDTO poParam);
        Task<APF00110ResultDTO> GetAllocationDetail(GetAllocationDetailParameterDTO poParam);
        IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeList();
    }
}
