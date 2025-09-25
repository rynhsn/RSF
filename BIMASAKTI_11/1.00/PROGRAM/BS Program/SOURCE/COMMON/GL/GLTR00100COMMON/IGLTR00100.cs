using System.Collections.Generic;

namespace GLTR00100COMMON
{
    public interface IGLTR00100
    {
        GLTR00100InitialDTO GetInitialVar();
        GLTR00100Record<GLTR00100DTO> GetGLJournal(GLTR00100DTO poParam);
    }

}
