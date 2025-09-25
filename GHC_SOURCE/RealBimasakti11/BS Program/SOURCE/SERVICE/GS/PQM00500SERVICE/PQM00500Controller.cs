using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PQM00500BACK;
using PQM00500COMMON;
using PQM00500COMMON.DTO_s;
using PQM00500COMMON.DTO_s.Base;
using PQM00500COMMON.DTO_s.Helper;
using PQM00500COMMON.Interfaces;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PQM00500SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PQM00500Controller : ControllerBase, IPQM00500
    {
        private PQM00500Logger _logger;

        private readonly ActivitySource _activitySource;

        public PQM00500Controller(ILogger<PQM00500Controller> logger)
        {
            PQM00500Logger.R_InitializeLogger(logger);
            _logger = PQM00500Logger.R_GetInstanceLogger();
            _activitySource = PQM00500Activity.R_InitializeAndGetActivitySource(nameof(PQM00500Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<CompanyInfoDTO> GetList_CompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new();
            List<CompanyInfoDTO> loRtnTemp = null;
            try
            {
                var loCls = new PQM00500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetList_CompanyInfo();
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
        public IAsyncEnumerable<UserDTO> GetList_User()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new();
            List<UserDTO> loRtnTemp = null;
            try
            {
                var loCls = new PQM00500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetList_User(R_Utility.R_GetStreamingContext<string>(PQM00500ContextConstant.CCOMPANY_ID) ?? "");
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
        public GeneralRecordAPIResultDTO<UserDTO> GetRecord_User(UserDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new();
            GeneralRecordAPIResultDTO<UserDTO> loRtn = null;
            try
            {
                var loCls = new PQM00500Cls();
                loRtn = new()
                {
                    data = new()
                };
                ShowLogExecute();
                loRtn.data = loCls.GetList_User(poParam.CCOMPANY_ID).FirstOrDefault(u => u.CUSER_ID == poParam.CUSER_ID)!;
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
