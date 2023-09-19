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
        GLM00500PeriodCount GLM00500GetPeriodCount(GLM00500YearParamsDTO poParams);
        GLM00500GSMCompanyDTO GLM00500GetGSMCompany();
        GLM00500BudgetCalculateDTO GLM00500BudgetCalculate();
        void GLM00500GenerateBudget(GLM00500GenerateAccountBudgetDTO poGenerateAccountBudgetDTO);
    }
    
    public interface IGLM00500Upload
    {
        GLM00500UploadErrorReturnDTO GLM00500UploadGetBudgetList();
    }
}