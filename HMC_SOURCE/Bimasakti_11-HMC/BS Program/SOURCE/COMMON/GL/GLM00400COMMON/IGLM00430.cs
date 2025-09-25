using System.Collections.Generic;

namespace GLM00400COMMON
{
    public interface IGLM00430
    {
        IAsyncEnumerable<GLM00430DTO> GetSourceAllocationAccountList();
        IAsyncEnumerable<GLM00431DTO> GetAllocationAccountList();
        GLM00431DTO SaveAllocationAccountList(GLM00431DTO poParam);
    }

}
