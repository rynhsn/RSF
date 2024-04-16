using System.Collections.Generic;
using LMT03500Common.DTOs;
using LMT03500Common.Params;

namespace LMT03500Common
{
    public interface ILMT03500UtilityUsage
    {
        IAsyncEnumerable<LMT03500BuildingDTO> LMT03500GetBuildingListStream();
        LMT03500SingleDTO<LMT03500BuildingDTO> LMT03500GetBuildingRecord(LMT03500SearchTextDTO poText);
        IAsyncEnumerable<PMT03500UtilityUsageDTO> LMT03500GetUtilityUsageListStream();
        LMT03500SingleDTO<LMT03500UtilityUsageDetailDTO> LMT03500GetUtilityUsageDetail(LMT03500UtilityUsageDetailParam poParam);
        LMT03500ListDTO<LMT03500FunctDTO> LMT03500GetUtilityTypeList();
        LMT03500ListDTO<LMT03500FloorDTO> LMT03500GetFloorList(LMT03500FloorParam poParam);
        LMT03500ListDTO<LMT03500YearDTO> LMT03500GetYearList();
        LMT03500ListDTO<LMT03500PeriodDTO> LMT03500GetPeriodList(LMT03500PeriodParam poParam);

        LMT03500ExcelDTO LMT03500DownloadTemplateFile();
    }
}