using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02100BACK;
using PMT02100BACK.OpenTelemetry;
using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.Loggers;
using R_BackEnd;
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
    public class PMT02100Controller : ControllerBase, IPMT02100
    {
        private LoggerPMT02100 _logger;
        private readonly ActivitySource _activitySource;
        public PMT02100Controller(ILogger<PMT02100Controller> logger)
        {
            LoggerPMT02100.R_InitializeLogger(logger);
            _logger = LoggerPMT02100.R_GetInstanceLogger();
            _activitySource = PMT02100ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02100Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02100HandoverDTO> GetHandoverList()
        {
            using Activity activity = _activitySource.StartActivity("GetHandoverList");
            _logger.LogInfo("Start || GetHandoverList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT02100HandoverDTO> loRtn = null;
            PMT02100Cls loCls = new PMT02100Cls();
            List<PMT02100HandoverDTO> loTempRtn = null;
            PMT02100HandoverParameterDTO loParameter = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetHandoverList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT02100HandoverParameterDTO>(ContextConstant.PMT02100_GET_HANDOVER_LIST_STREAM_CONTEXT);

                _logger.LogInfo("Run GetHandoverList(Cls) || GetHandoverList(Controller)");
                loTempRtn = loCls.GetHandoverList(loParameter);

                _logger.LogInfo("Run GetHandoverStream(Controller) || GetHandoverList(Controller)");
                loRtn = GetHandoverStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHandoverList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<PMT02100HandoverDTO> GetHandoverStream(List<PMT02100HandoverDTO> poParameter)
        {
            foreach (PMT02100HandoverDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public IAsyncEnumerable<PMT02100HandoverBuildingDTO> GetHandoverBuildingList()
        {
            using Activity activity = _activitySource.StartActivity("GetHandoverBuildingList");
            _logger.LogInfo("Start || GetHandoverBuildingList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT02100HandoverBuildingDTO> loRtn = null;
            PMT02100Cls loCls = new PMT02100Cls();
            List<PMT02100HandoverBuildingDTO> loTempRtn = null;
            PMT02100HandoverBuildingParameterDTO loParameter = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetHandoverBuildingList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<PMT02100HandoverBuildingParameterDTO>(ContextConstant.PMT02100_GET_HANDOVER_BUILDING_LIST_STREAM_CONTEXT);

                _logger.LogInfo("Run GetHandoverBuildingList(Cls) || GetHandoverBuildingList(Controller)");
                loTempRtn = loCls.GetHandoverBuildingList(loParameter);

                _logger.LogInfo("Run GetHandoverBuildingStream(Controller) || GetHandoverBuildingList(Controller)");
                loRtn = GetHandoverBuildingStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHandoverBuildingList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<PMT02100HandoverBuildingDTO> GetHandoverBuildingStream(List<PMT02100HandoverBuildingDTO> poParameter)
        {
            foreach (PMT02100HandoverBuildingDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            _logger.LogInfo("Start || GetPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPropertyListDTO> loRtn = null;
            PMT02100Cls loCls = new PMT02100Cls();
            List<GetPropertyListDTO> loTempRtn = null;
            try
            {
                _logger.LogInfo("Run GetPropertyList(Cls) || GetPropertyList(Controller)");
                loTempRtn = loCls.GetPropertyList();

                _logger.LogInfo("Run GetPropertyStream(Controller) || GetPropertyList(Controller)");
                loRtn = GetPropertyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetPropertyListDTO> GetPropertyStream(List<GetPropertyListDTO> poParameter)
        {
            foreach (GetPropertyListDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GetPMSystemParamResultDTO GetPMSystemParam(GetPMSystemParamParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetPMSystemParam");
            _logger.LogInfo("Start || GetPMSystemParam(Controller)");
            R_Exception loException = new R_Exception();
            GetPMSystemParamResultDTO loRtn = new GetPMSystemParamResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPMSystemParam(Controller)");
                PMT02100Cls loCls = new PMT02100Cls();
                
                _logger.LogInfo("Run GetPMSystemParam(Cls) || GetPMSystemParam(Controller)");
                loRtn.Data = loCls.GetPMSystemParam(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPMSystemParam(Controller)");
            return loRtn;
        }
    }
}
