using PMB02200COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMB02200COMMON
{
    public interface IPMB02200
    {
        IAsyncEnumerable<UtilityChargesDTO> GetUtilityCharges();

    }
}
