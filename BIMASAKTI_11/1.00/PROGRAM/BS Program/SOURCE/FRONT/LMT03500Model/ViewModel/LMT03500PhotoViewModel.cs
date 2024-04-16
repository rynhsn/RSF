using System.Threading.Tasks;
using LMT03500Common.DTOs;
using R_BlazorFrontEnd;
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
}