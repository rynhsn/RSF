using GLT00500COMMON.DTOs;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLT00500MODEL.ViewModel;

namespace GLT00500FRONT
{
    public partial class ImportAdjustmentJournalError : R_Page
    {
        private ImportAdjustmentJournalErrorViewModel loImportAdjustmentJournalErrorViewModel = new();

        private R_Conductor _conductorImportAdjustmentJournalErrorRef;

        private R_Grid<ImportAdjustmentJournalErrorDTO> _gridImportAdjustmentJournalErrorRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loImportAdjustmentJournalErrorViewModel.loParam = (OpenViewErrorLogsParameterDTO)poParameter;
                await _gridImportAdjustmentJournalErrorRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetImportAdjustmentJournalListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loImportAdjustmentJournalErrorViewModel.GetImportAdjustmentJournalErrorListStreamAsync();
                eventArgs.ListEntityResult = loImportAdjustmentJournalErrorViewModel.loErrorList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnClickClose()
        {
            await this.Close(true, false);
        }
    }
}