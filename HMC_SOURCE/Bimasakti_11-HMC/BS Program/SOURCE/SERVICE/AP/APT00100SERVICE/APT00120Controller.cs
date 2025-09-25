using APT00100COMMON.Loggers;
using APT00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00100COMMON.DTOs.APT00120;
using APT00100BACK;
using APT00100COMMON.DTOs.APT00121;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using APT00100BACK.OpenTelemetry;

namespace APT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00120Controller : ControllerBase, IAPT00120
    {
        private LoggerAPT00120 _logger;
        private readonly ActivitySource _activitySource;

        public APT00120Controller(ILogger<APT00120Controller> logger)
        {
            LoggerAPT00120.R_InitializeLogger(logger);
            _logger = LoggerAPT00120.R_GetInstanceLogger();
            _activitySource = APT00120ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00120Controller));
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
                APT00120Cls loCls = new APT00120Cls();
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
