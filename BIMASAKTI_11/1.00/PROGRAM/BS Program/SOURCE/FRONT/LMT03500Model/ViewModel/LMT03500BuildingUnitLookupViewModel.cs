using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Model.ViewModel
{
    public class LMT03500BuildingUnitLookupViewModel : R_ViewModel<LMT03500BuildingDTO>
    {
        private LMT03500UpdateMeterModel _model = new LMT03500UpdateMeterModel();
        
        public ObservableCollection<LMT03500BuildingUnitDTO> GridList = new ObservableCollection<LMT03500BuildingUnitDTO>();
        
        public async Task GetList(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMT03500UpdateMeterHeader)poParam;
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CPROPERTY_ID,loParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CBUILDING_ID,loParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CFLOOR_ID, string.Empty);
                var loReturn = await _model.GetListStreamAsync<LMT03500BuildingUnitDTO>(nameof(ILMT03500UpdateMeter
                    .LMT03500GetBuildingUnitListStream));
                GridList = new ObservableCollection<LMT03500BuildingUnitDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}