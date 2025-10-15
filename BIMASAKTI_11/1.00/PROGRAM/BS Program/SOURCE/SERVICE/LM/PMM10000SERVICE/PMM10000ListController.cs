using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM10000BACK;
using PMM10000COMMON.Call_Type_Pricelist;
using PMM10000COMMON.Interface;
using PMM10000COMMON.Logs;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using PMM10000COMMON.UtilityDTO;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM10000ListController : ControllerBase, IPMM10000List
    {
        private LoggerPMM10000 _logger;
        private readonly ActivitySource _activitySource;

        public PMM10000ListController(ILogger<PMM10000ListController> logger)
        {
            //Initial and Get Logger
            LoggerPMM10000.R_InitializeLogger(logger);
            _logger = LoggerPMM10000.R_GetInstanceLogger();
            _activitySource = PMM10000Activity.R_InitializeAndGetActivitySource(nameof(PMM10000ListController));

        }
        [HttpPost]
        public IAsyncEnumerable<PMM10000SLACallTypeDTO> GetCallTypeList()
        {
            string lcMethodName = nameof(GetCallTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM10000SLACallTypeDTO>? loRtn = null;
            List<PMM10000SLACallTypeDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM10000DbParameterDTO();
                var loCls = new PMM10000ListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetSLACallTypeList(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<PMM10000CategoryDTO> GetCategoryList()
        {
            string lcMethodName = nameof(GetCategoryList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM10000CategoryDTO>? loRtn = null;
            List<PMM10000CategoryDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM10000DbParameterDTO();
                var loCls = new PMM10000ListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetSLACategoryList(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public IAsyncEnumerable<PMM10000PricelistDTO> GetPricelist()
        {
            string lcMethodName = nameof(GetPricelist);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM10000PricelistDTO>? loRtn = null;
            List<PMM10000PricelistDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM10000DbParameterDTO();
                var loCls = new PMM10000ListCls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CCALL_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCALL_TYPE_ID);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetPricelist(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<PMM10000PricelistDTO> GetAssignPricelist()
        {
            string lcMethodName = nameof(GetAssignPricelist);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMM10000PricelistDTO>? loRtn = null;
            List<PMM10000PricelistDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMM10000DbParameterDTO();
                var loCls = new PMM10000ListCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CCALL_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCALL_TYPE_ID);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetAssignPricelist(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
      

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async IAsyncEnumerable<T> HelperStream<T>(List<T> poParameter)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }

        }


    }
}
