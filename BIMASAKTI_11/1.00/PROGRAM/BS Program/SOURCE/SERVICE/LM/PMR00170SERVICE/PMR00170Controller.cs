using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00170BACK;
using PMR00170COMMON;
using PMR00170COMMON.Utility_Report;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMR00170SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMR00170Controller : ControllerBase, IPMR00170
    {
        private LoggerPMR00170 _logger;
        private readonly ActivitySource _activitySource;
        public PMR00170Controller(ILogger<PMR00170Controller> logger)
        {
            LoggerPMR00170.R_InitializeLogger(logger);
            _logger= LoggerPMR00170.R_GetInstanceLogger();
            _activitySource = PMR00170Activity.R_InitializeAndGetActivitySource(nameof(PMR00170Controller));
        }
        [HttpPost]
        public IAsyncEnumerable<PMR00170PropertyDTO> GetPropertyListStream()
        {
            string lcMethodName = nameof(GetPropertyListStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMR00170PropertyDTO> loRtn = null;
            List<PMR00170PropertyDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PMR00170DBParamDTO();
                var loCls = new PMR00170Cls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method GetAllPropertyList");
                loRtnTemp = loCls.GetPropertyList(loDbParameter);
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
            return loRtn;
        }
        [HttpPost]
        public PMR00170InitialProcess GetInitialProcess()
        {
            string lcMethodName = nameof(GetInitialProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            PMR00170DBParamDTO loDbParameter = new();
            PMR00170InitialProcess loReturn = null;
            try
            {
                var loCls = new PMR00170Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Call method InitialProcess on Controller");

                loReturn = loCls.GetInitialProcess(loDbParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn!;
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
