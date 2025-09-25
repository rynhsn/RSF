using PMT00100COMMON.Booking;
using PMT00100COMMON.ChangeUnit;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT00100COMMON.Interface
{
    public interface IPM00100ChangeUnit : R_IServiceCRUDBase<PMT00100ChangeUnitDTO>
    {
        PMT00100ChangeUnitDTO GetAgreementUnitInfoDetail(PMT00100ChangeUnitDTO poParam);
    }
}
