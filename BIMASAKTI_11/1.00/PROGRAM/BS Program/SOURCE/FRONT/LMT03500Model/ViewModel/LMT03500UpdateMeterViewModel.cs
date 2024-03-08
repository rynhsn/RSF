using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Model.ViewModel
{
    public class LMT03500UpdateMeterViewModel : R_ViewModel<LMT03500UtilityMeterDTO>
    {
        private LMT03500UpdateMeterModel _model = new LMT03500UpdateMeterModel();
        public LMT03500UpdateMeterHeader Header = new LMT03500UpdateMeterHeader();
        
        public ObservableCollection<LMT03500UtilityMeterDTO> GridList = new ObservableCollection<LMT03500UtilityMeterDTO>();
        public LMT03500UtilityMeterDTO Entity = new LMT03500UtilityMeterDTO();

        public async Task Init(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                Header.CPROPERTY_ID = poParam.ToString();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetList(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMT03500UpdateMeterHeader)poParam;
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CPROPERTY_ID,loParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CBUILDING_ID,loParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CFLOOR_ID, string.Empty);
                var loReturn = await _model.GetListStreamAsync<LMT03500UtilityMeterDTO>(nameof(ILMT03500UpdateMeter
                    .LMT03500GetUtilityMeterListStream));
                GridList = new ObservableCollection<LMT03500UtilityMeterDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public void GetRecord(LMT03500UtilityMeterDTO poEntity)
        {
            Entity = poEntity;
        }
        
    }
}