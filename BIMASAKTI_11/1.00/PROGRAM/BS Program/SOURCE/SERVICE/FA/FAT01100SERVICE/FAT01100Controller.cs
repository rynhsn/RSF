using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using FAT01100Common;
using FAT01100Common.DTOs;
using FAT01100Back;
using FAT01100Back.DTOs;

namespace FAT01100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAT01100Controller : ControllerBase, IFAT01100
    {
        private readonly LoggerFAT01100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT01100Controller(ILogger<FAT01100Controller> logger)
        {
            LoggerFAT01100.R_InitializeLogger(logger);
            _logger = LoggerFAT01100.R_GetInstanceLogger();
            _activitySource = FAT01100Activity.R_InitializeAndGetActivitySource(nameof(FAT01100Controller));
        }

        [HttpPost]
        public async Task<FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>>> FAT01100GeTransList(FAT01100GeTransListParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GeTransList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<List<FAT01100GeTransListResultDTO>>();

            try
            {
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new FAT01100Cls();
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GeTransList(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public async Task<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>> FAT01100GetGetSystemParam(FAT01100GetGetSystemParamParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetGetSystemParam);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetGetSystemParam(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public async Task<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>> FAT01100GetYearRange(FAT01100GetYearRangeParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetYearRange);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetYearRange(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public async Task<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>> FAT01100GetDeptLookupList(FAT01100GetDeptLookupListParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetDeptLookupList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn.Data = await loCls.FAT01100GetDeptLookupList(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
