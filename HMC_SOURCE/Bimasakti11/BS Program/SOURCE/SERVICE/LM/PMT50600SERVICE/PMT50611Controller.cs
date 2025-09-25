using PMT50600BACK;
using PMT50600BACK.OpenTelemetry;
using PMT50600COMMON;
using PMT50600COMMON.DTOs;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT50611Controller : ControllerBase, IPMT50611
    {
        private LoggerPMT50611 _logger;
        private readonly ActivitySource _activitySource;

        public PMT50611Controller(ILogger<PMT50611Controller> logger)
        {
            LoggerPMT50611.R_InitializeLogger(logger);
            _logger = LoggerPMT50611.R_GetInstanceLogger();
            _activitySource = PMT50611ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50611Controller));
        }

        [HttpPost]
        public PMT50611DetailResultDTO GetDetailInfo(PMT50611DetailParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetDetailInfo");
            _logger.LogInfo("Start || GetDetailInfo(Controller)");
            R_Exception loException = new R_Exception();
            PMT50611DetailResultDTO loRtn = new PMT50611DetailResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetDetailInfo(Controller)");
                PMT50611Cls loCls = new PMT50611Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetDetailInfo(Cls) || GetDetailInfo(Controller)");
                loRtn.Data = loCls.GetDetailInfo(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetDetailInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public PMT50611HeaderResultDTO GetHeaderInfo(PMT50611HeaderParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetHeaderInfo");
            _logger.LogInfo("Start || GetHeaderInfo(Controller)");
            R_Exception loException = new R_Exception();
            PMT50611HeaderResultDTO loRtn = new PMT50611HeaderResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetHeaderInfo(Controller)");
                PMT50611Cls loCls = new PMT50611Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetHeaderInfo(Cls) || GetHeaderInfo(Controller)");
                loRtn.Data = loCls.GetHeaderInfo(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHeaderInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT50611ListDTO> GetInvoiceItemList()
        {
            using Activity activity = _activitySource.StartActivity("GetInvoiceItemList");
            _logger.LogInfo("Start || GetInvoiceItemList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT50611ListDTO> loRtn = null;
            PMT50611Cls loCls = new PMT50611Cls();
            List<PMT50611ListDTO> loTempRtn = null;
            PMT50611ListParameterDTO loParameter = new PMT50611ListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetInvoiceItemList(Controller)");
                loParameter.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.PMT50611_REC_ID_STREAMING_CONTEXT);
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetInvoiceItemList(Cls) || GetInvoiceItemList(Controller)");
                loTempRtn = loCls.GetInvoiceItemList(loParameter);

                _logger.LogInfo("Run GetInvoiceItemStream(Controller) || GetInvoiceItemList(Controller)");
                loRtn = GetInvoiceItemStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetInvoiceItemList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<PMT50611ListDTO> GetInvoiceItemStream(List<PMT50611ListDTO> poParameter)
        {
            foreach (PMT50611ListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
