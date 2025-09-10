using PMM07500COMMON.DTO_s.stamp_code;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM07500COMMON.Interfaces
{
    public interface IPMM07500 : R_IServiceCRUDBase<PMM07500GridDTO>
    {
        IAsyncEnumerable<PMM07500GridDTO> GetStampList();

    }
}
