using GLT00200COMMON;
using GLT00200COMMON.DTOs.GLT00200;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GLT00200MODEL.ViewModel
{
    public class ImportJournalErrorViewModel : R_ViewModel<ImportJournalErrorDTO>
    {
        private GLT00200Model loModel = new GLT00200Model();

        public ObservableCollection<ImportJournalErrorDTO> loErrorList = new ObservableCollection<ImportJournalErrorDTO>();

        public OpenViewErrorLogsParameterDTO loParam = new OpenViewErrorLogsParameterDTO();

        public async Task GetImportJournalErrorListStreamAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GLT00200_PROCESS_ID_STREAMING_CONTEXT, loParam.CPROCESS_ID);

                ImportJournalErrorResultDTO loResult = await loModel.GetImportJournalErrorListStreamAsync();

                loErrorList = new ObservableCollection<ImportJournalErrorDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
