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
    public class LMM01510Controller : ControllerBase, ILMM01510
    {
        private LoggerLMM01510 _Logger;
        public LMM01510Controller(ILogger<LoggerLMM01510> logger)
        {
            //Initial and Get Logger
            LoggerLMM01510.R_InitializeLogger(logger);
            _Logger = LoggerLMM01510.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01510DTO> LMM01510TemplateAndBankAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01510DTO> loRtn = null;
            List<LMM01510DTO> loTempRtn = null;
            var loParameter = new LMM01510DTO();
            _Logger.LogInfo("Start LMM01510TemplateAndBankAccountList");

            try
            {
                var loCls = new LMM01510Cls();
                loTempRtn = new List<LMM01510DTO>();

                _Logger.LogInfo("Set Param LMM01510TemplateAndBankAccountList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);

                _Logger.LogInfo("Call Back Method GetAllTemplateAndBankAccount");
                loTempRtn = loCls.GetAllTemplateAndBankAccount(loParameter);

                _Logger.LogInfo("Call Stream Method Data LMM01510TemplateAndBankAccountList");
                loRtn = GetTemplateAndBankAccountListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End LMM01510TemplateAndBankAccountList");

            return loRtn;
        }
        private async IAsyncEnumerable<LMM01510DTO> GetTemplateAndBankAccountListStream(List<LMM01510DTO> poParameter)
        {
            foreach (LMM01510DTO item in poParameter)
            {
                yield return item;
            }
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete LMM01510");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete LMM01510");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01510Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM01510Cls");

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM01510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01511DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01511DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01511DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01510");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01510");

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01510Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01510Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01511DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01511DTO> loRtn = new R_ServiceSaveResultDTO<LMM01511DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01510");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01510");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01510Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01510Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01510");

            return loRtn;
        }
    }
}
