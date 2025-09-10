using GLTR00100COMMON;
using System.Collections.Generic;

namespace GLTR00100BACK
{
    public interface IGLTR00100
    {
        Task<GLTR00100InitialDTO> GetInitialVar();
        Task<GLTR00100Record<GLTR00100DTO>> GetGLJournal(GLTR00100DTO poParam);
    }

}
