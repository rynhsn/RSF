using System;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Model.ViewModel
{
    public class LMT03500UtilityDetailViewModel : R_ViewModel<LMT03500UtilityUsageDetailDTO>
    {
        private LMT03500UtilityUsageModel _model = new LMT03500UtilityUsageModel();
        public PMT03500DetailParam Param = new PMT03500DetailParam();
        public LMT03500UtilityUsageDetailDTO Entity = new LMT03500UtilityUsageDetailDTO();

        public string DisplayEC = "d-none";
        public string DisplayWG = "d-none";

        public async Task Init(object obj)
        {
            // var loParam = (LMT03500UtilityUsageDTO)obj;     
            Param = (PMT03500DetailParam)obj;
            
            switch (Param.EUTILITY_TYPE)
            {
                case ELMT03500UtilityUsageType.EC:
                    DisplayEC = "d-block";
                    break;
                case ELMT03500UtilityUsageType.WG:
                    DisplayWG = "d-block";
                    break;
            }

            await GetRecord(Param.OUTILITY_HEADER);
        }

        public async Task GetRecord(LMT03500UtilityUsageDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new LMT03500UtilityUsageDetailParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CREF_NO = poParam.CREF_NO,
                    CCHARGES_TYPE = poParam.CCHARGES_TYPE
                };
                var loReturn =
                    await _model
                        .GetAsync<LMT03500SingleDTO<LMT03500UtilityUsageDetailDTO>, LMT03500UtilityUsageDetailParam>(
                            nameof(ILMT03500UtilityUsage.LMT03500GetUtilityUsageDetail), loParam);
                Entity = loReturn.Data;
                Entity.CINV_YEAR = Entity.CINV_PRD.Substring(0, 4);
                Entity.CINV_MONTH = Entity.CINV_PRD.Substring(4, 2);
                Entity.CUTILITY_YEAR = Entity.CUTILITY_PRD.Substring(0, 4);
                Entity.CUTILITY_MONTH = Entity.CUTILITY_PRD.Substring(4, 2);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}