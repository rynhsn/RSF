
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using R_OpenTelemetry;
using PMM00200COMMON;
using PMM00200BACK;
using PMM00200COMMON.DTO_s;

namespace PMM00200SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM00200Controller : ControllerBase, IPMM00200
    {
        private PMM00200Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMM00200Controller(ILogger<PMM00200Controller> logger)
        {
            //initiate
            PMM00200Logger.R_InitializeLogger(logger);
            _logger = PMM00200Logger.R_GetInstanceLogger();
            _activitySource = PMM00200Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PMM00200GridDTO> GetUserParamList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PMM00200GridDTO> loRtnTemp = null;
            PMM00200DBParam loDbParam;
            PMM00200Cls loCls;
            try
            {
                loCls = new PMM00200Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetUserParamList(new PMM00200DBParam()
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
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return PMM00200StreamListHelper(loRtnTemp);
        }

        private async IAsyncEnumerable<T> PMM00200StreamListHelper<T>(List<T> loRtnTemp)
        {
            foreach (T loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM00200DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM00200DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<PMM00200DTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PMM00200Cls loCls;
            try
            {
                loCls = new PMM00200Cls(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<PMM00200DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
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
        public R_ServiceSaveResultDTO<PMM00200DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM00200DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<PMM00200DTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PMM00200Cls loCls;
            try
            {
                loCls = new PMM00200Cls();
                loRtn = new R_ServiceSaveResultDTO<PMM00200DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);//call clsMethod to save
            }
            catch (Exception ex)
            {
                loException.Add(ex); ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public PMM00200ActiveInactiveParamDTO GetActiveParam(ActiveInactiveParam poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            PMM00200ActiveInactiveParamDTO loRtn = null;
            R_Exception loException = new R_Exception();
            PMM00200Cls loCls;
            try
            {
                loCls = new PMM00200Cls();
                loRtn = new PMM00200ActiveInactiveParamDTO();
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //call clsMethod to save
                ShowLogExecute();
                loCls.ActiveInactiveUserParam(poParam);
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

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
    }
}