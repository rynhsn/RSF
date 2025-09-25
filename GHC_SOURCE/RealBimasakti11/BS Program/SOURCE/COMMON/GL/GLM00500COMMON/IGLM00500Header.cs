using System.Collections.Generic;
using GLM00500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GLM00500Common
{
    public interface IGLM00500Header : R_IServiceCRUDBase<GLM00500BudgetHDDTO>
    {
        IAsyncEnumerable<GLM00500BudgetHDDTO> GLM00500GetBudgetHDListStream();
        GLM00500GSMPeriodDTO GLM00500GetPeriods();
        GLM00500GLSystemParamDTO GLM00500GetSystemParams();
        GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetCurrencyTypeList();
        GLM00500ReturnDTO GLM00500FinalizeBudget(GLM00500CrecParamsDTO poParams);
        GLM00500AccountBudgetExcelDTO GLM00500DownloadTemplateFile();
    }
}