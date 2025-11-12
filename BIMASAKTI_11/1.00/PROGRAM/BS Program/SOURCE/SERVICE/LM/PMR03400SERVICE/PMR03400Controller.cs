using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR03400BACK;
using PMR03400COMMON;
using PMR03400COMMON.DTO_s;
using PMR03400COMMON.DTOs;
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

namespace PMR03400SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMR03400Controller : ControllerBase, IPMR03400General
    {
        private PMR03400Logger _logger;

        private readonly ActivitySource _activitySource;

        //constructor
        public PMR03400Controller(ILogger<PMR03400Controller> logger)
        {
            //initiate
            PMR03400Logger.R_InitializeLogger(logger);
            _logger = PMR03400Logger.R_GetInstanceLogger();
            _activitySource = PMR03400Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public async Task<PMR03400SingleDTO<PMR03400SystemParamDTO>> PMR03400GetSystemParam(PMR03400SystemParamDTO loParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMR03400GetSystemParam));
            _logger.LogInfo("Start - Get System Param");
            var loEx = new R_Exception();
            var loCls = new PMR03400GeneralCls();
            var loReturn = new PMR03400SingleDTO<PMR03400SystemParamDTO>();

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

        private async IAsyncEnumerable<PropertyDTO> StreamPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMR03400GeneralCls loCls;
            try
            {
                loCls = new PMR03400GeneralCls();
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

        private async IAsyncEnumerable<PeriodDtDTO> StreamPeriodList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PeriodDtDTO> loRtnTemp = null;
            PMR03400GeneralCls loCls;
            try
            {
                loCls = new PMR03400GeneralCls();
                ShowLogExecute();
                loRtnTemp = await loCls.GetPeriodDtList(R_BackGlobalVar.COMPANY_ID, R_Utility.R_GetStreamingContext<string>(PMR03400ContextConstant.CYEAR));
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
        public async Task<PMR03400ResultBaseDTO<PeriodYearDTO>> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            PMR03400ResultBaseDTO<PeriodYearDTO> loRtn = new PMR03400ResultBaseDTO<PeriodYearDTO>();

            try
            {
                var loCls = new PMR03400GeneralCls();

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
