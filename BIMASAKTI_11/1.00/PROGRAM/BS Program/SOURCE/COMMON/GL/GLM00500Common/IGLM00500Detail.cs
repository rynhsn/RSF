using GLM00500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GLM00500Common
{
    public interface IGLM00500Detail : R_IServiceCRUDBase<GLM00500BudgetDTDTO>
    {
        GLM00500ListDTO<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTList();
        GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetRoundingMethodList();
        GLM00500PeriodCount GLM00500GetPeriodCount();
    }
}