using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLB00200Common;
using GLB00200Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GLB00200Front
{
    public partial class GLB00200Detail : R_Page
    {
        private R_Grid<GLB00200JournalDetailDTO> _gridReversingDetail;
        private R_Conductor _conductorJournalPeriod;

        private R_ConductorGrid _conductorReversingJournalDetail;

        private GLB00200ViewModel _viewModelGLB00200Detail = new();
        private bool IsModalHidden = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var param = (GLB00200DTO)poParameter;
                _viewModelGLB00200Detail.CurrentReversingJournal = param;
                IsModalHidden = false;

                await _gridReversingDetail.R_RefreshGrid(param);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //var tempParam = (GLB00200DTO)eventArgs.Parameter;
                await _viewModelGLB00200Detail.GetDetail_ReversingJournal();
                eventArgs.ListEntityResult = _viewModelGLB00200Detail.DetailReversingJournalList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region CloseButton
        private async Task OnClickClose()
        {
            var loEx = new R_Exception();
            try
            {
                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

    }
}
