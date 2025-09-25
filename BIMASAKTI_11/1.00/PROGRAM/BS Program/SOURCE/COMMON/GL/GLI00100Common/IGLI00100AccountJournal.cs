using System.Collections.Generic;
using GLI00100Common.DTOs;

namespace GLI00100Common
{
    public interface IGLI00100AccountJournal
    {
        GLI00100AccountAnalysisDetailDTO GLI00100GetAccountAnalysisDetail(GLI00100AccountAnalysisDetailParamDTO poParams);
        IAsyncEnumerable<GLI00100TransactionGridDTO> GLI00100GetTransactionGridStream();
    }
}