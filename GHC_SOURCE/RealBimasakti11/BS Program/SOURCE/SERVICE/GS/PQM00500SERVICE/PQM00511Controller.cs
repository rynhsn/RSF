using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PQM00500BACK;
using PQM00500COMMON;
using PQM00500COMMON.DTO_s;
using PQM00500COMMON.Interfaces;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PQM00500SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PQM00511Controller : ControllerBase, IPQM00511
    {
        private PQM00500Logger _logger;

        private readonly ActivitySource _activitySource;

        public PQM00511Controller(ILogger<PQM00511Controller> logger)
        {
            PQM00500Logger.R_InitializeLogger(logger);
            _logger = PQM00500Logger.R_GetInstanceLogger();
            _activitySource = PQM00500Activity.R_InitializeAndGetActivitySource(nameof(PQM00511Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<MenuUserDTO> GetList_UserMenu()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new();
            List<MenuUserDTO> loRtnTemp = null;
            try
            {
                var loCls = new PQM00511Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetList_UserMenu(
                    new MenuUserDTO
                    {
                        CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(PQM00500ContextConstant.CCOMPANY_ID),
                        CMENU_ID = R_Utility.R_GetStreamingContext<string>(PQM00500ContextConstant.CMENU_ID),
                        CUSER_ID = R_Utility.R_GetStreamingContext<string>(PQM00500ContextConstant.CUSER_ID),
                    });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<MenuUserDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<MenuUserDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<MenuUserDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PQM00511Cls loCls;
            try
            {
                loCls = new(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<MenuUserDTO>();
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<MenuUserDTO> R_ServiceSave(R_ServiceSaveParameterDTO<MenuUserDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<MenuUserDTO> loRtn = null;
            R_Exception loException = new();
            PQM00511Cls loCls;
            try
            {
                loCls = new();
                loRtn = new();
                poParameter.Entity.CUSER_LOGIN = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);//call clsMethod to save
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<MenuUserDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            PQM00511Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new PQM00511Cls(); //create cls class instance
                poParameter.Entity.CUSER_LOGIN = R_BackGlobalVar.USER_ID;
                ShowLogExecute();

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        //helper method
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> loRtnTemp)
        {
            foreach (T loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
    }
}
