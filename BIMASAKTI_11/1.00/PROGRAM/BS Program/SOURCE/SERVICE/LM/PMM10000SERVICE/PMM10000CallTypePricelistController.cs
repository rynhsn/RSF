using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM10000BACK;
using PMM10000COMMON.Call_Type_Pricelist;
using PMM10000COMMON.Interface;
using PMM10000COMMON.Logs;
using PMM10000COMMON.UtilityDTO;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM10000CallTypePricelistController : ControllerBase, IPMM10000CallTypePricelist
    {
        private LoggerPMM10000 _logger;
        private readonly ActivitySource _activitySource;

        public PMM10000CallTypePricelistController(ILogger<PMM10000CallTypePricelistController> logger)
        {
            //Initial and Get Logger
            LoggerPMM10000.R_InitializeLogger(logger);
            _logger = LoggerPMM10000.R_GetInstanceLogger();
            _activitySource = PMM10000Activity.R_InitializeAndGetActivitySource(nameof(PMM10000CallTypePricelistController));

        }
        [HttpPost]
        public AssignUnassignPricelistDTO AssignPricelist(PMM10000DbParameterDTO poParameter)
        {
            string lcMethodName = nameof(AssignPricelist);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new AssignUnassignPricelistDTO();

            try
            {
                var loCls = new PMM10000CallTypePricelistCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Method AssignPricelist");
                loCls.AssignPricelist(poParameter);

                if (!loEx.Haserror)
                {
                    loRtn.LSTATUS = true;
                }
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
        public AssignUnassignPricelistDTO UnassignPricelist(PMM10000DbParameterDTO poParameter)
        {
            string lcMethodName = nameof(UnassignPricelist);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new AssignUnassignPricelistDTO();

            try
            {
                var loCls = new PMM10000CallTypePricelistCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Method UnassignPricelist");
                loCls.UnassignPricelist(poParameter);

                if (!loEx.Haserror)
                {
                    loRtn.LSTATUS = true;
                }
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
    }
}
