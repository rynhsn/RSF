using PMM07500COMMON.DTO_s.stamp_amount;
using PMM07500COMMON.DTO_s.stamp_code;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM07500COMMON.Interfaces
{
    public interface IPMM07520 : R_IServiceCRUDBase<PMM07520GridDTO>
    {
        IAsyncEnumerable<PMM07520GridDTO> GetStampAmountList();

    }
}
