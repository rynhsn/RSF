using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02600BACK.OpenTelemetry;
using PMT02600BACK;
using PMT02600COMMON.DTOs.PMT02650;
using PMT02600COMMON.Loggers;
using PMT02600COMMON;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs;

namespace PMT02600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT02650Controller : ControllerBase, IPMT02650
    {
        private LoggerPMT02650 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02650Controller(ILogger<LoggerPMT02650> logger)
        {
            //Initial and Get Logger
            LoggerPMT02650.R_InitializeLogger(logger);
            _Logger = LoggerPMT02650.R_GetInstanceLogger();
            _activitySource = PMT02650ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02650Controller));
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion

        [HttpPost]
        public IAsyncEnumerable<PMT02650DTO> GetAgreementInvoicePlanStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementInvoicePlanStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02650DTO> loRtn = null;
            PMT02650ParameterDTO loEntity = null;
            _Logger.LogInfo("Start GetAgreementInvoicePlanStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementInvoicePlanStream");
                //PMT02650ParameterDTO loEntity = new PMT02650DTO();
                loEntity = R_Utility.R_GetStreamingContext<PMT02650ParameterDTO>(ContextConstant.PMT02650_AGREEMENT_INVOICE_PLAN_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreementInvoicePlan");
                var loCls = new PMT02650Cls();
                var loTempRtn = loCls.GetAllAgreementInvoicePlan(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementInvoicePlanStream");
                loRtn = GetStreamData<PMT02650DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementInvoicePlanStream");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02650ChargeDTO> GetAgreementChargeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAgreementChargeStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT02650ChargeDTO> loRtn = null;
            PMT02650ParameterDTO loEntity = null;
            _Logger.LogInfo("Start GetAgreementChargeStream");

            try
            {
                _Logger.LogInfo("Set Param GetAgreementChargeStream");
                //PMT02650ChargeDTO loEntity = new PMT02650ChargeDTO();
                loEntity = R_Utility.R_GetStreamingContext<PMT02650ParameterDTO>(ContextConstant.PMT02650_AGREEMENT_CHARGE_STREAMING_CONTEXT);

                _Logger.LogInfo("Call Back Method GetAllAgreementCharge");
                var loCls = new PMT02650Cls();
                var loTempRtn = loCls.GetAllAgreementCharge(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAgreementChargeStream");
                loRtn = GetStreamData<PMT02650ChargeDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAgreementChargeStream");

            return loRtn;
        }

    }
}