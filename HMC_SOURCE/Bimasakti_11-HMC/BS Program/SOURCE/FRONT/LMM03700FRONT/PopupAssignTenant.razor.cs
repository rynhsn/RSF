using LMM03700Common.DTO_s;
using LMM03700Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Xml.Linq;

namespace LMM03700Front
{
    public partial class PopupAssignTenant : R_Page
    {
        private R_ConductorGrid _conTenantToAssignRef;
        private R_Grid<SelectedTenantGridPopupDTO> _Grid;

        private LMM03710ViewModel _viewModelTC= new  LMM03710ViewModel();
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _Grid.R_RefreshGrid(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantGridPopupDTO>(eventArgs.Parameter);
                await _viewModelTC.GetTenantToAssignList(loParam);
                eventArgs.ListEntityResult = _viewModelTC.TenantToAssignList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #region Save Batch
        private void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            //var loData = (List<UserDTO>)events.Data;

            //events.Cancel = loData.Count == 0;
        }
        private async Task R_ServiceSaveBatchAsync(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = eventArgs.Data;
                await this.Close(true, loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {

        }
        #endregion
        public async Task Button_OnClickOkAsync()
        {
            await _conTenantToAssignRef.R_SaveBatch();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
