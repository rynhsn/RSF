using System.Collections.Generic;
using PMT03500Common.DTOs;
using PMT03500Common.Params;

namespace PMT03500Common
{
    public interface IPMT03500UtilityUsage
    {
        PMT03500SingleDTO<PMT03500SystemParamDTO> PMT03500GetSystemParam(PMT03500SystemParamParameter poParam);
        IAsyncEnumerable<PMT03500BuildingDTO> PMT03500GetBuildingListStream();
        PMT03500SingleDTO<PMT03500BuildingDTO> PMT03500GetBuildingRecord(PMT03500SearchTextDTO poText);
        IAsyncEnumerable<PMT03500UtilityUsageDTO> PMT03500GetUtilityUsageListStream();
        IAsyncEnumerable<PMT03500UtilityUsageDTO> PMT03500GetUtilityCutOffListStream();
        PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO> PMT03500GetUtilityUsageDetail(PMT03500UtilityUsageDetailParam poParam);
        PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO> PMT03500GetUtilityUsageDetailPhoto(PMT03500UtilityUsageDetailParam poParam);
        PMT03500ListDTO<PMT03500FunctDTO> PMT03500GetUtilityTypeList();
        PMT03500ListDTO<PMT03500FloorDTO> PMT03500GetFloorList(PMT03500FloorParam poParam);
        PMT03500ListDTO<PMT03500YearDTO> PMT03500GetYearList();
        PMT03500ListDTO<PMT03500PeriodDTO> PMT03500GetPeriodList(PMT03500PeriodParam poParam);
        PMT03500SingleDTO<PMT03500PeriodDTO> PMT03500GetPeriod(PMT03500PeriodParam poParam);
        PMT03500ListDTO<PMT03500RateWGListDTO> PMT03500GetRateWGList(PMT03500RateParam poParam);

        PMT03500ExcelDTO PMT03500DownloadTemplateFile();
    }
}