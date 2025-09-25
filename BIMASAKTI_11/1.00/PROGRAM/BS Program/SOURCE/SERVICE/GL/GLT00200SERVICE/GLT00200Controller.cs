using GLT00200BACK;
using GLT00200COMMON;
using GLT00200COMMON.DTOs.GLT00200;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public ImportJournalSaveResultDTO GetErrorCount(GetErrorCountParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            ImportJournalSaveResultDTO loRtn = null;
            GLT00200Cls loCls = new GLT00200Cls();

            try
            {
                string lcKeyGuid = R_Utility.R_GetContext<string>(ContextConstant.UPLOAD_GLT00200_ERROR_COUNT_GUID_CONTEXT);
                loRtn = loCls.GetErrorCount(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetImportJournalResult> GetSuccessProcessList()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetImportJournalResult> loRtn = null;
            GLT00200ValidateUploadCls loCls = new GLT00200ValidateUploadCls();
            List<GetImportJournalResult> loTempRtn = null;

            try
            {
                string lcKeyGuid = R_Utility.R_GetStreamingContext<string>(ContextConstant.UPLOAD_GLT00200_ERROR_GUID_STREAMING_CONTEXT);

                loTempRtn = loCls.GetSuccessProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

                loRtn = GetSuccessProcessStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

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
        public IAsyncEnumerable<GLT00200DetailDTO> ImportJournal()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GLT00200DetailDTO> loRtn = null;
            ImportJournalParameterDTO loParam = new ImportJournalParameterDTO();
            GLT00200Cls loCls = new GLT00200Cls();
            List<GLT00200DetailDTO> loTempRtn = null;

            try
            {
                loParam = R_Utility.R_GetStreamingContext<ImportJournalParameterDTO>(ContextConstant.GLT00200_IMPORT_JOURNAL_STREAMING_CONTEXT);
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                loTempRtn = loCls.ImportJournal(loParam);
                
                loRtn = GetContractorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GLT00200DetailDTO> GetContractorStream(List<GLT00200DetailDTO> poParameter)
        {
            foreach (GLT00200DetailDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public InitialProcessDTO InitialProcess()
        {
            var loEx = new R_Exception();
            
            GetCompanyDTO loCompany = new GetCompanyDTO();
            GetSystemParamDTO loSystemParam = new GetSystemParamDTO();
            List<GetDeptLookUpListDTO> loDeptLookUpList = new List<GetDeptLookUpListDTO>();
            GetSoftPeriodStartDateDTO loSoftPeriodStartData = new GetSoftPeriodStartDateDTO();
            GetUndoCommitJrnDTO loUndoCommitJrn = new GetUndoCommitJrnDTO();
            GetTransactionCodeDTO loTransactionCode = new GetTransactionCodeDTO();
            GetPeriodDTO loPeriod = new GetPeriodDTO();
            GetCurrentPeriodStartDateDTO loCurrentPeriodStartDate = new GetCurrentPeriodStartDateDTO();

            InitialProcessDTO loRtn = new InitialProcessDTO();
            InitialProcessParameterDTO loParam = new InitialProcessParameterDTO();

            try
            {
                GLT00200Cls loCls = new GLT00200Cls();

                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                loCompany = loCls.GetCompany(loParam);
                loSystemParam = loCls.GetSystemParam(loParam);

                loParam.CCYEAR = loSystemParam.CSOFT_PERIOD_YY;
                loParam.CPERIOD_NO = loSystemParam.CSOFT_PERIOD_MM;
                loParam.CCURRENT_PERIOD_YY = loSystemParam.CCURRENT_PERIOD_YY;
                loParam.CCURRENT_PERIOD_MM = loSystemParam.CCURRENT_PERIOD_MM;

                loCurrentPeriodStartDate = loCls.GetCurrentPeriodStartDate(loParam);
                loDeptLookUpList = loCls.GetDeptLookUpList(loParam);
                loSoftPeriodStartData = loCls.GetSoftPeriodStartDate(loParam);
                loUndoCommitJrn = loCls.GetUndoCommitJrn(loParam);
                loTransactionCode = loCls.GetTransactionCode(loParam);
                loPeriod = loCls.GetPeriod(loParam);

                loRtn.CompanyData = loCompany;
                loRtn.SystemParamData = loSystemParam;
                loRtn.CurrentPeriodStartDateData = loCurrentPeriodStartDate;
                loRtn.DeptLookUpListData = loDeptLookUpList;
                loRtn.SoftPeriodStartDateData = loSoftPeriodStartData;
                loRtn.UndoCommitJrnData = loUndoCommitJrn;
                loRtn.TransactionCodeData = loTransactionCode;
                loRtn.PeriodData = loPeriod;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public ImportJournalSaveResultDTO SaveImportJournal()
        {
            var loEx = new R_Exception();
            ImportJournalSaveResultDTO loRtn = new ImportJournalSaveResultDTO();
            ImportJournalSaveParameterDTO loParam = new ImportJournalSaveParameterDTO();

            try
            {
                GLT00200Cls loCls = new GLT00200Cls();

                loParam = R_Utility.R_GetContext<ImportJournalSaveParameterDTO>(ContextConstant.GLT00200_SAVE_IMPORT_JOURNAL_CONTEXT);
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn = loCls.SaveImportJournal(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<ImportJournalErrorDTO> GetImportJournalErrorList()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<ImportJournalErrorDTO> loRtn = null;
            ImportJournalErrorParameterDTO loParam = new ImportJournalErrorParameterDTO();
            GLT00200Cls loCls = new GLT00200Cls();
            List<ImportJournalErrorDTO> loTempRtn = null;

            try
            {
                loParam.CPROCESS_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GLT00200_PROCESS_ID_STREAMING_CONTEXT);
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                loTempRtn = loCls.GetImportJournalErrorList(loParam);

                loRtn = GetImportJournalErrorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

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
            var loEx = new R_Exception();
            var loRtn = new TemplateImportJournalDTO();

            try
            {
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

            return loRtn;
        }
    }
}
