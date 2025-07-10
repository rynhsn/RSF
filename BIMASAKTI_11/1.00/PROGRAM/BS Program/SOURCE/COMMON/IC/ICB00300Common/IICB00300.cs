using System.Collections.Generic;
using ICB00300Common.DTOs;

namespace ICB00300Common
{
    public interface IICB00300
    {
        ICB00300ListDTO<ICB00300PropertyDTO> ICB00300GetPropertyList();
        IAsyncEnumerable<ICB00300ProductDTO> ICB00300GetProductListStream();
    }
}