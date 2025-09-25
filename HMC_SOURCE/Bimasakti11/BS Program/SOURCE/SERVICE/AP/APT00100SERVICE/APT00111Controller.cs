using APT00100BACK;
using APT00100BACK.OpenTelemetry;
using APT00100COMMON;
using APT00100COMMON.DTOs;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using APT00100COMMON.Loggers;
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

namespace APT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00111Controller : ControllerBase, IAPT00111
    {
        private LoggerAPT00111 _logger;
        private readonly ActivitySource _activitySource;

        public APT00111Controller(ILogger<APT00111Controller> logger)
        {
            LoggerAPT00111.R_InitializeLogger(logger);
            _logger = LoggerAPT00111.R_GetInstanceLogger();
            _activitySource = APT00111ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00111Controller));
        }

        [HttpPost]
        public APT00111DetailResultDTO GetDetailInfo(APT00111DetailParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetDetailInfo");
            _logger.LogInfo("Start || GetDetailInfo(Controller)");
            R_Exception loException = new R_Exception();
            APT00111DetailResultDTO loRtn = new APT00111DetailResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetDetailInfo(Controller)");
                APT00111Cls loCls = new APT00111Cls();
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
        public APT00111HeaderResultDTO GetHeaderInfo(APT00111HeaderParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetHeaderInfo");
            _logger.LogInfo("Start || GetHeaderInfo(Controller)");
            R_Exception loException = new R_Exception();
            APT00111HeaderResultDTO loRtn = new APT00111HeaderResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetHeaderInfo(Controller)");
                APT00111Cls loCls = new APT00111Cls();
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
        public IAsyncEnumerable<APT00111ListDTO> GetInvoiceItemList()
        {
            using Activity activity = _activitySource.StartActivity("GetInvoiceItemList");
            _logger.LogInfo("Start || GetInvoiceItemList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<APT00111ListDTO> loRtn = null;
            APT00111Cls loCls = new APT00111Cls();
            List<APT00111ListDTO> loTempRtn = null;
            APT00111ListParameterDTO loParameter = new APT00111ListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetInvoiceItemList(Controller)");
                loParameter.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.APT00111_REC_ID_STREAMING_CONTEXT);
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
        private async IAsyncEnumerable<APT00111ListDTO> GetInvoiceItemStream(List<APT00111ListDTO> poParameter)
        {
            foreach (APT00111ListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
