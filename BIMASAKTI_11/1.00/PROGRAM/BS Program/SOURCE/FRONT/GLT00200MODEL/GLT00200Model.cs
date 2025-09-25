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
            var loEx = new R_Exception();
            TemplateImportJournalDTO loResult = new TemplateImportJournalDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateImportJournalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.DownloadTemplateImportJournal),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public ImportJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<ImportJournalSaveResultDTO> GetErrorCountAsync(GetErrorCountParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            ImportJournalSaveResultDTO loResult = new ImportJournalSaveResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<ImportJournalSaveResultDTO, GetErrorCountParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.GetErrorCount),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public IAsyncEnumerable<ImportJournalErrorDTO> GetImportJournalErrorList()
        {
            throw new NotImplementedException();
        }

        public async Task<ImportJournalErrorResultDTO> GetImportJournalErrorListStreamAsync()
        {
            var loEx = new R_Exception();
            List<ImportJournalErrorDTO> loResult = null;
            ImportJournalErrorResultDTO loRtn = new ImportJournalErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ImportJournalErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.GetImportJournalErrorList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
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
            var loEx = new R_Exception();
            List<GetImportJournalResult> loResult = null;
            GetSuccessProcessResultDTO loRtn = new GetSuccessProcessResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetImportJournalResult>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.GetSuccessProcessList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public IAsyncEnumerable<GLT00200DetailDTO> ImportJournal()
        {
            throw new NotImplementedException();
        }

        public async Task<GLT00200DetailResultDTO> ImportJournalStreamAsync()
        {
            var loEx = new R_Exception();
            List<GLT00200DetailDTO> loResult = null;
            GLT00200DetailResultDTO loRtn = new GLT00200DetailResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00200DetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.ImportJournal),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
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
            var loEx = new R_Exception();
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

        public ImportJournalSaveResultDTO SaveImportJournal()
        {
            throw new NotImplementedException();
        }
        public async Task<ImportJournalSaveResultDTO> SaveImportJournalAsync()
        {
            var loEx = new R_Exception();
            ImportJournalSaveResultDTO loRtn = new ImportJournalSaveResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<ImportJournalSaveResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00200.SaveImportJournal),
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
