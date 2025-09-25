using GSM04500Back;
using GSM04500Common;
using GSM04500Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace GSM04500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM04500Controller : ControllerBase, IGSM04500
    {
        private LoggerGSM04500 _loggerGSM04500;
        private readonly ActivitySource _activitySource;
        public GSM04500Controller(ILogger<GSM04500Controller> logger)
        {
            LoggerGSM04500.R_InitializeLogger(logger);
            _loggerGSM04500 = LoggerGSM04500.R_GetInstanceLogger();
            _activitySource = GSM04500Activity.R_InitializeAndGetActivitySource(nameof(GSM04500Controller));
        }

        [HttpPost]
        public GSM04500PropertyListDTO GetAllPropertyList()
        {
            string lcMethodName = nameof(GetAllPropertyList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GSM04500PropertyListDTO loRtn = null;
            var loParameter = new GSM04500DBParameter();

            try
            {
                var loCls = new GSM04500Cls();
                loRtn = new GSM04500PropertyListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerGSM04500.LogInfo("Call method GetAllPropertyList on Controller");
             //   var loResult = loCls.GetAllPropertyList(loParameter);
              //  loRtn.Data = loResult;
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
        public GSM04500JournalGroupTypeListDTO GetAllJournalGroupTypeList()
        {
            string lcMethodName = nameof(GetAllJournalGroupTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GSM04500JournalGroupTypeListDTO loResult = null;
            var loParameter = new GSM04500DBParameter();

            try
            {
                var loCls = new GSM04500Cls();
                loResult = new GSM04500JournalGroupTypeListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.LANGUAGE = R_BackGlobalVar.CULTURE;
                _loggerGSM04500.LogInfo("Call method GetAllJournalGroupTypeList on Controller");

                var loResultTemp = loCls.GetAllJournalGroupTypeList(loParameter);
                loResult.Data = loResultTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGSM04500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loResult;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500DTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            GSM04500Cls loCls;
            GSM04500DBParameter loDbParameter;

            try
            {
                loCls = new GSM04500Cls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerGSM04500.LogInfo("Call method R_Delete on Controller");

                loCls.R_Delete(poParameter.Entity);
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
        public R_ServiceGetRecordResultDTO<GSM04500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04500DTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM04500DTO>();

            try
            {
                var loCls = new GSM04500Cls();
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
        public R_ServiceSaveResultDTO<GSM04500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04500DTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM04500DTO> loRtn = null;
            GSM04500Cls loCls;

            try
            {
                loCls = new GSM04500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM04500DTO>();

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
        public IAsyncEnumerable<GSM04500DTO> GET_JOURNAL_GRP_LIST_STREAM()
        {
            string lcMethodName = nameof(GET_JOURNAL_GRP_LIST_STREAM);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GSM04500DBParameter loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM04500DTO> loRtn = null;
            List<GSM04500DTO> loRtnTemp;

            try
            {
                loDbParameter = new GSM04500DBParameter();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);

                _loggerGSM04500.LogInfo("Get Parameter GET_JOURNAL_GRP_LIST_STREAM on Controller");
                _loggerGSM04500.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                var loCls = new GSM04500Cls();
                _loggerGSM04500.LogInfo("Call method JOURNAL_GROUP_LIST");
                loRtnTemp = loCls.JOURNAL_GROUP_LIST(loDbParameter);
                _loggerGSM04500.LogInfo("Call method to streaming data");
                loRtn = GET_JOURNAL_GROUP_LIST_SERVICE(loRtnTemp);
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

        private async IAsyncEnumerable<GSM04500DTO> GET_JOURNAL_GROUP_LIST_SERVICE(List<GSM04500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }



    }
}