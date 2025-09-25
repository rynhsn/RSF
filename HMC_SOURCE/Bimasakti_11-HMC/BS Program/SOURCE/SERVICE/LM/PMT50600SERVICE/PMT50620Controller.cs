using PMT50600COMMON.Loggers;
using PMT50600COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT50600COMMON.DTOs.PMT50620;
using PMT50600BACK;
using PMT50600COMMON.DTOs.PMT50621;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using PMT50600BACK.OpenTelemetry;

namespace PMT50600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT50620Controller : ControllerBase, IPMT50620
    {
        private LoggerPMT50620 _logger;
        private readonly ActivitySource _activitySource;

        public PMT50620Controller(ILogger<PMT50620Controller> logger)
        {
            LoggerPMT50620.R_InitializeLogger(logger);
            _logger = LoggerPMT50620.R_GetInstanceLogger();
            _activitySource = PMT50620ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50620Controller));
        }

        [HttpPost]
        public OnCloseProcessResultDTO CloseFormProcess(OnCloseProcessParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("CloseFormProcess");
            _logger.LogInfo("Start || CloseFormProcess(Controller)");
            R_Exception loException = new R_Exception();
            OnCloseProcessResultDTO loRtn = new OnCloseProcessResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || CloseFormProcess(Controller)");
                PMT50620Cls loCls = new PMT50620Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run CloseFormProcess(Cls) || CloseFormProcess(Controller)");
                loCls.CloseFormProcess(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || CloseFormProcess(Controller)");
            return loRtn;
        }
    }
}
