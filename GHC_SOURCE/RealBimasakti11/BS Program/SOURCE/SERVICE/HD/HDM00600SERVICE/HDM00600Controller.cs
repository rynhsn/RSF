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
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HDM00600SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HDM00600Controller : ControllerBase, IHDM00600
    {
        //var & constructor
        private PricelistMaster_Logger _logger;
        private readonly ActivitySource _activitySource;
        public HDM00600Controller(ILogger<PricelistMaster_Logger> logger)
        {
            //initiate
            PricelistMaster_Logger.R_InitializeLogger(logger);
            _logger = PricelistMaster_Logger.R_GetInstanceLogger();
            _activitySource = PricelistMaster_Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        //method
        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            try
            {
                var loCls = new HDM00600Cls();
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
        public IAsyncEnumerable<CurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<CurrencyDTO> loRtnTemp = null;
            try
            {
                var loCls = new HDM00600Cls();

                ShowLogExecute();
                loRtnTemp = loCls.GetCurrencyList(new GeneralParamDTO()
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
        public GeneralAPIResultBaseDTO<ActiveInactivePricelistParam> ActiveInactive_Pricelist(ActiveInactivePricelistParam poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            GeneralAPIResultBaseDTO<ActiveInactivePricelistParam> loRtn = new();
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new HDM00600Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.ActiveInactivePricing(poParam);
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
        public IAsyncEnumerable<PricelistDTO> GetList_Pricelist()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PricelistDTO> loRtnTemp = null;
            try
            {
                var loCls = new HDM00600Cls();

                ShowLogExecute();
                loRtnTemp = loCls.GetList_Pricelist(new GetPricelist_ListParam()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PricelistMaster_ContextConstant.CPROPERTY_ID),
                    CSTATUS = R_Utility.R_GetStreamingContext<string>(PricelistMaster_ContextConstant.CSTATUS),
                    CLANG_ID = R_BackGlobalVar.CULTURE,
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
        public GeneralAPIResultBaseDTO<GeneralFileByteDTO> DownloadFile_TemplateExcelUpload()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new();
            GeneralAPIResultBaseDTO<GeneralFileByteDTO> loRtn = new();
            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_HD_API");
                var lcResourceFile = "BIMASAKTI_HD_API.Template.PricelistTemplate.xlsx";
                ShowLogExecute();
                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var loBytes = ms.ToArray();
                    loRtn.Data = new()
                    {
                        FileBytes = loBytes
                    };
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

        //helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
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
