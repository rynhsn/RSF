using GLT00200COMMON;
using GLT00200COMMON.DTOs.GLT00200;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLT00200MODEL
{
    public class GLT00200Model : R_BusinessObjectServiceClientBase<GLT00200HeaderDTO>, IGLT00200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLT00200";
        private const string DEFAULT_MODULE = "gl";

        public GLT00200Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateImportJournalDTO DownloadTemplateImportJournal()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateImportJournalDTO> DownloadTemplateImportJournalAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateImportJournalDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateImportJournalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.DownloadTemplateImportJournal),
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
        public ImportJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<ImportJournalSaveResultDTO> GetErrorCountAsync(GetErrorCountParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            ImportJournalSaveResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<ImportJournalSaveResultDTO, GetErrorCountParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.GetErrorCount),
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
        public IAsyncEnumerable<ImportJournalErrorDTO> GetImportJournalErrorList()
        {
            throw new NotImplementedException();
        }

        public async Task<ImportJournalErrorResultDTO> GetImportJournalErrorListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            ImportJournalErrorResultDTO loRtn = new ImportJournalErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ImportJournalErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.GetImportJournalErrorList),
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

        public IAsyncEnumerable<GetImportJournalResult> GetSuccessProcessList()
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

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetImportJournalResult>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.GetSuccessProcessList),
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
            InitialProcessDTO loRtn = new InitialProcessDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<InitialProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.InitialProcess),
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
