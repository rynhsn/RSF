using HDM00500BACK;
using HDM00500COMMON;
using HDM00500COMMON.DTO.General;
using HDM00500COMMON.DTO_s;
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
    public class HDM00501Controller : ControllerBase, IHDM00501
    {
        //var const
        private Taskchecklist_Logger _logger;
        private readonly ActivitySource _activitySource;
        public HDM00501Controller(ILogger<Taskchecklist_Logger> logger)
        {
            //initiate
            Taskchecklist_Logger.R_InitializeLogger(logger);
            _logger = Taskchecklist_Logger.R_GetInstanceLogger();
            _activitySource = TaskChecklist_Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        //method
        [HttpPost]
        public IAsyncEnumerable<ChecklistDTO> GetList_Checklist()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<ChecklistDTO> loRtnTemp = null;
            try
            {
                var loCls = new HDM00510Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetList_Checklist(new TaskchecklistDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(Taskchecklist_ContextConstant.CPROPERTY_ID),
                    CTASK_CHECKLIST_ID = R_Utility.R_GetStreamingContext<string>(Taskchecklist_ContextConstant.CTASK_CHECKLIST_ID),
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
        public R_ServiceGetRecordResultDTO<ChecklistDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<ChecklistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<ChecklistDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new HDM00510Cls(); //create cls class instance
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn = new R_ServiceGetRecordResultDTO<ChecklistDTO>();
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
        public R_ServiceSaveResultDTO<ChecklistDTO> R_ServiceSave(R_ServiceSaveParameterDTO<ChecklistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<ChecklistDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            HDM00510Cls loCls;
            try
            {
                loCls = new HDM00510Cls();
                loRtn = new R_ServiceSaveResultDTO<ChecklistDTO>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<ChecklistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            HDM00510Cls loCls = null;
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
