using LMM01500BACK;
using LMM01500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01500Controller : ControllerBase, ILMM01500
    {
        private LoggerLMM01500 _Logger;
        public LMM01500Controller(ILogger<LoggerLMM01500> logger)
        {
            //Initial and Get Logger
            LoggerLMM01500.R_InitializeLogger(logger);
            _Logger = LoggerLMM01500.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01501DTO> GetInvoiceGrpList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01501DTO> loRtn = null;
            List<LMM01501DTO> loTempRtn = null;
            var loParameter = new LMM01501ParameterDTO();
            _Logger.LogInfo("Start GetInvoiceGrpList");

            try
            {
                var loCls = new LMM01500Cls();
                loTempRtn = new List<LMM01501DTO>();

                _Logger.LogInfo("Set Param GetInvoiceGrpList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetAllInvoiceGrp");
                loTempRtn = loCls.GetAllInvoiceGrp(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetInvoiceGrpList");
                loRtn = GetInvoiceGrpListStream<LMM01501DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInvoiceGrpList");

            return loRtn;
        }
        private async IAsyncEnumerable<T> GetInvoiceGrpListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01500DTOPropety> GetProperty()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01500DTOPropety> loRtn = null;
            var loParameter = new LMM01500PropertyParameterDTO();
            _Logger.LogInfo("Start GetProperty");

            try
            {
                var loCls = new LMM01500Cls();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetProperty");
                loRtn = GetInvoiceGrpListStream<LMM01500DTOPropety>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetProperty");

            return loRtn;
        }

        [HttpPost]
        public LMM01500DTO LMM01500ActiveInactive(LMM01500DTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM01500DTO loRtn = new LMM01500DTO();
            LMM01500Cls loCls = new LMM01500Cls();
            _Logger.LogInfo("Start LMM01500ActiveInactive");

            try
            {
                _Logger.LogInfo("Set Param LMM01500ActiveInactive");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method LMM01500ActiveInactive");
                loCls.LMM01500ActiveInactiveSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End LMM01500ActiveInactive");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01502DTO> LMM01500LookupBank()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01502DTO> loRtn = null;
            var loParameter = new LMM01502DTO();
            _Logger.LogInfo("Start LMM01500LookupBank");

            try
            {
                var loCls = new LMM01500Cls();

                _Logger.LogInfo("Set Param LMM01500LookupBank");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _Logger.LogInfo("Call Back Method LMM01530LookupBank");
                var loResult = loCls.LMM01530LookupBank(loParameter);

                _Logger.LogInfo("Call Stream Method Data LMM01500LookupBank");
                loRtn = GetInvoiceGrpListStream<LMM01502DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End LMM01500LookupBank");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete LMM01500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete LMM01500");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01500Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM01500Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM01500");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01500DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01500DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01500");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01500Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01500Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01500");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01500DTO> loRtn = new R_ServiceSaveResultDTO<LMM01500DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01500");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01500Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01500Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01500");

            return loRtn;
        }
    }
}
