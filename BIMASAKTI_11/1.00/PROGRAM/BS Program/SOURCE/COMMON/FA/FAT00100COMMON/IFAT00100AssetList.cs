using FAT00100Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00100Common
{
    /// <summary>
    /// Interface for FAT00100 Asset List operations
    /// </summary>
    public interface IFAT00100AssetList
    {
        // Streaming methods
        IAsyncEnumerable<FAT00100GetTransAssetListResultDTO> FAT00100GetTransAssetList();

        // Non-streaming methods
        Task<FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>> FAT00100GetTransAsset(FAT00100GetTransAssetParameterDTO poParameter);
    }
}

