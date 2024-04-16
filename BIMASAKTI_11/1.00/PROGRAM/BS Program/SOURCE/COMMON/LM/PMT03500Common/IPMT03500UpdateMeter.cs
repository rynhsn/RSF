using System.Collections.Generic;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_CommonFrontBackAPI;

namespace PMT03500Common
{
    public interface IPMT03500UpdateMeter
    {
        IAsyncEnumerable<PMT03500UtilityMeterDTO> LMT03500GetUtilityMeterListStream();
        IAsyncEnumerable<PMT03500BuildingUnitDTO> LMT03500GetBuildingUnitListStream();
        
        PMT03500SingleDTO<PMT03500BuildingUnitDTO> LMT03500GetBuildingUnitRecord(PMT03500SearchTextDTO poParam);
        PMT03500SingleDTO<PMT03500UtilityMeterDetailDTO> LMT03500GetUtilityMeterDetail(
            PMT03500UtilityMeterDetailParam poParam);
        PMT03500SingleDTO<PMT03500AgreementUtilitiesDTO> LMT03500GetAgreementUtilities(
            PMT03500AgreementUtilitiesParam poParam);
        void LMT03500UpdateMeterNo(PMT03500UpdateChangeMeterNoParam poParam);
        void LMT03500ChangeMeterNo(PMT03500UpdateChangeMeterNoParam poParam);
    }
}