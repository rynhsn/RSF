using GLT00200BACK;
using GLT00200COMMON;
using GLT00200COMMON.DTOs.GLT00200;
using GLT00200COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GLT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00200Controller : ControllerBase, IGLT00200
    {
        private LoggerGLT00200 _logger;
        public GLT00200Controller(ILogger<GLT00200Controller> logger)
        {
            LoggerGLT00200.R_InitializeLogger(logger);
            _logger = LoggerGLT00200.R_GetInstanceLogger();
        }
/*
        [HttpPost]
        public ImportJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            ImportJournalSaveResultDTO loRtn = null;
            GLT00200Cls loCls = new GLT00200Cls();
            string lcKeyGuid;

            try
            {
                lcKeyGuid = R_Utility.R_GetContext<string>(ContextConstant.UPLOAD_GLT00200_ERROR_COUNT_GUID_CONTEXT);
                loRtn = loCls.GetErrorCount(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
*/
        [HttpPost]
        public IAsyncEnumerable<GetImportJournalResult> GetSuccessProcessList()
        {
            _logger.LogInfo("Start || GetSuccessProcessList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetImportJournalResult> loRtn = null;
            GLT00200Cls loCls = new GLT00200Cls();
            List<GetImportJournalResult> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetSuccessProcessList(Controller)");
                string lcKeyGuid = R_Utility.R_GetStreamingContext<string>(ContextConstant.UPLOAD_GLT00200_ERROR_GUID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetSuccessProcess(Cls) || GetSuccessProcessList(Controller)");
                loTempRtn = loCls.GetSuccessProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

                _logger.LogInfo("Run GetSuccessProcessStream(Controller) || GetSuccessProcessList(Controller)");
                loRtn = GetSuccessProcessStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSuccessProcessList(Controller)");

            return loRtn;
        }
        private async IAsyncEnumerable<GetImportJournalResult> GetSuccessProcessStream(List<GetImportJournalResult> poParameter)
        {
            foreach (GetImportJournalResult item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public InitialProcessDTO InitialProcess()
        {
            _logger.LogInfo("Start || InitialProcess(Controller)");
            R_Exception loEx = new R_Exception();
            /*
            GetCompanyDTO loCompany = new GetCompanyDTO();
            GetSystemParamDTO loSystemParam = new GetSystemParamDTO();
            List<GetDeptLookUpListDTO> loDeptLookUpList = new List<GetDeptLookUpListDTO>();
            GetSoftPeriodStartDateDTO loSoftPeriodStartData = new GetSoftPeriodStartDateDTO();
            GetUndoCommitJrnDTO loUndoCommitJrn = new GetUndoCommitJrnDTO();
            GetTransactionCodeDTO loTransactionCode = new GetTransactionCodeDTO();
            GetPeriodDTO loPeriod = new GetPeriodDTO();
            GetCurrentPeriodStartDateDTO loCurrentPeriodStartDate = new GetCurrentPeriodStartDateDTO();
*/
            InitialProcessDTO loRtn = new InitialProcessDTO();
            InitialProcessParameterDTO loParam = new InitialProcessParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || InitialProcess(Controller)");
                GLT00200Cls loCls = new GLT00200Cls();

                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetCompany(Cls) || InitialProcess(Controller)");
                loRtn.CompanyData = loCls.GetCompany(loParam);

                _logger.LogInfo("Run GetSystemParam(Cls) || InitialProcess(Controller)");
                loRtn.SystemParamData = loCls.GetSystemParam(loParam);

                _logger.LogInfo("Set Parameter Based On GetSystemParam(Cls) Result|| InitialProcess(Controller)");
                loParam.CCYEAR = loRtn.SystemParamData.CSOFT_PERIOD_YY;
                loParam.CPERIOD_NO = loRtn.SystemParamData.CSOFT_PERIOD_MM;
                loParam.CCURRENT_PERIOD_YY = loRtn.SystemParamData.CCURRENT_PERIOD_YY;
                loParam.CCURRENT_PERIOD_MM = loRtn.SystemParamData.CCURRENT_PERIOD_MM;

                _logger.LogInfo("Run GetCurrentPeriodStartDate(Cls) || InitialProcess(Controller)");
                loRtn.CurrentPeriodStartDateData = loCls.GetCurrentPeriodStartDate(loParam);

                _logger.LogInfo("Run GetDeptLookUpList(Cls) || InitialProcess(Controller)");
                loRtn.DeptLookUpListData = loCls.GetDeptLookUpList(loParam);

                _logger.LogInfo("Run GetSoftPeriodStartDate(Cls) || InitialProcess(Controller)");
                loRtn.SoftPeriodStartDateData = loCls.GetSoftPeriodStartDate(loParam);

                _logger.LogInfo("Run GetUndoCommitJrn(Cls) || InitialProcess(Controller)");
                loRtn.UndoCommitJrnData = loCls.GetUndoCommitJrn(loParam);

                _logger.LogInfo("Run GetTransactionCode(Cls) || InitialProcess(Controller)");
                loRtn.TransactionCodeData = loCls.GetTransactionCode(loParam);


                _logger.LogInfo("Run GetPeriod(Cls) || InitialProcess(Controller)");
                loRtn.PeriodData = loCls.GetPeriod(loParam);
/*
                loRtn.CompanyData = loCompany;
                loRtn.SystemParamData = loSystemParam;
                loRtn.CurrentPeriodStartDateData = loCurrentPeriodStartDate;
                loRtn.DeptLookUpListData = loDeptLookUpList;
                loRtn.SoftPeriodStartDateData = loSoftPeriodStartData;
                loRtn.UndoCommitJrnData = loUndoCommitJrn;
                loRtn.TransactionCodeData = loTransactionCode;
                loRtn.PeriodData = loPeriod;*/
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || InitialProcess(Controller)");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<ImportJournalErrorDTO> GetImportJournalErrorList()
        {
            _logger.LogInfo("Start || GetImportJournalErrorList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<ImportJournalErrorDTO> loRtn = null;
            ImportJournalErrorParameterDTO loParam = new ImportJournalErrorParameterDTO();
            GLT00200Cls loCls = new GLT00200Cls();
            List<ImportJournalErrorDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetImportJournalErrorList(Controller)");
                loParam.CPROCESS_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GLT00200_PROCESS_ID_STREAMING_CONTEXT);
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetImportJournalErrorList(Cls) || GetImportJournalErrorList(Controller)");
                loTempRtn = loCls.GetImportJournalErrorList(loParam);

                _logger.LogInfo("Run GetImportJournalErrorStream(Controller) || GetImportJournalErrorList(Controller)");
                loRtn = GetImportJournalErrorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetImportJournalErrorList(Controller)");

            return loRtn;
        }
        private async IAsyncEnumerable<ImportJournalErrorDTO> GetImportJournalErrorStream(List<ImportJournalErrorDTO> poParameter)
        {
            foreach (ImportJournalErrorDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateImportJournalDTO DownloadTemplateImportJournal()
        {
            _logger.LogInfo("Start || DownloadTemplateImportJournal(Controller)");
            R_Exception loEx = new R_Exception();
            TemplateImportJournalDTO loRtn = new TemplateImportJournalDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateImportJournal(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GL_API");
                var lcResourceFile = "BIMASAKTI_GL_API.Template.GL_JOURNAL_UPLOAD_TEMPLATE.zip";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || DownloadTemplateImportJournal(Controller)");

            return loRtn;
        }
    }
}
