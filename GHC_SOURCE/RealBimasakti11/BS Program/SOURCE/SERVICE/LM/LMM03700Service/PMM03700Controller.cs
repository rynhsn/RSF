using PMM03700BACK;
using PMM03700COMMON;
using PMM03700COMMON.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Runtime.CompilerServices;
using R_OpenTelemetry;
using System.Diagnostics;
using System.Reflection;

namespace PMM03700Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM03700Controller : ControllerBase, IPMM03700
    {
        private LoggerPMM03700 _logger;

        private readonly ActivitySource _activitySource;

        public PMM03700Controller(ILogger<PMM03700Controller> logger)
        {
            //initiate
            LoggerPMM03700.R_InitializeLogger(logger);
            _logger = LoggerPMM03700.R_GetInstanceLogger();
            _activitySource = PMM03700Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TenantClassificationGroupDTO> loRtnTemp = null;
            PMM03700Cls loCls;
            try
            {
                loCls = new PMM03700Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetTenantClassGrpList(new TenantClassificationGroupDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CPROPERTY_ID),
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            PMM03700Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new PMM03700Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new PMM03700Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn = new R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO>();
                ShowLogExecute();
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<TenantClassificationGroupDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<TenantClassificationGroupDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PMM03700Cls loCls;
            try
            {
                loCls = new PMM03700Cls();
                loRtn = new R_ServiceSaveResultDTO<TenantClassificationGroupDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> PMM03700GetPropertyData()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMM03700Cls loCls;
            try
            {
                loCls = new PMM03700Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new PropertyDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        #region logger
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        #endregion
    }
}