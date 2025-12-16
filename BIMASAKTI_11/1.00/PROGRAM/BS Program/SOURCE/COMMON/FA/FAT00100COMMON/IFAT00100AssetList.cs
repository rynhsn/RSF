using FAT00100Common.DTOs;
using System.Collections.Generic;

namespace FAT00100Common
{
    /// <summary>
    /// Interface for FAT00100 Asset List operations
    /// </summary>
    public interface IFAT00100AssetList
    {
        // Streaming methods
        IAsyncEnumerable<FAT00100GetAssetListResultDTO> GetAssetList();
    }
}

