using GLT00500COMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GLT00500COMMON;

namespace GLT00500MODEL.ViewModel
{
    public class ImportAdjustmentJournalErrorViewModel : R_ViewModel<ImportAdjustmentJournalErrorDTO>
    {
        private GLT00500Model loModel = new GLT00500Model();

        public ObservableCollection<ImportAdjustmentJournalErrorDTO> loErrorList = new ObservableCollection<ImportAdjustmentJournalErrorDTO>();

        public OpenViewErrorLogsParameterDTO loParam = new OpenViewErrorLogsParameterDTO();

        public async Task GetImportAdjustmentJournalErrorListStreamAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GLT00500_PROCESS_ID_STREAMING_CONTEXT, loParam.CPROCESS_ID);

                ImportAdjustmentJournalErrorResultDTO loResult = await loModel.GetImportAdjustmentJournalErrorListStreamAsync();

                loErrorList = new ObservableCollection<ImportAdjustmentJournalErrorDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
