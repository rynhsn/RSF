using GLT00500COMMON;
using GLT00500COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLT00500MODEL
{
    public class GLT00500Model : R_BusinessObjectServiceClientBase<GLT00500HeaderDTO>, IGLT00500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLT00500";
        private const string DEFAULT_MODULE = "gl";

        public GLT00500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateImportAdjustmentJournalDTO DownloadTemplateImportAdjustmentJournal()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateImportAdjustmentJournalDTO> DownloadTemplateImportAdjustmentJournalAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateImportAdjustmentJournalDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateImportAdjustmentJournalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00500.DownloadTemplateImportAdjustmentJournal),
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
/*
        public ImportAdjustmentJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<ImportAdjustmentJournalSaveResultDTO> GetErrorCountAsync(GetErrorCountParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            ImportAdjustmentJournalSaveResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<ImportAdjustmentJournalSaveResultDTO, GetErrorCountParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00500.GetErrorCount),
                    poParameter,
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
*/
        public IAsyncEnumerable<ImportAdjustmentJournalErrorDTO> GetImportAdjustmentJournalErrorList()
        {
            throw new NotImplementedException();
        }

        public async Task<ImportAdjustmentJournalErrorResultDTO> GetImportAdjustmentJournalErrorListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            ImportAdjustmentJournalErrorResultDTO loRtn = new ImportAdjustmentJournalErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ImportAdjustmentJournalErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00500.GetImportAdjustmentJournalErrorList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public IAsyncEnumerable<GetImportAdjustmentJournalResult> GetSuccessProcessList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetSuccessProcessResultDTO> GetSuccessProcessListStreamingAsync()
        {
            R_Exception loEx = new R_Exception();
            GetSuccessProcessResultDTO loRtn = new GetSuccessProcessResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetImportAdjustmentJournalResult>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00500.GetSuccessProcessList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public InitialProcessDTO InitialProcess()
        {
            throw new NotImplementedException();
        }
        public async Task<InitialProcessDTO> InitialProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            InitialProcessDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<InitialProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00500.InitialProcess),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
