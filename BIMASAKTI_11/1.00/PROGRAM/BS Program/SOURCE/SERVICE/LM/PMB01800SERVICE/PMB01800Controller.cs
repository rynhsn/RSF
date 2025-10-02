using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMB01800BACK;
using PMB01800Common.DTOs;
using PMB01800COMMON;
using PMB01800COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB01800SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMB01800Controller : ControllerBase, IPMB01800
    {
        private LoggerPMB01800 _logger;
        private readonly ActivitySource _activitySource;

        public PMB01800Controller(ILogger<PMB01800Controller> logger)
        {
            //Initial and Get Logger
            LoggerPMB01800.R_InitializeLogger(logger);
            _logger = LoggerPMB01800.R_GetInstanceLogger();
            _activitySource = PMB01800Activity.R_InitializeAndGetActivitySource(nameof(PMB01800Controller));
        }

        [HttpPost]
        public async IAsyncEnumerable<PMB01800GetDepositListDTO> PMB01800GetDepositListStream()
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01800GetDepositListStream));
            _logger.LogInfo("Start - PMB01800GetDepositListStream");
            R_Exception loEx = new();
            PMB01800GetDepositListParamDTO loDbPar;
            List<PMB01800GetDepositListDTO> loRtnTmp = new();
            PMB01800Cls loCls;

            try
            {
                loDbPar = new PMB01800GetDepositListParamDTO();

                _logger.LogInfo("Set Parameter");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMB01800ContextHeaderDTO.CPROPERTY_ID);
                loDbPar.CTRANS_TYPE = R_Utility.R_GetStreamingContext<string>(PMB01800ContextHeaderDTO.CTRANS_TYPE);
                loDbPar.CPAR_TRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMB01800ContextHeaderDTO.CPAR_TRANS_CODE);
                loDbPar.CPAR_DEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMB01800ContextHeaderDTO.CPAR_DEPT_CODE);
                
                loCls = new PMB01800Cls();
                _logger.LogInfo("PMB01800GetDepositListStream");
                loRtnTmp = await loCls.PMB01800GetDepositList(loDbPar);

                // loRtn = GetStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetBillingStatementDetailListStream");

            foreach (var loItem in loRtnTmp)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async Task<PMB01800ListBase<PMB01800PropertyDTO>> PMB01800GetPropertyList()
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMB01800GetPropertyList));
            R_Exception loEx = new();
            PMB01800ListBase<PMB01800PropertyDTO> loReturn = new();
            PMB01800Cls loCls = new();

            try
            {
                _logger.LogInfo("Set Parameter - Get Property List");
                var loParam = new PMB01800PropertyParamDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };

                _logger.LogInfo("Start - Get Property List");
                loReturn.Data = await loCls.GetPropertyList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - Get Property List");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
    }
}
