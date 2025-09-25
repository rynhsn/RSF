using System.Collections.Generic;
using GLI00100Common.DTOs;

namespace GLI00100Common
{
    public interface IGLI00100
    {
        GLI00100AccountDTO GLI00100GetAccountDetail(GLI00100AccountParameterDTO poParams);
        GLI00100AccountAnalysisDTO GLI00100GetAccountAnalysisDetail(GLI00100AccountAnalysisParameterDTO poParams);
        IAsyncEnumerable<GLI00100BudgetDTO> GLI00100GetBudgetStream(GLI00100BudgetParamsDTO poParams);
    }
}