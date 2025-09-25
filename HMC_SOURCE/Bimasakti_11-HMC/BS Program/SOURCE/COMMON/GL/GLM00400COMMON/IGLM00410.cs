using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GLM00400COMMON
{
    public interface IGLM00410 : R_IServiceCRUDBase<GLM00410DTO>
    {
        IAsyncEnumerable<GLM00411DTO> GetAllocationAccountList();
        IAsyncEnumerable<GLM00412DTO> GetAllocationTargetCenterList();
        IAsyncEnumerable<GLM00413DTO> GetAllocationTargetCenterByPeriodList();
        IAsyncEnumerable<GLM00414DTO> GetAllocationPeriodList();
        IAsyncEnumerable<GLM00415DTO> GetAllocationPeriodByTargetCenterList();

        GLM00400Result<GLM00413DTO> GetAllocationTargetCenterByPeriod(GLM00413DTO poParam);
        GLM00400Result<GLM00413DTO> SaveAllocationTargetCenterByPeriod(GLM00413DTO poParam);
    }

}
