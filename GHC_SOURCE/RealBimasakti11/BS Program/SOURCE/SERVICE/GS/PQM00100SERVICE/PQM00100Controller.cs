using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PQM00100BACK;
using PQM00100COMMON;
using PQM00100COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PQM00100SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PQM00100Controller : ControllerBase, IPQM00100
    {
        private PQM00100Logger _logger;

        private readonly ActivitySource _activitySource;

        public PQM00100Controller(ILogger<PQM00100Controller> logger)
        {
            PQM00100Logger.R_InitializeLogger(logger);
            _logger = PQM00100Logger.R_GetInstanceLogger();
            _activitySource = PQM00100Activity.R_InitializeAndGetActivitySource(nameof(PQM00100Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<ServiceGridDTO> GetList_Service()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new();
            List<ServiceGridDTO> loRtnTemp = null;
            try
            {
                var loCls = new PQM00100Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetList_Service();
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
        public R_ServiceGetRecordResultDTO<ServiceGridDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<ServiceGridDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<ServiceGridDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PQM00100Cls loCls;
            try
            {
                loCls = new(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<ServiceGridDTO>();
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
        public R_ServiceSaveResultDTO<ServiceGridDTO> R_ServiceSave(R_ServiceSaveParameterDTO<ServiceGridDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<ServiceGridDTO> loRtn = null;
            R_Exception loException = new();
            PQM00100Cls loCls;
            try
            {
                loCls = new();
                loRtn = new();
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<ServiceGridDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            PQM00100Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new PQM00100Cls(); //create cls class instance
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