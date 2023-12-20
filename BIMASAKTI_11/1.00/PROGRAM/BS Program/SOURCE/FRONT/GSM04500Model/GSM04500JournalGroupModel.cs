using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM04500Model
{
    public class GSM04500JournalGroupModel : R_BusinessObjectServiceClientBase<GSM04500JournalGroupDTO>,
        IGSM04500JournalGroup
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM04500JournalGroup";
        private const string DEFAULT_MODULE = "gs";

        public GSM04500JournalGroupModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region not implemented
        public IAsyncEnumerable<GSM04500JournalGroupDTO> GSM04500GetAllJournalGroupListStream()
        {
            throw new NotImplementedException();
        }

        public GSM04500JournalGroupExcelDTO GSM04500DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        #endregion
        
        
        public async Task<List<GSM04500JournalGroupDTO>> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM04500JournalGroupDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500JournalGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500JournalGroup.GSM04500GetAllJournalGroupListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public async Task<GSM04500JournalGroupExcelDTO> GSM04500DownloadTemplateFileModel()
        {
            var loEx = new R_Exception();
            GSM04500JournalGroupExcelDTO loResult = new GSM04500JournalGroupExcelDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04500JournalGroupExcelDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500JournalGroup.GSM04500DownloadTemplateFile),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            
            return loResult;
        }
    }
}