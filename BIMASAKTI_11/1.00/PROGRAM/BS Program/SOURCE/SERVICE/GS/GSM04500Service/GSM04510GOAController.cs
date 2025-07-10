using GSM04500Back;
using GSM04500Common;
using GSM04500Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM04500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM04510GOAController : ControllerBase, IGSM04510GOA
    {
        private LoggerGSM04500 _loggerGSM04500;
        private readonly ActivitySource _activitySource;
        public GSM04510GOAController(ILogger<GSM04510GOAController> logger)
        {
            LoggerGSM04500.R_InitializeLogger(logger);
            _loggerGSM04500 = LoggerGSM04500.R_GetInstanceLogger();
            _activitySource = GSM04500Activity.R_InitializeAndGetActivitySource(nameof(GSM04510GOAController));
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04510GOADTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM04510GOADTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04510GOADTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM04510GOADTO>();

            try
            {
                var loCls = new GSM04510GOACls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerGSM04500.LogInfo("Call method R_GetRecord on Controller");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGSM04500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM04510GOADTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04510GOADTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM04510GOADTO> loRtn = null;
            GSM04510GOACls loCls;

            try
            {
                loCls = new GSM04510GOACls();
                loRtn = new R_ServiceSaveResultDTO<GSM04510GOADTO>();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerGSM04500.LogInfo("Call method R_Save on Controller");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM04510GOADTO> JOURNAL_GRP_GOA_LIST()
        {
            string lcMethodName = nameof(JOURNAL_GRP_GOA_LIST);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GSM04510GOADBParameter loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM04510GOADTO> loRtn = null;
            List<GSM04510GOADTO> loRtnTemp;

            try
            {
                loDbParameter = new GSM04510GOADBParameter();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);
                loDbParameter.CJOURNAL_GRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJOURNAL_GRP_CODE);

                _loggerGSM04500.LogInfo("Get Parameter JOURNAL_GRP_GOA_LIST on Controller");
                _loggerGSM04500.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                var loCls = new GSM04510GOACls();
                _loggerGSM04500.LogInfo("Call method JOURNAL_GROUP_GOA_LIST");
                loRtnTemp = loCls.JOURNAL_GROUP_GOA_LIST(loDbParameter);
                _loggerGSM04500.LogInfo("Call method to streaming data");
                loRtn = GET_JOURNAL_GROUP_GOA_LIST(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGSM04500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }

        private async IAsyncEnumerable<GSM04510GOADTO> GET_JOURNAL_GROUP_GOA_LIST(List<GSM04510GOADTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
