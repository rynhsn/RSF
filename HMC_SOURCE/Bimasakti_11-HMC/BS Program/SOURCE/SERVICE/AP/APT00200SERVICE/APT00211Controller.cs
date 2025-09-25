using APT00200BACK;
using APT00200BACK.OpenTelemetry;
using APT00200COMMON;
using APT00200COMMON.DTOs;
using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using APT00200COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00211Controller : ControllerBase, IAPT00211
    {
        private LoggerAPT00211 _logger;
        private readonly ActivitySource _activitySource;
        public APT00211Controller(ILogger<APT00211Controller> logger)
        {
            LoggerAPT00211.R_InitializeLogger(logger);
            _logger = LoggerAPT00211.R_GetInstanceLogger();
            _activitySource = APT00211ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00211Controller));
        }

        [HttpPost]
        public APT00211DetailResultDTO GetDetailInfo(APT00211DetailParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetDetailInfo");
            _logger.LogInfo("Start || GetDetailInfo(Controller)");
            R_Exception loException = new R_Exception();
            APT00211DetailResultDTO loRtn = new APT00211DetailResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetDetailInfo(Controller)");
                APT00211Cls loCls = new APT00211Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetDetailInfo(Cls) || GetDetailInfo(Controller)");
                loRtn.Data = loCls.GetDetailInfo(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetDetailInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public APT00211HeaderResultDTO GetHeaderInfo(APT00211HeaderParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetHeaderInfo");
            _logger.LogInfo("Start || GetHeaderInfo(Controller)");
            R_Exception loException = new R_Exception();
            APT00211HeaderResultDTO loRtn = new APT00211HeaderResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetHeaderInfo(Controller)");
                APT00211Cls loCls = new APT00211Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetHeaderInfo(Cls) || GetHeaderInfo(Controller)");
                loRtn.Data = loCls.GetHeaderInfo(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetHeaderInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APT00211ListDTO> GetPurchaseReturnItemList()
        {
            using Activity activity = _activitySource.StartActivity("GetPurchaseReturnItemList");
            _logger.LogInfo("Start || GetPurchaseReturnItemList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<APT00211ListDTO> loRtn = null;
            APT00211Cls loCls = new APT00211Cls();
            List<APT00211ListDTO> loTempRtn = null;
            APT00211ListParameterDTO loParameter = new APT00211ListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPurchaseReturnItemList(Controller)");
                loParameter.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.APT00211_REC_ID_STREAMING_CONTEXT);
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetPurchaseReturnItemList(Cls) || GetPurchaseReturnItemList(Controller)");
                loTempRtn = loCls.GetPurchaseReturnItemList(loParameter);

                _logger.LogInfo("Run GetPurchaseReturnItemStream(Controller) || GetPurchaseReturnItemList(Controller)");
                loRtn = GetPurchaseReturnItemStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPurchaseReturnItemList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<APT00211ListDTO> GetPurchaseReturnItemStream(List<APT00211ListDTO> poParameter)
        {
            foreach (APT00211ListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
