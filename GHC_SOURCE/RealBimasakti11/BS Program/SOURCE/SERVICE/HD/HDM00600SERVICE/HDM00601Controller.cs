using HDM00600BACK;
using HDM00600COMMON;
using HDM00600COMMON.DTO;
using HDM00600COMMON.DTO.General;
using HDM00600COMMON.DTO.Helper;
using HDM00600COMMON.DTO_s.Helper;
using HDM00600COMMON.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HDM00600SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HDM00601Controller : ControllerBase, IHDM00601
    {
        //var & constructor
        private PricelistMaster_Logger _logger;
        private readonly ActivitySource _activitySource;
        public HDM00601Controller(ILogger<PricelistMaster_Logger> logger)
        {
            //initiate
            PricelistMaster_Logger.R_InitializeLogger(logger);
            _logger = PricelistMaster_Logger.R_GetInstanceLogger();
            _activitySource = PricelistMaster_Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        //method
        [HttpPost]
        public R_ServiceGetRecordResultDTO<PricelistDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PricelistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<PricelistDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new HDM00610Cls(); //create cls class instance
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                loRtn = new R_ServiceGetRecordResultDTO<PricelistDTO>();
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
        public R_ServiceSaveResultDTO<PricelistDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PricelistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<PricelistDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            HDM00610Cls loCls;
            try
            {
                loCls = new HDM00610Cls();
                loRtn = new R_ServiceSaveResultDTO<PricelistDTO>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PricelistDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            HDM00610Cls loCls = null;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new HDM00610Cls(); //create cls class instance
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

        //logger
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");
        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);


    }
}
