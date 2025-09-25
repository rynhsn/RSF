using System.Collections.Generic;

namespace PMR00170COMMON
{
    public interface IPMR00170
    {
        IAsyncEnumerable<PMR00170PropertyDTO> GetPropertyListStream();
        PMR00170InitialProcess GetInitialProcess();
    }
}
