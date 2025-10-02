using PMB01800Common.DTOs;
using PMB01800COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMB01800COMMON
{
    public interface IPMB01800
    {
        Task<PMB01800ListBase<PMB01800PropertyDTO>> PMB01800GetPropertyList();
        IAsyncEnumerable<PMB01800GetDepositListDTO> PMB01800GetDepositListStream();
    }
}
