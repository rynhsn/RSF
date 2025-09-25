using GLT00500BACK;
using GLT00500COMMON;
using GLT00500COMMON.DTOs;
using GLT00500COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GLT00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00500Controller : ControllerBase, IGLT00500
    {
        private LoggerGLT00500 _logger;
        public GLT00500Controller(ILogger<GLT00500Controller> logger)
        {
            LoggerGLT00500.R_InitializeLogger(logger);
            _logger = LoggerGLT00500.R_GetInstanceLogger();
        }
        /*
                [HttpPost]
                public ImportAdjustmentJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter)
                {
                    R_Exception loException = new R_Exception();
                    ImportAdjustmentJournalSaveResultDTO loRtn = null;
                    GLT00500Cls loCls = new GLT00500Cls();
                    string lcKeyGuid;

                    try
                    {
                        lcKeyGuid = R_Utility.R_GetContext<string>(ContextConstant.UPLOAD_GLT00500_ERROR_COUNT_GUID_CONTEXT);
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
        public IAsyncEnumerable<GetImportAdjustmentJournalResult> GetSuccessProcessList()
        {
            _logger.LogInfo("Start || GetSuccessProcessList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetImportAdjustmentJournalResult> loRtn = null;
            GLT00500Cls loCls = new GLT00500Cls();
            List<GetImportAdjustmentJournalResult> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetSuccessProcessList(Controller)");
                string lcKeyGuid = R_Utility.R_GetStreamingContext<string>(ContextConstant.UPLOAD_GLT00500_ERROR_GUID_STREAMING_CONTEXT);

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
        private async IAsyncEnumerable<GetImportAdjustmentJournalResult> GetSuccessProcessStream(List<GetImportAdjustmentJournalResult> poParameter)
        {
            foreach (GetImportAdjustmentJournalResult item in poParameter)
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
                GLT00500Cls loCls = new GLT00500Cls();

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
        public IAsyncEnumerable<ImportAdjustmentJournalErrorDTO> GetImportAdjustmentJournalErrorList()
        {
            _logger.LogInfo("Start || GetImportAdjustmentJournalErrorList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<ImportAdjustmentJournalErrorDTO> loRtn = null;
            ImportAdjustmentJournalErrorParameterDTO loParam = new ImportAdjustmentJournalErrorParameterDTO();
            GLT00500Cls loCls = new GLT00500Cls();
            List<ImportAdjustmentJournalErrorDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetImportAdjustmentJournalErrorList(Controller)");
                loParam.CPROCESS_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GLT00500_PROCESS_ID_STREAMING_CONTEXT);
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetImportAdjustmentJournalErrorList(Cls) || GetImportAdjustmentJournalErrorList(Controller)");
                loTempRtn = loCls.GetImportAdjustmentJournalErrorList(loParam);

                _logger.LogInfo("Run GetImportAdjustmentJournalErrorStream(Controller) || GetImportAdjustmentJournalErrorList(Controller)");
                loRtn = GetImportAdjustmentJournalErrorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetImportAdjustmentJournalErrorList(Controller)");

            return loRtn;
        }
        private async IAsyncEnumerable<ImportAdjustmentJournalErrorDTO> GetImportAdjustmentJournalErrorStream(List<ImportAdjustmentJournalErrorDTO> poParameter)
        {
            foreach (ImportAdjustmentJournalErrorDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateImportAdjustmentJournalDTO DownloadTemplateImportAdjustmentJournal()
        {
            _logger.LogInfo("Start || DownloadTemplateImportAdjustmentJournal(Controller)");
            R_Exception loEx = new R_Exception();
            TemplateImportAdjustmentJournalDTO loRtn = new TemplateImportAdjustmentJournalDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateImportAdjustmentJournal(Controller)");
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
            _logger.LogInfo("End || DownloadTemplateImportAdjustmentJournal(Controller)");

            return loRtn;
        }
    }
}
