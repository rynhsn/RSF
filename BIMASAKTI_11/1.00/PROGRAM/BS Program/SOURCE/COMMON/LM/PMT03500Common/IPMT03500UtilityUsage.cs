using System.Collections.Generic;
using PMT03500Common.DTOs;
using PMT03500Common.Params;

namespace PMT03500Common
{
    public interface IPMT03500UtilityUsage
    {
        IAsyncEnumerable<PMT03500BuildingDTO> LMT03500GetBuildingListStream();
        PMT03500SingleDTO<PMT03500BuildingDTO> LMT03500GetBuildingRecord(PMT03500SearchTextDTO poText);
        IAsyncEnumerable<PMT03500UtilityUsageDTO> LMT03500GetUtilityUsageListStream();
        PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO> LMT03500GetUtilityUsageDetail(PMT03500UtilityUsageDetailParam poParam);
        PMT03500ListDTO<PMT03500FunctDTO> LMT03500GetUtilityTypeList();
        PMT03500ListDTO<PMT03500FloorDTO> LMT03500GetFloorList(PMT03500FloorParam poParam);
        PMT03500ListDTO<PMT03500YearDTO> LMT03500GetYearList();
        PMT03500ListDTO<PMT03500PeriodDTO> LMT03500GetPeriodList(PMT03500PeriodParam poParam);

        PMT03500ExcelDTO LMT03500DownloadTemplateFile();
    }
}