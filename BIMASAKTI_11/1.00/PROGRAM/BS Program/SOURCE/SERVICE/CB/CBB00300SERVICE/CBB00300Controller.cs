using CBB00300BACK;
using CBB00300BACK.Open_Telemetry;
using CBB00300COMMON;
using CBB00300COMMON.DTOs;
using CBB00300COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBB00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBB00300Controller : ControllerBase, ICBB00300
    {
        private LoggerCBB00300 _Logger;
        private readonly ActivitySource _activitySource;
        public CBB00300Controller(ILogger<LoggerCBB00300> logger)
        {
            //Initial and Get Logger
            LoggerCBB00300.R_InitializeLogger(logger);
            _Logger = LoggerCBB00300.R_GetInstanceLogger();
            _activitySource = CBB00300ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(CBB00300Controller));
        }

        [HttpPost]
        public GenerateCashflowResultDTO GenerateCashflowProcess(GenerateCashflowParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GenerateCashflowProcess");
            R_Exception loEx = new R_Exception();
            GenerateCashflowResultDTO loRtn = new GenerateCashflowResultDTO();
            _Logger.LogInfo("Start GenerateCashflowProcess");

            try
            {
                CBB00300Cls loCls = new CBB00300Cls();

                _Logger.LogInfo("Call Back Method GenerateCashflowProcessDisplay");
                loCls.GenerateCashflowProcess(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GenerateCashflowProcess");

            return loRtn;
        }

        [HttpPost]
        public CBB00300ResultDTO GetCashflowInfo()
        {
            _Logger.LogInfo("Start GetCashflowInfo");
            using Activity activity = _activitySource.StartActivity("GetCashflowInfo");
            R_Exception loEx = new R_Exception();
            CBB00300ResultDTO loRtn = new CBB00300ResultDTO();

            try
            {
                _Logger.LogInfo("Call Back Method GetCashflowInfo");
                CBB00300Cls loCls = new CBB00300Cls();
                loRtn.Data = loCls.GetCashflowInfo();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetCashflowInfo");
            return loRtn;
        }
    }
}
