using GSM04000Back;
using GSM04000Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using GSM04000Common.DTO_s;

namespace GSM04000Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM04000Controller : ControllerBase, IGSM04000
    {
        private LoggerGSM04000 _logger;

        private readonly ActivitySource _activitySource;

        public GSM04000Controller(ILogger<GSM04000Controller> logger)
        {
            //initiate
            LoggerGSM04000.R_InitializeLogger(logger);
            _logger = LoggerGSM04000.R_GetInstanceLogger();
            _activitySource = GSM04000Activity.R_InitializeAndGetActivitySource(nameof(GSM04000Controller));
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<DepartmentDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            GSM04000Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new GSM04000Cls(); //create cls class instance
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<DepartmentDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<DepartmentDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<DepartmentDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM04000Cls loCls;
            try
            {
                loCls = new GSM04000Cls(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<DepartmentDTO>();
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
        public R_ServiceSaveResultDTO<DepartmentDTO> R_ServiceSave(R_ServiceSaveParameterDTO<DepartmentDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<DepartmentDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM04000Cls loCls;
            try
            {
                loCls = new GSM04000Cls();
                loRtn = new R_ServiceSaveResultDTO<DepartmentDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);//call clsMethod to save
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
        public IAsyncEnumerable<DepartmentDTO> GetAllDeptList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<DepartmentDTO> loRtnTemp = null;
            GSM04000Cls loCls;
            try
            {
                loCls = new GSM04000Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetDepartmentList(new GeneralParamDTO()
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
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public GeneralAPIResultDTO<DepartmentDTO> ActiveInactiveDepartmentAsync(ActiveInactiveParam poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            GeneralAPIResultDTO<DepartmentDTO> loRtn = new GeneralAPIResultDTO<DepartmentDTO>();
            GSM04000Cls loCls = new GSM04000Cls();
            try
            {
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.ActiveInactiveDept(poParam);
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
        public GeneralAPIResultDTO<DeptUserIsExistDTO> CheckIsUserDeptExist(DepartmentDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            GeneralAPIResultDTO<DeptUserIsExistDTO> loRtn = null;
            GSM04000Cls loCls=null;
            try
            {
                loCls = new();
                loRtn = new();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                ShowLogExecute();
                loRtn.data.LIS_USER_DEPT_EXIST = loCls.CheckIsUserDeptExist(poParam);
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
        public GeneralAPIResultDTO<DeleteAssignedUser> DeleteDeptUserWhenChangingEveryone(DepartmentDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            GeneralAPIResultDTO<DeleteAssignedUser> loRtn = null;
            GSM04000Cls loCls;
            try
            {
                loCls = new(); //create cls class instance
                ShowLogExecute();
                poEntity.CCOMPANY_ID= R_BackGlobalVar.COMPANY_ID;
                loCls.DeleteAssignedUserDept(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loRtn.data.LSUCCESS = !loException.Haserror;
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        [HttpPost]
        public GeneralAPIResultDTO<UploadFileDTO> DownloadUploadDeptTemplate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new();
            GeneralAPIResultDTO<UploadFileDTO> loRtn = new();
            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Department.xlsx";
                ShowLogExecute();
                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var loBytes = ms.ToArray();
                    loRtn.data= new();
                    loRtn.data.FileBytes = loBytes;
                }
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