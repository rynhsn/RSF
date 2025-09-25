using System.Collections.Generic;

namespace GLM00400COMMON
{
    public interface IGLM00400
    {
        GLM00400InitialDTO GetInitialVar(GLM00400InitialDTO poParam);
        GLM00400GLSystemParamDTO GetSystemParam(GLM00400GLSystemParamDTO poParam);
        IAsyncEnumerable<GLM00400DTO> GetAllocationJournalHDList();
    }

}
