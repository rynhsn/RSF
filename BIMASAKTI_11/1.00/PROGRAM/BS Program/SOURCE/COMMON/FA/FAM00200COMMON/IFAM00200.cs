using FAM00200Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAM00200Common
{
    public interface IFAM00200
    {
        IAsyncEnumerable<FAM00200DTO> GetListTaxType();
        Task<FAM00200SingleResult<FAM00200DTO>> GetTaxType(FAM00200DTO poEntity);
        Task<FAM00200SingleResult<FAM00200DTO>> SaveTaxType(FAM00200SaveParameterDTO poEntity);
    }
}
