using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PMB00300Common;
using PMB00300Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMB00300Model.ViewModel
{
    public class PMB00300ViewModel : R_ViewModel<PMB00300RecalcDTO>
    {
        private PMB00300Model _model = new PMB00300Model();
        public ObservableCollection<PMB00300RecalcDTO> GridList = new ObservableCollection<PMB00300RecalcDTO>();
        public PMB00300RecalcDTO Entity = new PMB00300RecalcDTO();
        
        public List<PMB00300PropertyDTO> PropertyList = new List<PMB00300PropertyDTO>();
        public PMB00300PropertyDTO Property = new PMB00300PropertyDTO();
        
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<PMB00300ListDTO<PMB00300PropertyDTO>>(
                        nameof(IPMB00300.PMB00300GetPropertyList));
                PropertyList = loReturn.Data;
                Property.CPROPERTY_ID = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetRecalcList()
        {
            var loEx = new R_Exception();
            try
            {
                
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CPROPERTY_ID, Property.CPROPERTY_ID);
                var loReturn =
                    await _model.GetAsync<PMB00300ListDTO<PMB00300RecalcDTO>>(
                        nameof(IPMB00300.PMB00300GetRecalcListStream));
                GridList = new ObservableCollection<PMB00300RecalcDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}