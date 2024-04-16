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
        
        public ObservableCollection<PMT03500BuildingUnitDTO> GridList = new ObservableCollection<PMT03500BuildingUnitDTO>();
        
        public async Task GetList(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMT03500UpdateMeterHeader)poParam;
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID,loParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CBUILDING_ID,loParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CFLOOR_ID, string.Empty);
                var loReturn = await _model.GetListStreamAsync<PMT03500BuildingUnitDTO>(nameof(ILMT03500UpdateMeter
                    .LMT03500GetBuildingUnitListStream));
                GridList = new ObservableCollection<PMT03500BuildingUnitDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task<PMT03500BuildingUnitDTO> GetRecord(LMT03500SearchTextDTO poParam)
        {
            var loEx = new R_Exception();
            PMT03500BuildingUnitDTO loReturn = null;

            try
            {
                var loResult = await _model.GetAsync<LMT03500SingleDTO<PMT03500BuildingUnitDTO>, LMT03500SearchTextDTO>(nameof(ILMT03500UpdateMeter.LMT03500GetBuildingUnitRecord), poParam);
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