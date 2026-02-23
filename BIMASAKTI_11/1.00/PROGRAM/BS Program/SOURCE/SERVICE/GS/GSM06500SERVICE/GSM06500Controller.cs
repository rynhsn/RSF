using GSM06500Back;
using GSM06500Common;
using GSM06500Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace GSM06500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM06500Controller : ControllerBase, IGSM06500
    {
        private LoggerGSM06500 _loggerGSM06500;
        private readonly ActivitySource _activitySource;
        public GSM06500Controller(ILogger<GSM06500Controller> logger)
        {
            //Initial and Get Logger
            LoggerGSM06500.R_InitializeLogger(logger);
            _loggerGSM06500 = LoggerGSM06500.R_GetInstanceLogger();
            _activitySource = GSM06500Activity.R_InitializeAndGetActivitySource(nameof(GSM06500Controller));
            // _activitySource = GSM06500Activity.R_InitializeAndGetActivitySource("GSM06500Controller");

        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM06500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceDelete));
            _loggerGSM06500.LogInfo("START process method R_ServiceDelete on Controller");

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            GSM06500Cls loCls;
            try
            {
                loCls = new GSM06500Cls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerGSM06500.LogInfo("Call method R_Delete");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM06500.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();

            _loggerGSM06500.LogInfo("END process method R_ServiceDelete on Controller");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM06500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM06500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
            _loggerGSM06500.LogInfo("START process method R_ServiceGetRecord on Controller");
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM06500DTO>();

            try
            {
                var loCls = new GSM06500Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerGSM06500.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGSM06500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerGSM06500.LogInfo("END process method R_ServiceGetRecord on Controller");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM06500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM06500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceSave));
            _loggerGSM06500.LogInfo("START process method R_ServiceSave on Controller");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM06500DTO> loRtn = null;
            GSM06500Cls loCls;

            try
            {
                loCls = new GSM06500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM06500DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerGSM06500.LogInfo("Call method R_ServiceSave");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM06500.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGSM06500.LogInfo("END process method R_ServiceSave on Controller");
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM06500DTO> GetTermOfPaymentList()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetTermOfPaymentList));
            _loggerGSM06500.LogInfo("START process method GetTermOfPaymentList on Controller");
            var loEx = new R_Exception();
            GSM06500DBParameter loDbParameter;
            IAsyncEnumerable<GSM06500DTO> loRtn = null;
            List<GSM06500DTO> loRtnTemp;

            try
            {
                loDbParameter = new GSM06500DBParameter();
                var loCls = new GSM06500Cls();
                _loggerGSM06500.LogInfo("Set Parameter GetTermOfPaymentList on Controller");
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; ;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _loggerGSM06500.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                _loggerGSM06500.LogInfo("Call method TERM_OF_LIST");
                loRtnTemp = loCls.TERM_OF_LIST(loDbParameter);
                loRtn = GetPaymentofTerm(loRtnTemp);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGSM06500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerGSM06500.LogInfo("End process method GetallTermOfpaymentList on Controller");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM06500DTO> GetPaymentofTerm(List<GSM06500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

    }
}