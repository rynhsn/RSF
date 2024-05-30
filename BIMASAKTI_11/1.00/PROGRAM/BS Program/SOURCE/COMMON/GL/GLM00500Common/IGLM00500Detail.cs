using System.Collections.Generic;
using GLM00500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GLM00500Common
{
    public interface IGLM00500Detail : R_IServiceCRUDBase<GLM00500BudgetDTDTO>
    {
        IAsyncEnumerable<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTListStream();
        GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetRoundingMethodList();
        IAsyncEnumerable<GLM00500BudgetWeightingDTO> GLM00500GetBudgetWeightingListStream();
        GLM00500PeriodCountDTO GLM00500GetPeriodCount(GLM00500YearParamsDTO poParams);
        GLM00500GSMCompanyDTO GLM00500GetGSMCompany();
        GLM00500BudgetCalculateDTO GLM00500BudgetCalculate(GLM00500CalculateParamDTO poParams);
        GLM00500ReturnDTO GLM00500GenerateBudget(GLM00500GenerateAccountBudgetDTO poGenerateAccountBudgetDTO);
    }
    
}