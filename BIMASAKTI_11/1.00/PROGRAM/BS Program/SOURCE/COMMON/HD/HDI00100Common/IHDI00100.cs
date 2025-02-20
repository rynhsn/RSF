using System.Collections.Generic;
using HDI00100Common.DTOs;

namespace HDI00100Common
{
    public interface IHDI00100
    {
        HDI00100ListDTO<HDI00100PropertyDTO> HDI00100GetPropertyList();
        IAsyncEnumerable<HDI00100TaskSchedulerDTO> HDI00100GetTaskSchedulerListStream();
    }
}