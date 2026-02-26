using FAM00100Back;
using FAM00100Common.DTOs;
using FAM00100Common.DTOs.FAM00100;
using FAM00100Common.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;

namespace FAM00100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAM00100Controller : ControllerBase, IFAM00100
    {
        private LoggerFAM00100 _Logger;
        private readonly ActivitySource _activitySource;
        public FAM00100Controller(ILogger<LoggerFAM00100> logger)
        {
            //Initial and Get Logger
            LoggerFAM00100.R_InitializeLogger(logger);
            _Logger = LoggerFAM00100.R_GetInstanceLogger();
            _activitySource = FAM00100ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(FAM00100Controller));
        }

        [HttpPost]
        public async Task<FAM00100SingleResult<FAM00100ValidateInitDTO>> GetInitValidate()
        {
            using Activity activity = _activitySource.StartActivity("GetInitValidate");
            var loEx = new R_Exception();
            FAM00100SingleResult<FAM00100ValidateInitDTO> loRtn = new FAM00100SingleResult<FAM00100ValidateInitDTO>();
            _Logger.LogInfo("Start GetInitValidate");

            try
            {
                var loCls = new FAM00100Cls();

                _Logger.LogInfo("Call Back Method GetInitValidate");
                loRtn.Data = await loCls.GetInitValidate();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitValidate");

            return loRtn;
        }

        [HttpPost]
        public async Task<FAM00100SingleResult<FAM00100DTO>> GetSystemParamCB()
        {
            using Activity activity = _activitySource.StartActivity("GetSystemParamCB");
            var loEx = new R_Exception();
            FAM00100SingleResult<FAM00100DTO> loRtn = new FAM00100SingleResult<FAM00100DTO>();
            _Logger.LogInfo("Start GetSystemParamCB");

            try
            {
                var loCls = new FAM00100Cls();

                _Logger.LogInfo("Call Back Method GetSystemParamCB");
                loRtn.Data = await loCls.GetSystemParamCB();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSystemParamCB");

            return loRtn;
        }

        [HttpPost]
        public async Task<FAM00100SingleResult<FAM00100DTO>> SaveSystemParamCB(FAM00100SaveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveSystemParamCB");
            var loEx = new R_Exception();
            FAM00100SingleResult<FAM00100DTO> loRtn = new FAM00100SingleResult<FAM00100DTO>();
            _Logger.LogInfo("Start SaveSystemParamCB");

            try
            {
                var loCls = new FAM00100Cls();

                _Logger.LogInfo("Call Back Method SaveSystemParamCB");
                loRtn.Data = await loCls.SaveSystemParamCB(poEntity.Entity, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveSystemParamCB");

            return loRtn;
        }

        [HttpPost]
        public async Task<FAM00100SingleResult<FAM00100GSPeriodYearRangeDTO>> GetGSPeriodYearRange()
        {
            using Activity activity = _activitySource.StartActivity("GetGSPeriodYearRange");
            var loEx = new R_Exception();
            FAM00100SingleResult<FAM00100GSPeriodYearRangeDTO> loRtn = new FAM00100SingleResult<FAM00100GSPeriodYearRangeDTO>();
            _Logger.LogInfo("Start GetSystemParamCB");

            try
            {
                var loCls = new FAM00100Cls();

                _Logger.LogInfo("Call Back Method GetGSPeriodYearRange");
                loRtn.Data = await loCls.GetPeriodYearRangeRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSPeriodYearRange");

            return loRtn;
        }
    }
}
