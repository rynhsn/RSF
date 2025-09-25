using System;
using System.Collections.Generic;
using System.Text;
using R_CommonFrontBackAPI;
using PMM00500Common.DTOs;


namespace PMM00500Common
{
    public interface IPMM00510 : R_IServiceCRUDBase<PMM00510DTO>
    {
        IAsyncEnumerable<PropertyListStreamChargeDTO> GetPropertyListCharges();
        IAsyncEnumerable<PMM00500GridDTO> GetAllChargesList();
        IAsyncEnumerable<ChargesTaxTypeDTO> GetChargesTaxType();
        IAsyncEnumerable<ChargesTaxCodeDTO> GetChargesTaxCode();
        IAsyncEnumerable<AccurualDTO> GetAccrualList();
        ActiveInactiveDTO RSP_LM_ACTIVE_INACTIVE_Method(PMM00510DTO poActiveInactive);
        CopyNewProcessListDTO CopyNewProcess(PMM00510DTO poData);
        IAsyncEnumerable<PMM00510DTO> UnitChargesPrint();
        IAsyncEnumerable<UnitChargesTypeDTO> GetUnitChargeType();
    }
}
