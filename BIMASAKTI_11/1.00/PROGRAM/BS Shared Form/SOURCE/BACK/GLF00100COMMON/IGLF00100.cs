using System.Collections.Generic;

namespace GLF00100COMMON
{
    public interface IGLF00100
    {
        GLF00100SingleResult<GLF00100InitialDTO> GetInfoCompany();
        GLF00100SingleResult<GLF00100DTO> GetJournalDetail(GLF00100ParameterDTO poParam);
        IAsyncEnumerable<GLF00101DTO> GetJournalDetailList();
    }
}
