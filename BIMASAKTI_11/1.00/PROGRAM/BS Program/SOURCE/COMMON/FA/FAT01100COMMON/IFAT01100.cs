using System.Collections.Generic;
using System.Threading.Tasks;
using FAT01100Common.DTOs;

namespace FAT01100Common
{
    /// <summary>
    /// Interface for FAT01100Cls - Get Transaction List (RSP_FAT01100_GET_TRANS_LIST)
    /// </summary>
    public interface IFAT01100
    {
        /// <summary>
        /// Get transaction list via RSP_FAT01100_GET_TRANS_LIST
        /// </summary>
        Task<FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>>> FAT01100GeTransList(FAT01100GeTransListParameterDTO poParameter);
        Task<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>> FAT01100GetDeptLookupList(FAT01100GetDeptLookupListParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>> FAT01100GetYearRange(FAT01100GetYearRangeParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>> FAT01100GetGetSystemParam(FAT01100GetGetSystemParamParameterDTO poParameter);
    }
}
