using PMR00150COMMON.Utility_Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public interface IPMR00150
    {
        IAsyncEnumerable<PMR00150PropertyDTO> GetPropertyListStream();
        PMR00150InitialProcess GetInitialProcess();
    }
}
