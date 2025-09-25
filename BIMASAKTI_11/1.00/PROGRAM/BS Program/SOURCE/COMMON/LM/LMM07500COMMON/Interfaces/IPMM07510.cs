using PMM07500COMMON.DTO_s.stamp_code;
using PMM07500COMMON.DTO_s.stamp_date;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM07500COMMON.Interfaces
{
    public interface IPMM07510 : R_IServiceCRUDBase<PMM07510GridDTO>
    {
        IAsyncEnumerable<PMM07510GridDTO> GetStampDateList();

    }
}
