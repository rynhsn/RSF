using System.Threading.Tasks;
using PMT03500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Model.ViewModel
{
    public class PMT03500PhotoViewModel : R_ViewModel<PMT03500UtilityUsageDetailDTO>
    {        
        private PMT03500UtilityUsageModel _model = new PMT03500UtilityUsageModel();

        public PMT03500UtilityUsageDetailDTO Entity = new PMT03500UtilityUsageDetailDTO();

        public async Task Init(object obj)
        {
            var loEntity = (PMT03500UtilityUsageDTO)obj;
            var loEnt = R_FrontUtility.ConvertObjectToObject<PMT03500UtilityUsageDetailDTO>(loEntity);
            await GetRecord(loEnt);
        }

        public async Task GetRecord(PMT03500UtilityUsageDetailDTO poEntity)
        {
            Entity = poEntity;
            await Task.CompletedTask;
        }
    }
}