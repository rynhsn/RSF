using GLF00100COMMON;
using System.Collections.Generic;

namespace GLF00100BACK
{
    public interface IGLF00100
    {
        Task<GLF00100SingleResult<GLF00100InitialDTO>> GetInfoCompany();
        Task<GLF00100SingleResult<GLF00100DTO>> GetJournalDetail(GLF00100ParameterDTO poParam);
        IAsyncEnumerable<GLF00101DTO> GetJournalDetailList();
    }
}
