using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02550;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL.View_Model
{
    public class LookUpUserViewModel : R_ViewModel<GetUserIdNameDTO>
    {
        private GSM02550Model loModel = new GSM02550Model();

        public ObservableCollection<GetUserIdNameDTO> loUserList = new ObservableCollection<GetUserIdNameDTO>();

        public GetUserIdNameResultDTO loUserRtn = null;

        public string SELECTED_PROPERTY_ID = "";


        public async Task GetUserIdNameListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02550_PROPERTY_ID_STREAMING_CONTEXT, SELECTED_PROPERTY_ID);
                loUserRtn = await loModel.GetUserIdNameListStreamAsync();
                loUserList = new ObservableCollection<GetUserIdNameDTO>(loUserRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

    }
}
