using PMM03700BACK;
using PMM03700COMMON;
using PMM03700COMMON.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMM03700Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM03710Controller : ControllerBase, IPMM03710
    {
        private LoggerPMM03700 _logger;

        private readonly ActivitySource _activitySource;

        public PMM03710Controller(ILogger<PMM03710Controller> logger)
        {
            //initiate
            LoggerPMM03700.R_InitializeLogger(logger);
            _logger = LoggerPMM03700.R_GetInstanceLogger();
            _activitySource = PMM03700Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        private async IAsyncEnumerable<T> StreamHelper<T>(List<T> entityList)
        {
            foreach (T entity in entityList)
            {
                yield return entity;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TenantClassificationDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            PMM03710Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new PMM03710Cls();
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
        public R_ServiceGetRecordResultDTO<TenantClassificationDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TenantClassificationDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<TenantClassificationDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn = new R_ServiceGetRecordResultDTO<TenantClassificationDTO>();
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
        public R_ServiceSaveResultDTO<TenantClassificationDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TenantClassificationDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<TenantClassificationDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls();
                loRtn = new R_ServiceSaveResultDTO<TenantClassificationDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CTENANT_CLASSIFICATION_GROUP_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID);
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
        public IAsyncEnumerable<TenantClassificationDTO> GetTenantClassificationList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TenantClassificationDTO> loRtnTemp = null;
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetTenantClassList(new TenantClassificationDBListMaintainParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CTENANT_CLASSIFICATION_GROUP_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID),
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
            return StreamHelper<TenantClassificationDTO>(loRtnTemp);
        }

        [HttpPost]
        public IAsyncEnumerable<TenantDTO> GetAssignedTenantList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TenantDTO> loRtnTemp = null;
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetAssignedTenantList(new TenantClassificationDBListMaintainParamDTO()
                {

                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CPROPERTY_ID),
                    CTENANT_CLASSIFICATION_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CTENANT_CLASSIFICATION_ID),
                    CUSER_ID = R_BackGlobalVar.USER_ID,
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
            return StreamHelper<TenantDTO>(loRtnTemp);
        }

        [HttpPost]
        public IAsyncEnumerable<TenantDTO> GetAvailableTenantList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TenantDTO> loRtnTemp = null;
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetTenantToAssigntList(new TenantClassificationDBListMaintainParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CTENANT_CLASSIFICATION_GROUP_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID),
                    CTENANT_CLASSIFICATION_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CTENANT_CLASSIFICATION_ID),
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
            return StreamHelper<TenantDTO>(loRtnTemp);
        }

        [HttpPost]
        public IAsyncEnumerable<TenantDTO> GetTenantToMoveList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TenantDTO> loRtnTemp = null;
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetTenantToMoveList(new TenantParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CTENANT_CLASSIFICATION_ID = R_Utility.R_GetStreamingContext<string>(PMM03700ContextConstant.CTENANT_CLASSIFICATION_ID),
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
            return StreamHelper<TenantDTO>(loRtnTemp);
        }

        [HttpPost]
        public TenantResultDumpDTO AssignTenant(TenantParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.AssignTenant(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return new();
        }

        [HttpPost]
        public TenantResultDumpDTO MoveTenant(TenantMoveParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            PMM03710Cls loCls;
            try
            {
                loCls = new PMM03710Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loCls.MoveTenantUsingRDT(poParam);
                ShowLogExecute();
                loCls.MoveTenant(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return new();
        }

        #region logger
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        #endregion

    }
}
