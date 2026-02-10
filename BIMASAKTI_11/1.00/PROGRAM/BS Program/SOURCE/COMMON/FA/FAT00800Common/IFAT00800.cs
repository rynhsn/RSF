using FAT00800Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00800Common
{
    /// <summary>
    /// Interface for FAT00800 List operations - Transaction List functionality
    /// </summary>
    public interface IFAT00800
    {
        /// <summary>
        /// Get transaction list via RSP_FAT00800_GET_TRANS_LIST (streaming)
        /// </summary>
        IAsyncEnumerable<FAT00800GetTransListResultDTO> FAT00800GetTransList();
        IAsyncEnumerable<FAT00800GetDeptLookupListResultDTO> FAT00800GetDeptLookupList();

        /// <summary>
        /// Get system parameters (delegates to FAT00800Cls)
        /// </summary>
        Task<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>> FAT00800GetGetSystemParam(FAT00800GetGetSystemParamParameterDTO poParameter);

        /// <summary>
        /// Get year range (delegates to FAT00800Cls)
        /// </summary>
        Task<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>> FAT00800GetYearRange(FAT00800GetYearRangeParameterDTO poParameter);
    }
}
