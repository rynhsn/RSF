using GSM02000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM02000Common
{
    public interface IGSM02000 : R_IServiceCRUDBase<GSM02000DTO>
    {
        GSM02000ListDTO GetAllSalesTax();
    }
}