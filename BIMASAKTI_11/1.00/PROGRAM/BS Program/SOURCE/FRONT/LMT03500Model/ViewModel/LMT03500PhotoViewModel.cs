using System;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMT03500Model.ViewModel
{
    public class LMT03500PhotoViewModel : R_ViewModel<LMT03500UtilityUsageDetailDTO>
    {        
        private LMT03500UtilityUsageModel _model = new LMT03500UtilityUsageModel();

        public LMT03500UtilityUsageDetailDTO Entity = new LMT03500UtilityUsageDetailDTO();

        public async Task Init(object obj)
        {
            var loEntity = (LMT03500UtilityUsageDTO)obj;
            var loEnt = R_FrontUtility.ConvertObjectToObject<LMT03500UtilityUsageDetailDTO>(loEntity);
            await GetRecord(loEnt);
        }

        public async Task GetRecord(LMT03500UtilityUsageDetailDTO poEntity)
        {
            Entity = poEntity;
            await Task.CompletedTask;
        }
    }

    public class LMT03500UtilityDetailViewModel : R_ViewModel<LMT03500UtilityUsageDetailDTO>
    {
        private LMT03500UtilityUsageModel _model = new LMT03500UtilityUsageModel();

        public LMT03500UtilityUsageDetailDTO Entity = new LMT03500UtilityUsageDetailDTO();

        public async Task Init(object obj)
        {
            var loParam = (LMT03500UtilityUsageDTO)obj;     
            await GetRecord(loParam);
        }

        public async Task GetRecord(LMT03500UtilityUsageDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new LMT03500UtilityUsageDetailParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CREF_NO = poParam.CREF_NO
                };
                var loReturn =
                    await _model.GetAsync<LMT03500SingleDTO<LMT03500UtilityUsageDetailDTO>, LMT03500UtilityUsageDetailParam>(
                        nameof(ILMT03500UtilityUsage.LMT03500GetUtilityUsageDetail), loParam);
                Entity = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}