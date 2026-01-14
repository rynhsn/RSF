using R_CommonFrontBackAPI;
using FAT00100Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00100Common
{
    /// <summary>
    /// Interface for FAT0010002 - Fixed Asset Acquisition Detail operations
    /// </summary>
    public interface IFAT0010002 : R_IServiceCRUDAsyncBase<FAT0010002DTO>
    {
        // Non-streaming methods
        Task<FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>> GetFAAcquisitionDetailHeader(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter);
        Task<FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT0010002ValidateDeptCodeParameterDTO poParameter);
        Task<FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>> GetDecliningDeprAmt(FAT0010002GetDecliningDeprAmtParameterDTO poParameter);
        Task<FAT0010002ResultDTO<FAT0010002GetTransDetailResultDTO>> FAT0010002GetTransDetail(FAT0010002GetTransDetailParameterDTO poParameter);

        // Streaming methods
        IAsyncEnumerable<FAT00100GetStatusListResultDTO> GetComboDepreciationMethod();
        IAsyncEnumerable<FAT0010002GetFAAcquisitionDetailAssetListResultDTO> GetFAAcquisitionDetailAssetList();
        IAsyncEnumerable<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO> GetFAAcquisitionDetailAllocExpenPageList();
        IAsyncEnumerable<FAT00100GetTransExpAllocListResultDTO> FAT00100GetTransExpAllocList();
    }
}
