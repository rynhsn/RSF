using GLM00100Common.DTOs;
using R_CommonFrontBackAPI;

namespace GLM00100Common
{
    public interface IGLM00100 : R_IServiceCRUDBase<GLM00100DTO>
    {
        GLM00100CreateSystemParameterResultDTO CreateSystemParameter();
        GLM00100GSMPeriod GetStartingPeriodYear();
        GLM00100ResultData GetCheckerSystemParameter();
        
        
    }
}