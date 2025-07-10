using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02100BACK.OpenTelemetry;
using PMT02100BACK;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.Loggers;
using PMT02100COMMON;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON.DTOs.PMT02120;
using PMT02100COMMON.DTOs.Utility;
using PMT02100COMMON.DTOs.PMT02110;
using PMT02100COMMON.DTOs.PMT02130;

namespace PMT02100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT02120Controller : ControllerBase, IPMT02120
    {
        private LoggerPMT02120 _logger;
        private readonly ActivitySource _activitySource;
        public PMT02120Controller(ILogger<PMT02120Controller> logger)
        {
            LoggerPMT02120.R_InitializeLogger(logger);
            _logger = LoggerPMT02120.R_GetInstanceLogger();
            _activitySource = PMT02120ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02120Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02120EmployeeListDTO> GetEmployeeList()
        {
            using Activity activity = _activitySource.StartActivity("GetEmployeeList");
            _logger.LogInfo("Start || GetEmployeeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT02120EmployeeListDTO> loRtn = null;
            PMT02120Cls loCls = new PMT02120Cls();
            List<PMT02120EmployeeListDTO> loTempRtn = null;
            PMT02120EmployeeListParameterDTO loParameter = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetEmployeeList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT02120EmployeeListParameterDTO>(ContextConstant.PMT02120_GET_EMPLOYEE_LIST_STREAM_CONTEXT);

                _logger.LogInfo("Run GetEmployeeList(Cls) || GetEmployeeList(Controller)");
                loTempRtn = loCls.GetEmployeeList(loParameter);

                _logger.LogInfo("Run GetEmployeeStream(Controller) || GetEmployeeList(Controller)");
                loRtn = GetEmployeeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetEmployeeList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<PMT02120EmployeeListDTO> GetEmployeeStream(List<PMT02120EmployeeListDTO> poParameter)
        {
            foreach (PMT02120EmployeeListDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02120RescheduleListDTO> GetRescheduleList()
        {
            using Activity activity = _activitySource.StartActivity("GetRescheduleList");
            _logger.LogInfo("Start || GetRescheduleList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT02120RescheduleListDTO> loRtn = null;
            PMT02120Cls loCls = new PMT02120Cls();
            List<PMT02120RescheduleListDTO> loTempRtn = null;
            PMT02120RescheduleListParameterDTO loParameter = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetRescheduleList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT02120RescheduleListParameterDTO>(ContextConstant.PMT02120_GET_RESCHEDULE_LIST_STREAM_CONTEXT);

                _logger.LogInfo("Run GetRescheduleList(Cls) || GetRescheduleList(Controller)");
                loTempRtn = loCls.GetRescheduleList(loParameter);

                _logger.LogInfo("Run GetRescheduleStream(Controller) || GetRescheduleList(Controller)");
                loRtn = GetRescheduleStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRescheduleList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<PMT02120RescheduleListDTO> GetRescheduleStream(List<PMT02120RescheduleListDTO> poParameter)
        {
            foreach (PMT02120RescheduleListDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02120HandoverUtilityDTO> GetHandoverUtilityList()
        {
            using Activity activity = _activitySource.StartActivity("GetHandoverUtilityList");
            _logger.LogInfo("Start || GetHandoverUtilityList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT02120HandoverUtilityDTO> loRtn = null;
            PMT02120Cls loCls = new PMT02120Cls();
            List<PMT02120HandoverUtilityDTO> loTempRtn = null;
            PMT02120HandoverUtilityParameterDTO loParameter = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetHandoverUtilityList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT02120HandoverUtilityParameterDTO>(ContextConstant.PMT02120_GET_HANDOVER_UTILITY_LIST_STREAM_CONTEXT);

                _logger.LogInfo("Run GetHandoverUtilityList(Cls) || GetHandoverUtilityList(Controller)");
                loTempRtn = loCls.GetHandoverUtilityList(loParameter);

                _logger.LogInfo("Run GetEmployeeStream(Controller) || GetHandoverUtilityList(Controller)");
                loRtn = GetHandoverUtilityStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHandoverUtilityList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<PMT02120HandoverUtilityDTO> GetHandoverUtilityStream(List<PMT02120HandoverUtilityDTO> poParameter)
        {
            foreach (PMT02120HandoverUtilityDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public PMT02100VoidResultHelperDTO HandoverProcess(PMT02120HandoverProcessParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("HandoverProcess");
            _logger.LogInfo("Start || HandoverProcess(Controller)");
            R_Exception loException = new R_Exception();
            PMT02100VoidResultHelperDTO loRtn = new PMT02100VoidResultHelperDTO();

            try
            {
                _logger.LogInfo("Set Parameter || HandoverProcess(Controller)");
                PMT02120Cls loCls = new PMT02120Cls();

                _logger.LogInfo("Run HandoverProcess(Cls) || HandoverProcess(Controller)");
                loCls.HandoverProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || HandoverProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public PMT02100VoidResultHelperDTO ReinviteProcess(PMT02120ReinviteProcessParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("ReinviteProcess");
            _logger.LogInfo("Start || ReinviteProcess(Controller)");
            R_Exception loException = new R_Exception();
            PMT02100VoidResultHelperDTO loRtn = new PMT02100VoidResultHelperDTO();

            try
            {
                _logger.LogInfo("Set Parameter || ReinviteProcess(Controller)");
                PMT02120Cls loCls = new PMT02120Cls();

                _logger.LogInfo("Run ReinviteProcess(Cls) || ReinviteProcess(Controller)");
                loCls.ReinviteProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ReinviteProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public PMT02100VoidResultHelperDTO RescheduleProcess(PMT02120RescheduleProcessParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RescheduleProcess");
            _logger.LogInfo("Start || RescheduleProcess(Controller)");
            R_Exception loException = new R_Exception();
            PMT02100VoidResultHelperDTO loRtn = new PMT02100VoidResultHelperDTO();

            try
            {
                _logger.LogInfo("Set Parameter || RescheduleProcess(Controller)");
                PMT02120Cls loCls = new PMT02120Cls();

                _logger.LogInfo("Run RescheduleProcess(Cls) || RescheduleProcess(Controller)");
                loCls.RescheduleProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RescheduleProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public PMT02100VoidResultHelperDTO AssignEmployee(PMT02120AssignEmployeeParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("AssignEmployee");
            _logger.LogInfo("Start || AssignEmployee(Controller)");
            R_Exception loException = new R_Exception();
            PMT02100VoidResultHelperDTO loRtn = new PMT02100VoidResultHelperDTO();

            try
            {
                _logger.LogInfo("Set Parameter || AssignEmployee(Controller)");
                PMT02120Cls loCls = new PMT02120Cls();

                _logger.LogInfo("Run AssignEmployee(Cls) || AssignEmployee(Controller)");
                loCls.AssignEmployee(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || AssignEmployee(Controller)");
            return loRtn;
        }
    }
}
