using System.Collections.Generic;
using LMT03500Common.DTOs;

namespace LMT03500Common
{
    public interface ILMT03500UpdateMeter
    {
        IAsyncEnumerable<LMT03500UtilityMeterDTO> LMT03500GetUtilityMeterListStream();
        IAsyncEnumerable<LMT03500BuildingUnitDTO> LMT03500GetBuildingUnitListStream();
    }
}