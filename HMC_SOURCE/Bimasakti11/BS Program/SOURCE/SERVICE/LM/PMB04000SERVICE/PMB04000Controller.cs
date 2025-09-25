using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB04000BACK;
using PMB04000COMMON.Context;
using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;
using PMB04000COMMON.Interface;
using PMB04000COMMON.Logs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB04000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMB04000Controller : IPMB04000
    {
        private readonly LoggerPMB04000? _logger;
        private readonly ActivitySource _activitySource;
        public PMB04000Controller(ILogger<PMB04000Controller> logger)
        {
            //Initial and Get Logger
            LoggerPMB04000.R_InitializeLogger(logger);
            _logger = LoggerPMB04000.R_GetInstanceLogger();
            _activitySource = PMB04000Activity.R_InitializeAndGetActivitySource(nameof(PMB04000Controller));

        }
        [HttpPost]
        public IAsyncEnumerable<PMB04000DTO> GetInvReceipt_List()
        {
            string lcMethodName = nameof(GetInvReceipt_List);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMB04000DTO> loRtn = null;
            List<PMB04000DTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMB04000ParamDTO();
                var loCls = new PMB04000Cls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CPROPERTY_ID);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CDEPT_CODE);
                loDbParameter.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CTENANT_ID);
                loDbParameter.LALL_TENANT = R_Utility.R_GetStreamingContext<bool>(PMB04000ContextDTO.LALL_TENANT);
                loDbParameter.CINVOICE_TYPE = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CINVOICE_TYPE);
                loDbParameter.CPERIOD = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CPERIOD);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CTRANS_CODE);
                loDbParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetInvoiceReceiptList(loDbParameter); ;

                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public PeriodYearDTO GetPeriodYearRange()
        {
            string lcMethodName = nameof(GetPeriodYearRange);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            PeriodYearDTO? loRtn = null;
            try
            {
                var loDbParameter = new PMB04000ParamDTO();
                var loCls = new PMB04000Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetPeriodYearRange(loDbParameter); 
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public IAsyncEnumerable<TemplateDTO> GetTemplateList()
        {
            string lcMethodName = nameof(GetTemplateList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<TemplateDTO> loRtn = null;
            List<TemplateDTO> loRtnTemp;
            try
            {
                var loDbParameter = new TemplateParamDTO();
                var loCls = new PMB04000Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CPROPERTY_ID);
                loDbParameter.CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CPROGRAM_ID);
                loDbParameter.CTEMPLATE_ID = R_Utility.R_GetStreamingContext<string>(PMB04000ContextDTO.CTEMPLATE_ID);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetTemplateList(loDbParameter);

                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async IAsyncEnumerable<T> HelperStream<T>(List<T> poParameter)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            foreach (T item in poParameter)
            {
                yield return item;
            }
        }
    }
}
