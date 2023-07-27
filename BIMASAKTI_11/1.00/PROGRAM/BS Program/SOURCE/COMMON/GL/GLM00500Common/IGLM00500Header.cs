using GLM00500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GLM00500Common
{
    public interface IGLM00500Header : R_IServiceCRUDBase<GLM00500BudgetHDDTO>
    {
        GLM00500ListDTO<GLM00500BudgetHDDTO> GLM00500GetBudgetHDList();
        GLM00500GSMPeriodDTO GLM00500GetPeriods();
        GLM00500GLSystemParamDTO GLM00500GetSystemParams();
        GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetCurrencyTypeList();
        void GLM00500FinalizeBudget();
        GLM00500AccountBudgetExcelDTO GLM00500DownloadTemplateFile();
    }
}