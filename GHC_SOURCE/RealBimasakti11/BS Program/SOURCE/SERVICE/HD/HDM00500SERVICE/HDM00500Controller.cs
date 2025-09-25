using HDM00500BACK;
using HDM00500COMMON;
using HDM00500COMMON.DTO.General;
using HDM00500COMMON.DTO_s;
using HDM00500COMMON.DTO_s.Helper;
using HDM00500COMMON.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HDM00500SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HDM00500Controller : ControllerBase, IHDM00500
    {
        //var & constructor
        private Taskchecklist_Logger _logger;
        private readonly ActivitySource _activitySource;
        public HDM00500Controller(ILogger<Taskchecklist_Logger> logger)
        {
            //initiate
            Taskchecklist_Logger.R_InitializeLogger(logger);
            _logger = Taskchecklist_Logger.R_GetInstanceLogger();
            _activitySource = TaskChecklist_Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetList_Property()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            try
            {
                var loCls = new HDM00500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new GeneralParamDTO()
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
            return StreamListHelper(loRtnTemp);
        }
        [HttpPost]
        public IAsyncEnumerable<TaskchecklistDTO> GetList_Taskchecklist()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TaskchecklistDTO> loRtnTemp = null;
            try
            {
                var loCls = new HDM00500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetList_Taskchecklist(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(Taskchecklist_ContextConstant.CPROPERTY_ID),
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
            return StreamListHelper(loRtnTemp);
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<TaskchecklistDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TaskchecklistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<TaskchecklistDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new HDM00500Cls(); //create cls class instance
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                loRtn = new R_ServiceGetRecordResultDTO<TaskchecklistDTO>();
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
        public R_ServiceSaveResultDTO<TaskchecklistDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TaskchecklistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<TaskchecklistDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            HDM00500Cls loCls;
            try
            {
                loCls = new HDM00500Cls();
                loRtn = new R_ServiceSaveResultDTO<TaskchecklistDTO>();
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
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TaskchecklistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            HDM00500Cls loCls = null;
            try
            {
                loRtn = new();
                loCls = new();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
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
        [HttpPost]
        public GeneralAPIResultBaseDTO<object> ActiveInactive_Taskchecklist(TaskchecklistDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            GeneralAPIResultBaseDTO<object> loRtn = new();
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new HDM00500Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.ActiveInactive_Taskchecklist(poParam);
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
        //helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
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
