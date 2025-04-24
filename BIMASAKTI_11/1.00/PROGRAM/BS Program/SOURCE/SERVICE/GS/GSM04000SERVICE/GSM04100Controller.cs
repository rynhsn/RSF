using GSM04000Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using GSM04000Back;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection;
using GSM04000Common.DTO_s;

namespace GSM04000Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM04100Controller : ControllerBase, IGSM04100
    {
        private LoggerGSM04000 _logger;

        private readonly ActivitySource _activitySource;

        public GSM04100Controller(ILogger<GSM04100Controller> logger)
        {
            //initiate
            LoggerGSM04000.R_InitializeLogger(logger);
            _logger = LoggerGSM04000.R_GetInstanceLogger();
            _activitySource = GSM04000Activity.R_InitializeAndGetActivitySource(nameof(GSM04000Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<UserDepartmentDTO> GetUserDeptList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            List<UserDepartmentDTO> loRtnTemp = null;
            R_Exception loException = new R_Exception(); //declare exeption instance for trycatch
            GSM04100ListDBParameterDTO loDbParam; //dec
            GSM04100Cls loCls;
            try
            {
                loCls = new GSM04100Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetUserDeptList(new DepartmentDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE)
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
        public IAsyncEnumerable<UserDepartmentDTO> GetUserList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            List<UserDepartmentDTO> loRtnTemp = null;
            R_Exception loException = new R_Exception();
            GSM04100Cls loCls;
            try
            {
                loCls = new GSM04100Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetUserToAssignList(
                    new UserLookupListParamDTO()
                    {
                        CPARAMETER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE),
                        CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                        CPROGRAM_CODE = ContextConstant.CPROGRAM_CODE
                    }
                );
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);

        }

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> loRtnTemp)
        {
            foreach (T loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<UserDepartmentDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            GSM04100Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new GSM04100Cls(); //create cls class instance
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
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
        public R_ServiceGetRecordResultDTO<UserDepartmentDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<UserDepartmentDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<UserDepartmentDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM04100Cls loCls = null;
            try
            {
                loCls = new(); //create cls class instance
                loRtn = new();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
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
        public R_ServiceSaveResultDTO<UserDepartmentDTO> R_ServiceSave(R_ServiceSaveParameterDTO<UserDepartmentDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<UserDepartmentDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM04100Cls loCls;
            try
            {
                loRtn = new R_ServiceSaveResultDTO<UserDepartmentDTO>();
                loCls = new GSM04100Cls(); //create instance
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
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