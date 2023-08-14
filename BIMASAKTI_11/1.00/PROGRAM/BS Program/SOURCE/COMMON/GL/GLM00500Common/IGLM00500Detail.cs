using System.Collections.Generic;
using GLM00500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GLM00500Common
{
    public interface IGLM00500Detail : R_IServiceCRUDBase<GLM00500BudgetDTDTO>
    {
        GLM00500ListDTO<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTList();
        GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetRoundingMethodList();
        GLM00500ListDTO<GLM00500BudgetWeightingDTO> GLM00500GetBudgetWeightingList();
        GLM00500PeriodCount GLM00500GetPeriodCount();
        GLM00500GSMCompanyDTO GLM00500GetGSMCompany();
        GLM00500BudgetCalculateDTO GLM00500BudgetCalculate();
        void GLM00500GenerateBudget(GLM00500GenerateAccountBudgetDTO poGenerateAccountBudgetDTO);
    }
    
    public interface IGLM00500Upload
    {
        GLM00500UploadCheckErrorDTO GLM00500UploadCheckBudget(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO);
        
        void GLM00500UploadBudget(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO);
        
        GLM00500ListDTO<GLM00500UploadFromSystemDTO> GLM00500UploadGetBudgetList();

        GLM00500UploadErrorDTO GLM00500UploadGetErrorMsg();
    }
}