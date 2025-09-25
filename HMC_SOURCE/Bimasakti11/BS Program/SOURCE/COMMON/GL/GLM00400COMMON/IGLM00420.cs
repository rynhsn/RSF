using System.Collections.Generic;

namespace GLM00400COMMON
{
    public interface IGLM00420
    {
        IAsyncEnumerable<GLM00420DTO> GetSourceAllocationCenterList();
        IAsyncEnumerable<GLM00421DTO> GetAllocationCenterList();
        GLM00421DTO SaveAllocationCenterList(GLM00421DTO poParam);
    }

}
