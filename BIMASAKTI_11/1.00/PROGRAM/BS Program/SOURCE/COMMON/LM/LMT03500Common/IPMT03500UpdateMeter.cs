using System.Collections.Generic;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_CommonFrontBackAPI;

namespace PMT03500Common
{
    public interface IPMT03500UpdateMeter
    {
        IAsyncEnumerable<PMT03500UtilityMeterDTO> PMT03500GetUtilityMeterListStream();
        IAsyncEnumerable<PMT03500BuildingUnitDTO> PMT03500GetBuildingUnitListStream();
        PMT03500ListDTO<PMT03500MeterNoDTO> PMT03500GetMeterNoList(PMT03500MeterNoParam poParam);
        PMT03500ListDTO<PMT03500PeriodRangeDTO> PMT03500GetPeriodRangeList(PMT03500PeriodRangeParam poParam);
        
        PMT03500SingleDTO<PMT03500BuildingUnitDTO> PMT03500GetBuildingUnitRecord(PMT03500SearchTextDTO poParam);
        PMT03500SingleDTO<PMT03500UtilityMeterDetailDTO> PMT03500GetUtilityMeterDetail(
            PMT03500UtilityMeterDetailParam poParam);
        PMT03500SingleDTO<PMT03500AgreementUtilitiesDTO> PMT03500GetAgreementUtilities(
            PMT03500AgreementUtilitiesParam poParam);
        PMT03500UtilityMeterDetailDTO PMT03500UpdateMeterNo(PMT03500UpdateChangeMeterNoParam poParam);
        PMT03500UtilityMeterDetailDTO PMT03500ChangeMeterNo(PMT03500UpdateChangeMeterNoParam poParam);
    }
}