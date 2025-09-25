using Global_PMBACK;
using Global_PMCOMMON.DTOs.Generic__DTO;
using Global_PMCOMMON.DTOs.Response.Invoice_Type;
using Global_PMCOMMON.DTOs.Response.Property;
using Global_PMCOMMON.DTOs.User_Param_Detail;
using Global_PMCOMMON.Interface;
using Global_PMCOMMON.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_PMSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class GlobalFunctionPMController : ControllerBase, IGlobalFunctionPM
    {
        private readonly LoggerGlobalFunctionPM _logger;
        private readonly ActivitySource _activitySource;
        public GlobalFunctionPMController(ILogger<GlobalFunctionPMController> logger)
        {
            LoggerGlobalFunctionPM.R_InitializeLogger(logger);
            _logger = LoggerGlobalFunctionPM.R_GetInstanceLogger();
            _activitySource = GlobalFunctionPMActivity.R_InitializeAndGetActivitySource(nameof(GlobalFunctionPMController));

        }     

        [HttpPost]
        public GenericRecord<GetUserParamDetailDTO> UserParamDetail(GetUserParamDetailParameterDTO poParam)
        {
            string lcMethodName = nameof(UserParamDetail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GenericRecord<GetUserParamDetailDTO> loReturn = new();
            try
            {
                var loCls = new GlobalFunctionPMCls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo($"Call method {0}", lcMethodName);
                var loTemp = loCls.GetUserParamDetailDb(poParam);

                loReturn.Data = loTemp;

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> PropertyList()
        {
            string lcMethodName = nameof(PropertyList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<PropertyDTO>? loRtn =null;
            List<PropertyDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PropertyParameterDTO();
                var loCls = new GlobalFunctionPMCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetPropertyListDb(loDbParameter);
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
        public IAsyncEnumerable<InvoiceTypeDTO> InvoiceType(InvoiceTypeParameterDTO poParameter)
        {
            string lcMethodName = nameof(InvoiceType);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<InvoiceTypeDTO>? loRtn = null;
            List<InvoiceTypeDTO> loRtnTemp;
            try
            {
                var loCls = new GlobalFunctionPMCls();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE; 
                _logger.LogDebug("DbParameter {@Parameter} ", poParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetInvoiceTypeDb(poParameter);
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
        public async IAsyncEnumerable<T> HelperStream<T>(List<T> poParameter)
        {
            foreach (T item in poParameter)
            {
                yield return item;
            }
        }

    }
}
