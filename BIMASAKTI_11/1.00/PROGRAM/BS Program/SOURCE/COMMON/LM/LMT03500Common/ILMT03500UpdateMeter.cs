using System.Collections.Generic;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using R_CommonFrontBackAPI;

namespace LMT03500Common
{
    public interface ILMT03500UpdateMeter
    {
        IAsyncEnumerable<LMT03500UtilityMeterDTO> LMT03500GetUtilityMeterListStream();
        IAsyncEnumerable<LMT03500BuildingUnitDTO> LMT03500GetBuildingUnitListStream();
        LMT03500SingleDTO<LMT03500UtilityMeterDetailDTO> LMT03500GetUtilityMeterDetail(
            LMT03500UtilityMeterDetailParam poParam);
        LMT03500SingleDTO<LMT03500AgreementUtilitiesDTO> LMT03500GetAgreementUtilities(
            LMT03500AgreementUtilitiesParam poParam);
        void LMT03500UpdateMeterNo(LMT03500UpdateChangeMeterNoParam poParam);
        void LMT03500ChangeMeterNo(LMT03500UpdateChangeMeterNoParam poParam);
    }
}