using PMM01500BACK;
using PMM01500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM01510Controller : ControllerBase, IPMM01510
    {
        private LoggerPMM01510 _Logger;
        private readonly ActivitySource _activitySource;
        public PMM01510Controller(ILogger<LoggerPMM01510> logger)
        {
            //Initial and Get Logger
            LoggerPMM01510.R_InitializeLogger(logger);
            _Logger = LoggerPMM01510.R_GetInstanceLogger();
            _activitySource = PMM01510ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01510Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01510DTO> PMM01510TemplateAndBankAccountList()
        {
            using Activity activity = _activitySource.StartActivity("PMM01510TemplateAndBankAccountList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01510DTO> loRtn = null;
            List<PMM01510DTO> loTempRtn = null;
            var loParameter = new PMM01510DTO();
            _Logger.LogInfo("Start PMM01510TemplateAndBankAccountList");

            try
            {
                var loCls = new PMM01510Cls();
                loTempRtn = new List<PMM01510DTO>();

                _Logger.LogInfo("Set Param PMM01510TemplateAndBankAccountList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);

                _Logger.LogInfo("Call Back Method GetAllTemplateAndBankAccount");
                loTempRtn = loCls.GetAllTemplateAndBankAccount(loParameter);

                _Logger.LogInfo("Call Stream Method Data PMM01510TemplateAndBankAccountList");
                loRtn = GetTemplateAndBankAccountListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End PMM01510TemplateAndBankAccountList");

            return loRtn;
        }
        private async IAsyncEnumerable<PMM01510DTO> GetTemplateAndBankAccountListStream(List<PMM01510DTO> poParameter)
        {
            foreach (PMM01510DTO item in poParameter)
            {
                yield return item;
            }
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01511DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete PMM01510");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete PMM01510");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01510Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMM01510Cls");

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete PMM01510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01511DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM01511DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMM01511DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01511DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMM01510");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord PMM01510");

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01510Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMM01510Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMM01510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01511DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01511DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMM01511DTO> loRtn = new R_ServiceSaveResultDTO<PMM01511DTO>();
            _Logger.LogInfo("Start ServiceSave PMM01510");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave PMM01510");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01510Cls();

                _Logger.LogInfo("Call Back Method R_Save PMM01510Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMM01510");

            return loRtn;
        }
    }
}
