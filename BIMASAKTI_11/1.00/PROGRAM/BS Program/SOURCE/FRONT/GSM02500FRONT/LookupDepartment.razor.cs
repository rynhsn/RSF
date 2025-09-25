using GSM02500COMMON.DTOs.GSM02560;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500MODEL.View_Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;

namespace GSM02500FRONT
{
    public partial class LookupDepartment : R_Page
    {
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private LookupDepartmentViewModel loDepartmentViewModel = new LookupDepartmentViewModel();
        private R_Grid<GetDepartmentLookupListDTO> _gridDepartmentRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loDepartmentViewModel.SELECTED_PROPERTY_ID = (string)poParameter;
                await _gridDepartmentRef.R_RefreshGrid(null);
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
                await loDepartmentViewModel.GetDepartmentIdNameListStreamAsync();

                eventArgs.ListEntityResult = loDepartmentViewModel.loDepartmentList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = _gridDepartmentRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}
