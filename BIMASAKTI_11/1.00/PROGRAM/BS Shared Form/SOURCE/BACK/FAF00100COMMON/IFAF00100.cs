using System.Collections.Generic;
using System.Threading.Tasks;
using FAF00100COMMON.DTOs;
using R_CommonFrontBackAPI;

namespace FAF00100COMMON
{
    public interface IFAF00100 : R_IServiceCRUDAsyncBase<FAF00100GetAssetResultDTO>
    {
        IAsyncEnumerable<FAF00100GetAssetAllocResultDTO> GetListAssetAlloc();
    }
}
