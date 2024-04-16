using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT03500Model.ViewModel
{
    public class PMT03500BuildingLookupViewModel : R_ViewModel<PMT03500BuildingDTO>
    {
        private PMT03500UtilityUsageModel _model = new PMT03500UtilityUsageModel();

        public ObservableCollection<PMT03500BuildingDTO> GridList = new ObservableCollection<PMT03500BuildingDTO>();

        public async Task GetList(object poPropertyId)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID, (string)poPropertyId);
                var loReturn = await _model.GetListStreamAsync<PMT03500BuildingDTO>(nameof(IPMT03500UtilityUsage
                    .LMT03500GetBuildingListStream));
                GridList = new ObservableCollection<PMT03500BuildingDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMT03500BuildingDTO> GetRecord(PMT03500SearchTextDTO poParam)
        {
            var loEx = new R_Exception();
            PMT03500BuildingDTO loReturn = null;

            try
            {
                var loResult = await _model.GetAsync<PMT03500SingleDTO<PMT03500BuildingDTO>, PMT03500SearchTextDTO>(nameof(IPMT03500UtilityUsage.LMT03500GetBuildingRecord), poParam);
                loReturn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn;
        }
    }
}