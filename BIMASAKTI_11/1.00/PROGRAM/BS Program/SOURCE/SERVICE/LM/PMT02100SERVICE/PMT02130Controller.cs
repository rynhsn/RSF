using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02100BACK.OpenTelemetry;
using PMT02100BACK;
using PMT02100COMMON.DTOs.PMT02130;
using PMT02100COMMON.Loggers;
using PMT02100COMMON;
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
    public class PMT02130Controller : ControllerBase, IPMT02130
    {
        private LoggerPMT02130 _logger;
        private readonly ActivitySource _activitySource;
        public PMT02130Controller(ILogger<PMT02130Controller> logger)
        {
            LoggerPMT02130.R_InitializeLogger(logger);
            _logger = LoggerPMT02130.R_GetInstanceLogger();
            _activitySource = PMT02130ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02130Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02130HandoverUnitDTO> GetHandoverUnitList()
        {
            using Activity activity = _activitySource.StartActivity("GetHandoverUnitList");
            _logger.LogInfo("Start || GetHandoverUnitList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT02130HandoverUnitDTO> loRtn = null;
            PMT02130Cls loCls = new PMT02130Cls();
            List<PMT02130HandoverUnitDTO> loTempRtn = null;
            PMT02130HandoverUnitParameterDTO loParameter = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetHandoverUnitList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT02130HandoverUnitParameterDTO>(ContextConstant.PMT02130_GET_HANDOVER_UNIT_LIST_STREAM_CONTEXT);

                _logger.LogInfo("Run GetHandoverUnitList(Cls) || GetHandoverUnitList(Controller)");
                loTempRtn = loCls.GetHandoverUnitList(loParameter);

                _logger.LogInfo("Run GetEmployeeStream(Controller) || GetHandoverUnitList(Controller)");
                loRtn = GetHandoverUnitStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHandoverUnitList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<PMT02130HandoverUnitDTO> GetHandoverUnitStream(List<PMT02130HandoverUnitDTO> poParameter)
        {
            foreach (PMT02130HandoverUnitDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02130HandoverUnitChecklistDTO> GetHandoverUnitChecklistList()
        {
            using Activity activity = _activitySource.StartActivity("GetHandoverUnitChecklistList");
            _logger.LogInfo("Start || GetHandoverUnitChecklistList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT02130HandoverUnitChecklistDTO> loRtn = null;
            PMT02130Cls loCls = new PMT02130Cls();
            List<PMT02130HandoverUnitChecklistDTO> loTempRtn = null;
            PMT02130HandoverUnitChecklistParameterDTO loParameter = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetHandoverUnitChecklistList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT02130HandoverUnitChecklistParameterDTO>(ContextConstant.PMT02130_GET_HANDOVER_UNIT_CHECKLIST_LIST_STREAM_CONTEXT);

                _logger.LogInfo("Run GetHandoverUnitChecklistList(Cls) || GetHandoverUnitChecklistList(Controller)");
                loTempRtn = loCls.GetHandoverUnitChecklistList(loParameter);

                _logger.LogInfo("Run GetEmployeeStream(Controller) || GetHandoverUnitChecklistList(Controller)");
                loRtn = GetHandoverUnitChecklistStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHandoverUnitChecklistList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<PMT02130HandoverUnitChecklistDTO> GetHandoverUnitChecklistStream(List<PMT02130HandoverUnitChecklistDTO> poParameter)
        {
            foreach (PMT02130HandoverUnitChecklistDTO item in poParameter)
            {
                yield return item;
            }
        }

    }
}
