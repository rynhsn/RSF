using BlazorClientHelper;
using GLF00100COMMON;
using GLF00100Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLF00100FRONT
{
    public partial class GLF00100 : R_Page
    {
        private GLF00100ViewModel _viewModel = new GLF00100ViewModel(); 
        private R_Conductor _conductorRef;

        private R_Grid<GLF00101DTO> _gridRef;

        [Inject] IClientHelper clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Parameter
                var loData = (GLF00100ParameterDTO)poParameter;

                //Start
                await _viewModel.GetCompanyInfo();
                await _conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task JournalDetail_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetJournalDetail((GLF00100ParameterDTO)eventArgs.Data);

                eventArgs.Result = _viewModel.JrnlDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task JournalDetail_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (GLF00100DTO)eventArgs.Data;

                var loParam = new GLF00101DTO { CJRN_ID = loTempParam.CREC_ID };

                await _gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task JournalDetail_List_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetJournalDetailList((GLF00101DTO)eventArgs.Parameter);

                eventArgs.ListEntityResult = _viewModel.JrnlDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GLF00101DTO)eventArgs.Data;

                if (loData.NDEBIT > 0 && loData.NCREDIT == 0)
                {
                    loData.CDBCR = "D";
                }else if (loData.NDEBIT == 0 && loData.NCREDIT > 0)
                {
                    loData.CDBCR = "C";
                }
                else
                {
                    loData.CDBCR = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
