using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public interface IPMM01000 : R_IServiceCRUDBase<PMM01000DTO>
    {
        IAsyncEnumerable<PMM01002DTO> GetChargesUtilityList();
        PMM01000DTO PMM01000ActiveInactive(PMM01000DTO poParam);
        PMM01003DTO PMM01000CopyNewCharges(PMM01003DTO poParam);
        PMM01000Record<PMM01000BeforeDeleteDTO> ValidateBeforeDelete(PMM01000DTO poParam);

    }

}
