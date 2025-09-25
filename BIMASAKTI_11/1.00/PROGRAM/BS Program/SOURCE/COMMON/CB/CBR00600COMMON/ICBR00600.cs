using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBR00600COMMON
{
    public interface ICBR00600
    {
        Task<CBR00600Record<CBR00600InitialDTO>> GetInitialAsyncDTO();
        IAsyncEnumerable<CBR00600PeriodDTO> GetPeriodList();
    }

}
