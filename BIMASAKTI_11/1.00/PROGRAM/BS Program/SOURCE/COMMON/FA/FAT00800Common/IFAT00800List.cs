using FAT00800Common.DTOs;
using System.Collections.Generic;

namespace FAT00800Common
{
    /// <summary>
    /// Interface for FAT00800 List operations - Transaction List functionality
    /// </summary>
    public interface IFAT00800List
    {
        // Streaming methods for list operations
        IAsyncEnumerable<FAT00800TransListResultDTO> FAT00800TransList();
    }
}
