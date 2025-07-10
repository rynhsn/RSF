using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02100BACK;
using PMT02100BACK.OpenTelemetry;
using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02110;
using PMT02100COMMON.DTOs.Utility;
using PMT02100COMMON.Loggers;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT02110Controller : ControllerBase, IPMT02110
    {
        private LoggerPMT02110 _logger;
        private readonly ActivitySource _activitySource;
        public PMT02110Controller(ILogger<PMT02110Controller> logger)
        {
            LoggerPMT02110.R_InitializeLogger(logger);
            _logger = LoggerPMT02110.R_GetInstanceLogger();
            _activitySource = PMT02110ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02110Controller));
        }

        [HttpPost]
        public PMT02100VoidResultHelperDTO ConfirmScheduleProcess(PMT02110ConfirmParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("ConfirmScheduleProcess");
            _logger.LogInfo("Start || ConfirmScheduleProcess(Controller)");
            R_Exception loException = new R_Exception();
            PMT02100VoidResultHelperDTO loRtn = new PMT02100VoidResultHelperDTO();

            try
            {
                _logger.LogInfo("Set Parameter || ConfirmScheduleProcess(Controller)");
                PMT02110Cls loCls = new PMT02110Cls();

                _logger.LogInfo("Run ConfirmScheduleProcess(Cls) || ConfirmScheduleProcess(Controller)");
                loCls.ConfirmScheduleProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ConfirmScheduleProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public PMT02110TenantResultDTO GetTenantDetail(PMT02110TenantParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetTenantDetail");
            _logger.LogInfo("Start || GetTenantDetail(Controller)");
            R_Exception loException = new R_Exception();
            PMT02110TenantResultDTO loRtn = new PMT02110TenantResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantDetail(Controller)");
                PMT02110Cls loCls = new PMT02110Cls();

                _logger.LogInfo("Run GetTenantDetail(Cls) || GetTenantDetail(Controller)");
                loRtn.Data = loCls.GetTenantDetail(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantDetail(Controller)");
            return loRtn;
        }
    }
}
