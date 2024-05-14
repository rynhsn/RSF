using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs;
using GSM02500MODEL.View_Model;
using GSM02500COMMON.DTOs.GSM02550;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;

namespace GSM02500FRONT
{
    public partial class LookUpUser : R_Page
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private LookUpUserViewModel loUserViewModel = new LookUpUserViewModel();
        private R_Grid<GetUserIdNameDTO> _gridUserRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loUserViewModel.SELECTED_PROPERTY_ID = (string)poParameter;
                await _gridUserRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUserViewModel.GetUserIdNameListStreamAsync();

                eventArgs.ListEntityResult = loUserViewModel.loUserList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = _gridUserRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}