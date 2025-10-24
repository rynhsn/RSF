using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APR00700BACK;
using APR00700COMMON;
using APR00700COMMON.DTO_s;
using APR00700COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace APR00700SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APR00700Controller : ControllerBase, IAPR00700General
    {
        private APR00700Logger _logger;

        private readonly ActivitySource _activitySource;

        //constructor
        public APR00700Controller(ILogger<APR00700Controller> logger)
        {
            //initiate
            APR00700Logger.R_InitializeLogger(logger);
            _logger = APR00700Logger.R_GetInstanceLogger();
            _activitySource = APR00700Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public async Task<APR00700SingleDTO<APR00700SystemParamDTO>> APR00700GetSystemParam(APR00700SystemParamDTO loParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(APR00700GetSystemParam));
            _logger.LogInfo("Start - Get System Param");
            var loEx = new R_Exception();
            var loCls = new APR00700GeneralCls();
            var loReturn = new APR00700SingleDTO<APR00700SystemParamDTO>();

            try
            {
                _logger.LogInfo("Set Parameter");

                _logger.LogInfo("Get System Param");
                var loResult = await loCls.GetSystemParam(loParam);
                loReturn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Get System Param");
            return loReturn;
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            
            return StreamPropertyList();
        }

        [HttpPost]
        private async IAsyncEnumerable<PropertyDTO> StreamPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            APR00700GeneralCls loCls;
            try
            {
                loCls = new APR00700GeneralCls();
                ShowLogExecute();
                loRtnTemp = await loCls.GetPropertyList(new PropertyDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            foreach (var loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PeriodDtDTO> GetPeriodList()
        {
            return StreamPeriodList();
        }

        [HttpPost]
        private async IAsyncEnumerable<PeriodDtDTO> StreamPeriodList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PeriodDtDTO> loRtnTemp = null;
            APR00700GeneralCls loCls;
            try
            {
                loCls = new APR00700GeneralCls();
                ShowLogExecute();
                loRtnTemp = await loCls.GetPeriodDtList(R_BackGlobalVar.COMPANY_ID, R_Utility.R_GetStreamingContext<string>(APR00700ContextConstant.CYEAR));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            foreach (var loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public async Task<APR00700ResultBaseDTO<PeriodYearDTO>> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            APR00700ResultBaseDTO<PeriodYearDTO> loRtn = new APR00700ResultBaseDTO<PeriodYearDTO>();

            try
            {
                var loCls = new APR00700GeneralCls();

                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.Data = await loCls.GetPeriodYearRecord(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();

            return loRtn;
        }

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion

    }
}
