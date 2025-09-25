using APT00200COMMON.Loggers;
using APT00200COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00200COMMON.DTOs.APT00220;
using APT00200BACK;
using APT00200COMMON.DTOs.APT00221;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using APT00200BACK.OpenTelemetry;

namespace APT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00220Controller : ControllerBase, IAPT00220
    {
        private LoggerAPT00220 _logger;
        private readonly ActivitySource _activitySource;
        public APT00220Controller(ILogger<APT00220Controller> logger)
        {
            LoggerAPT00220.R_InitializeLogger(logger);
            _logger = LoggerAPT00220.R_GetInstanceLogger();
            _activitySource = APT00220ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00220Controller));
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
                APT00220Cls loCls = new APT00220Cls();
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
