using BlazorClientHelper;
using GLT00200COMMON.DTOs.GLT00200;
using GLT00200MODEL.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLT00200FRONT
{
    public partial class ImportJournalError : R_Page
    {
        private ImportJournalErrorViewModel loImportJournalErrorViewModel = new();

        private R_Conductor _conductorImportJournalErrorRef;

        private R_Grid<ImportJournalErrorDTO> _gridImportJournalErrorRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loImportJournalErrorViewModel.loParam = (OpenViewErrorLogsParameterDTO)poParameter;
                await _gridImportJournalErrorRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetImportJournalListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loImportJournalErrorViewModel.GetImportJournalErrorListStreamAsync();
                eventArgs.ListEntityResult = loImportJournalErrorViewModel.loErrorList;
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