using System.Collections.Generic;
using System.Threading.Tasks;
using GSM02000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM02000Common
{
    public interface IGSM02000 : R_IServiceCRUDBase<GSM02000DTO>
    {
        GSM02000ListDTO<GSM02000GridDTO> GetAllSalesTax();
        IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream();
        GSM02000ListDTO<GSM02000RoundingDTO> GetAllRounding();
        GSM02000ActiveInactiveDTO SetActiveInactive();
    }
}