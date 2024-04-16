using System;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT03500Model.ViewModel
{
    public class PMT03500UtilityDetailViewModel : R_ViewModel<PMT03500UtilityUsageDetailDTO>
    {
        private PMT03500UtilityUsageModel _model = new PMT03500UtilityUsageModel();
        public PMT03500DetailParam Param = new PMT03500DetailParam();
        public PMT03500UtilityUsageDetailDTO Entity = new PMT03500UtilityUsageDetailDTO();

        public string DisplayEC = "d-none";
        public string DisplayWG = "d-none";

        public async Task Init(object obj)
        {
            // var loParam = (PMT03500UtilityUsageDTO)obj;     
            Param = (PMT03500DetailParam)obj;
            
            switch (Param.EUTILITY_TYPE)
            {
                case EPMT03500UtilityUsageType.EC:
                    DisplayEC = "d-block";
                    break;
                case EPMT03500UtilityUsageType.WG:
                    DisplayWG = "d-block";
                    break;
            }

            await GetRecord(Param.OUTILITY_HEADER);
        }

        public async Task GetRecord(PMT03500UtilityUsageDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UtilityUsageDetailParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CREF_NO = poParam.CREF_NO,
                    CCHARGES_TYPE = poParam.CCHARGES_TYPE
                };
                var loReturn =
                    await _model
                        .GetAsync<PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO>, PMT03500UtilityUsageDetailParam>(
                            nameof(IPMT03500UtilityUsage.LMT03500GetUtilityUsageDetail), loParam);
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