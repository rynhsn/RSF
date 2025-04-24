using System.Collections.Generic;
using ICI00200Common.DTOs;
using ICI00200Common.Params;

namespace ICI00200Common
{
    public interface IICI00200
    {
        IAsyncEnumerable<ICI00200ProductDTO> ICI00200GetProductListStream();
        
        IAsyncEnumerable<ICI00200DeptDTO> ICI00200GetDeptListStream();
        IAsyncEnumerable<ICI00200WarehouseDTO> ICI00200GetWarehouseListStream();
        
        ICI00200SingleDTO<ICI00200ProductDetailDTO> ICI00200GetProductDetail(ICI00200DetailParam productDetailParam);
        ICI00200SingleDTO<ICI00200LastInfoDetailDTO> ICI00200GetLastInfoDetail(ICI00200DetailParam productDetailParam);
        ICI00200SingleDTO<ICI00200DeptWareDetailDTO> ICI00200GetDeptWareDetail(ICI00200DetailParam productDetailParam);
    }
}