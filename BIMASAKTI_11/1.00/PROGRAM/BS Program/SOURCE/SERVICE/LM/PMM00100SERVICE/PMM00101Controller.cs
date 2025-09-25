using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM00100BACK;
using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMM00100SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM00101Controller : ControllerBase, IPMM00102
    {
        //var&const
        private LoggerPMM00100 _logger;
        private readonly ActivitySource _activitySource;
        public PMM00101Controller(ILogger<PMM00101Controller> logger)
        {
            //initiate
            LoggerPMM00100.R_InitializeLogger(logger);
            _logger = LoggerPMM00100.R_GetInstanceLogger();
            _activitySource = PMM00100Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        //helpers
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");
        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> loRtnTemp)
        {
            foreach (T loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }

        //methods
        [HttpPost]
        public IAsyncEnumerable<HoUtilBuildingMappingDTO> GetBuildingList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<HoUtilBuildingMappingDTO> loRtnTemp = null;
            try
            {
                var loCls = new PMM00102Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetBuildingList(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID=R_Utility.R_GetStreamingContext<string>(PMM00100ContextConstant.CPROPERTY_ID),
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
        public R_ServiceGetRecordResultDTO<HoUtilBuildingMappingDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<HoUtilBuildingMappingDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<HoUtilBuildingMappingDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new PMM00102Cls(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<HoUtilBuildingMappingDTO>();
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
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<HoUtilBuildingMappingDTO> R_ServiceSave(R_ServiceSaveParameterDTO<HoUtilBuildingMappingDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<HoUtilBuildingMappingDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new PMM00102Cls();
                loRtn = new R_ServiceSaveResultDTO<HoUtilBuildingMappingDTO>();
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
            ShowLogEnd();
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<HoUtilBuildingMappingDTO> poParameter)
        {
            throw new NotImplementedException();
        }
    }
}
