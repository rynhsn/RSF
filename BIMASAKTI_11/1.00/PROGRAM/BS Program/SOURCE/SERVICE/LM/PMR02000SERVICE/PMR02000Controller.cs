using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR02000BACK;
using PMR02000COMMON;
using PMR02000COMMON.DTO_s;
using PMR02000COMMON.DTO_s.Print;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMR02000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMR02000Controller : ControllerBase, IPMR02000
    {
        private PMR02000Logger _logger;

        private readonly ActivitySource _activitySource;

        //constructor
        public PMR02000Controller(ILogger<PMR02000Controller> logger)
        {
            //initiate
            PMR02000Logger.R_InitializeLogger(logger);
            _logger = PMR02000Logger.R_GetInstanceLogger();
            _activitySource = PMR02000Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMR02000GeneralCls loCls;
            try
            {
                loCls = new PMR02000GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new PropertyDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
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
        public GeneralResultDTO<TodayDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GeneralResultDTO<TodayDTO> loRtn = new GeneralResultDTO<TodayDTO>();

            try
            {
                var loCls = new PMR02000GeneralCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetTodayDateRecord(R_BackGlobalVar.COMPANY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();

            return loRtn;
        }

        [HttpPost]
        public GeneralResultDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GeneralResultDTO<PeriodYearDTO> loRtn = new GeneralResultDTO<PeriodYearDTO>();

            try
            {
                var loCls = new PMR02000GeneralCls();

                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.Data = loCls.GetPeriodYearRecord(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CategoryTypeDTO> GetCategoryTypeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<CategoryTypeDTO> loRtnTemp = null;
            PMR02000GeneralCls loCls;
            try
            {
                loCls = new PMR02000GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetCustomerTypeList(new CategoryTypeParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMR02000ContextConstant.CPROPERTY_ID),
                    CCATEGORY_TYPE = R_Utility.R_GetStreamingContext<string>(PMR02000ContextConstant.CCATEGORY_TYPE),
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
        public IAsyncEnumerable<TransactionTypeDTO> GetTransTypeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TransactionTypeDTO> loRtnTemp = null;
            PMR02000GeneralCls loCls;
            try
            {
                loCls = new PMR02000GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetTransTypeList(new TransactionTypeDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
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
        //stream list helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

       



        #endregion

    }
}
