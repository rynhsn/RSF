using R_CommonFrontBackAPI;
using FAT00100Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00100Common
{
    /// <summary>
    /// Interface for FAT0010003 - Fixed Asset Transaction Detail operations
    /// </summary>
    public interface IFAT0010003 : R_IServiceCRUDAsyncBase<FAT0010003DTO>
    {
        // Non-streaming methods
        Task<FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>> GetDataHeader(FAT0010003GetDataHeaderParameterDTO poParameter);

        // Streaming methods
        IAsyncEnumerable<FAT0010003GetDataGridResultDTO> GetDataGrid();
    }
}

