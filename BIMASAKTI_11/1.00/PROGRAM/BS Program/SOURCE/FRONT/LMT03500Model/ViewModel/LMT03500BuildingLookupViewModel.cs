using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Model.ViewModel
{
    public class LMT03500BuildingLookupViewModel : R_ViewModel<LMT03500BuildingDTO>
    {
        private LMT03500UtilityUsageModel _model = new LMT03500UtilityUsageModel();

        public ObservableCollection<LMT03500BuildingDTO> GridList = new ObservableCollection<LMT03500BuildingDTO>();

        public async Task GetList(object poPropertyId)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CPROPERTY_ID, (string)poPropertyId);
                var loReturn = await _model.GetListStreamAsync<LMT03500BuildingDTO>(nameof(ILMT03500UtilityUsage
                    .LMT03500GetBuildingListStream));
                GridList = new ObservableCollection<LMT03500BuildingDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LMT03500BuildingDTO> GetRecord(LMT03500SearchTextDTO poParam)
        {
            var loEx = new R_Exception();
            LMT03500BuildingDTO loReturn = null;

            try
            {
                var loResult = await _model.GetAsync<LMT03500SingleDTO<LMT03500BuildingDTO>, LMT03500SearchTextDTO>(nameof(ILMT03500UtilityUsage.LMT03500GetBuildingRecord), poParam);
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