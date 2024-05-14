using GSM02500COMMON.DTOs.GSM02560;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs;

namespace GSM02500MODEL.View_Model
{
    public class LookupDepartmentViewModel : R_ViewModel<GetDepartmentLookupListDTO>
    {
        private GSM02560Model loModel = new GSM02560Model();

        public ObservableCollection<GetDepartmentLookupListDTO> loDepartmentList = new ObservableCollection<GetDepartmentLookupListDTO>();

        public GetDepartmentLookupListResultDTO loDepartmentRtn = null;

        public string SELECTED_PROPERTY_ID = "";


        public async Task GetDepartmentIdNameListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02560_PROPERTY_ID_STREAMING_CONTEXT, SELECTED_PROPERTY_ID);
                loDepartmentRtn = await loModel.GetDepartmentLookupListStreamAsync();
                loDepartmentList = new ObservableCollection<GetDepartmentLookupListDTO>(loDepartmentRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
