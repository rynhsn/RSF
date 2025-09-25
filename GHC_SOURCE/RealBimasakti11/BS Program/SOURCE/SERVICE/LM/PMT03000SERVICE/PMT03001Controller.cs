using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT03000BACK;
using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using PMT03000COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMT03000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT03001Controller : ControllerBase, IPMT03001
    {
        private LoggerPMT03000 _logger;
        private readonly ActivitySource _activitySource;
        public PMT03001Controller(ILogger<PMT03001Controller> logger)
        {
            //initiate
            LoggerPMT03000.R_InitializeLogger(logger);
            _logger = LoggerPMT03000.R_GetInstanceLogger();
            _activitySource = PMT03000Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        //helper
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
        public IAsyncEnumerable<UnitTypeCtgFacilityDTO> GetList_UnitTypeCtgFacility()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<UnitTypeCtgFacilityDTO> loRtnTemp = null;
            PMT03001Cls loCls = null;
            try
            {
                loCls = new PMT03001Cls();
                ShowLogExecute();
                loRtnTemp = loCls.Getlist_UnitTypeCtgFacilityDTO(new UnitTypeCtgFacilityDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CPROPERTY_ID),
                    CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CUNIT_TYPE_CATEGORY_ID),
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
        public IAsyncEnumerable<TenantUnitFacilityDTO> GetList_TenantUnitFacility()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TenantUnitFacilityDTO> loRtnTemp = null;
            PMT03001Cls loCls = null;
            try
            {
                loCls = new PMT03001Cls();
                ShowLogExecute();
                loRtnTemp = loCls.Getlist_TenantUnitFacilityDTO(new TenantUnitFacilityDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CPROPERTY_ID),
                    CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CTENANT_ID),
                    CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CBUILDING_ID),
                    CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CFLOOR_ID),
                    CUNIT_ID= R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CUNIT_ID),
                    CFACILITY_TYPE = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CFACILITY_TYPE),
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
        public GeneralAPIResultDTO<FacilityQtyDTO> GetRecord_FacilityQty(TenantUnitFacilityDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            GeneralAPIResultDTO<FacilityQtyDTO> loRtn = null;
            PMT03001Cls loCls;
            try
            {
                loRtn = new();
                loCls = new PMT03001Cls();
                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.data = loCls.GetRecord_FacilityQty(poParam);
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
        public GeneralAPIResultDTO<TenantUnitFacilityDTO> ActiveInactive_TenantUnitFacility(TenantUnitFacilityDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            GeneralAPIResultDTO<TenantUnitFacilityDTO> loRtn = null;
            PMT03001Cls loCls;
            try
            {
                loRtn = new();
                loCls = new PMT03001Cls();
                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.ActiveInactive_TenantUnitFacility(poParam);
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
        public R_ServiceGetRecordResultDTO<TenantUnitFacilityDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TenantUnitFacilityDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<TenantUnitFacilityDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new PMT03001Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn = new R_ServiceGetRecordResultDTO<TenantUnitFacilityDTO>();
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
        public R_ServiceSaveResultDTO<TenantUnitFacilityDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TenantUnitFacilityDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<TenantUnitFacilityDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PMT03001Cls loCls;
            try
            {
                loCls = new PMT03001Cls();
                loRtn = new R_ServiceSaveResultDTO<TenantUnitFacilityDTO>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TenantUnitFacilityDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            PMT03001Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new PMT03001Cls(); //create cls class instance
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
    }
}
