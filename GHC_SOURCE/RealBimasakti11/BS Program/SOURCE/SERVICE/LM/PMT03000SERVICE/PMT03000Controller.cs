using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT03000BACK;
using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using PMT03000COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMT03000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT03000Controller : ControllerBase, IPMT03000
    {
        private LoggerPMT03000 _logger;
        private readonly ActivitySource _activitySource;
        public PMT03000Controller(ILogger<PMT03000Controller> logger)
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
        public IAsyncEnumerable<BuildingDTO> GetList_Building()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<BuildingDTO> loRtnTemp = null;
            PMT03000Cls loCls = null;
            try
            {
                loCls = new PMT03000Cls();
                ShowLogExecute();
                loRtnTemp = loCls.Getlist_Building(new GeneralParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CPROPERTY_ID),
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
        public IAsyncEnumerable<BuildingUnitDTO> GetList_BuildingUnit()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<BuildingUnitDTO> loRtnTemp = null;
            PMT03000Cls loCls = null;
            try
            {
                loCls = new PMT03000Cls();
                ShowLogExecute();
                loRtnTemp = loCls.Getlist_BuildingUnit(new BuildingDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CPROPERTY_ID),
                    CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CBUILDING_ID),
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
        public IAsyncEnumerable<TransByUnitDTO> GetList_TransByUnit()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TransByUnitDTO> loRtnTemp = null;
            PMT03000Cls loCls = null;
            try
            {
                loCls = new PMT03000Cls();
                ShowLogExecute();
                loRtnTemp = loCls.Getlist_TransByUnit(new BuildingUnitDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CPROPERTY_ID),
                    CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CBUILDING_ID),
                    CFLOOR_ID= R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CFLOOR_ID),
                    CUNIT_ID= R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CUNIT_ID),
                    CUNIT_TYPE_CATEGORY_ID= R_Utility.R_GetStreamingContext<string>(PMT03000ContextConstant.CUNIT_TYPE_CATEGORY_ID),
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
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMT03000Cls loCls = null;
            try
            {
                loCls = new PMT03000Cls();
                ShowLogExecute();
                loRtnTemp = loCls.Getlist_Property(new GeneralParamDTO()
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
    }
}
