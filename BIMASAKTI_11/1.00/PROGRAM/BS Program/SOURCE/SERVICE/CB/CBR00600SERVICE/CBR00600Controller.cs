using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using CBR00600BACK;
using CBR00600COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace CBR00600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBR00600Controller : ControllerBase, ICBR00600
    {
        private LoggerCBR00600 _Logger;
        private readonly ActivitySource _activitySource;
        public CBR00600Controller(ILogger<LoggerCBR00600> logger)
        {
            //Initial and Get Logger
            LoggerCBR00600.R_InitializeLogger(logger);
            _Logger = LoggerCBR00600.R_GetInstanceLogger();
            _activitySource = CBR00600ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(CBR00600Controller));
        }

        [HttpPost]
        public async Task<CBR00600Record<CBR00600InitialDTO>> GetInitialAsyncDTO()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcess");
            var loEx = new R_Exception();
            CBR00600Record<CBR00600InitialDTO> loRtn = new CBR00600Record<CBR00600InitialDTO>();
            _Logger.LogInfo("Start GetAllInitialProcess");

            try
            {
                var loCls = new CBR00600Cls();

                _Logger.LogInfo("Call Back Method GetAllInitialData");
                loRtn.Data = new CBR00600InitialDTO() 
                { 
                    VAR_PROPERTY_LIST = await loCls.GetPropertyList(),
                    VAR_TRX_TYPE_LIST = await loCls.GetTrxTypeList(),
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcess");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CBR00600PeriodDTO> GetPeriodList()
        {
            return GetPeriodListStream();
        }
        private async IAsyncEnumerable<CBR00600PeriodDTO> GetPeriodListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetPeriodListStream");
            var loEx = new R_Exception();
            List<CBR00600PeriodDTO> loRtn = null;
            _Logger.LogInfo("Start GetPeriodListStream");

            try
            {
                var poParam = new CBR00600PeriodDTO();
                _Logger.LogInfo("Set Param GetPeriodListStream");
                poParam.CCYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                var loCls = new CBR00600Cls();

                _Logger.LogInfo("Call Back Method GetPeriodList");
                loRtn = await loCls.GetPeriodList(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPeriodListStream");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }
    }
}