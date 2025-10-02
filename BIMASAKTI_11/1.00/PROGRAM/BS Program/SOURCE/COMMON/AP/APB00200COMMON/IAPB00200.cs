using APB00200COMMON.DTO_s;
using APB00200COMMON.DTO_s.Helper;
using System.Collections.Generic;

namespace APB00200COMMON
{
    public interface IAPB00200
    {
        public RecordResultAPI<ClosePeriodDTO> GetRecord_ClosePeriod(ClosePeriodParam poParam);
        public RecordResultAPI<CloseAPProcessResultDTO> ProcessCloseAPPeriod(CloseAPProcessParam poParam);
        public IAsyncEnumerable<ErrorCloseAPProcessDTO> GetList_ErrorProcessCloseAPPeriod(CloseAPProcessParam poParam);
    }
}
