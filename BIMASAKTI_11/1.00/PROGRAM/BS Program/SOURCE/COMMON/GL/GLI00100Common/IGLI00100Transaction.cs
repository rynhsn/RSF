using System.Collections.Generic;
using GLI00100Common.DTOs;

namespace GLI00100Common
{
    public interface IGLI00100Transaction
    {
        GLI00100JournalDTO GLI00100GetJournalDetail(GLI00100JournalParamDTO poParams);
        IAsyncEnumerable<GLI00100JournalGridDTO> GLI00100GetJournalGridStream();
    }
}