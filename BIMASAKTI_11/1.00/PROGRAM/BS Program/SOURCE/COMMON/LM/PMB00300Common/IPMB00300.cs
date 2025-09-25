using System;
using System.Collections.Generic;
using PMB00300Common.DTOs;
using PMB00300Common.Params;

namespace PMB00300Common
{
    public interface IPMB00300
    {
        PMB00300ListDTO<PMB00300PropertyDTO> PMB00300GetPropertyList();
        IAsyncEnumerable<PMB00300RecalcDTO> PMB00300GetRecalcListStream();
        IAsyncEnumerable<PMB00300RecalcChargesDTO> PMB00300GetRecalcChargesListStream();
        IAsyncEnumerable<PMB00300RecalcRuleDTO> PMB00300GetRecalcRuleListStream();
        PMB00300SingleDTO<bool> PMB00300RecalcBillingRuleProcess(PMB00300RecalcProcessParam poEntity);
    }
}