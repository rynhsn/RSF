using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM09000Back;
using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Interface;
using PMM09000COMMON.Logs;
using PMM09000COMMON.UtiliyDTO;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM09000GetRecordController : ControllerBase, IPMM09000RecordController
    {
        private LoggerPMM09000 _logger;
        private readonly ActivitySource _activitySource;

        public PMM09000GetRecordController(ILogger<PMM09000GetRecordController> logger)
        {
            //Initial and Get Logger
            LoggerPMM09000.R_InitializeLogger(logger);
            _logger = LoggerPMM09000.R_GetInstanceLogger();
            _activitySource = PMM09000Activity.R_InitializeAndGetActivitySource(nameof(PMM09000GetRecordController));

        }
        [HttpPost]
        public PMM09000EntryHeaderDTO GetAmortizationDetail(PMM09000DbParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetAmortizationDetail);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new PMM09000EntryHeaderDTO();

            try
            {
                var loCls = new PMM09000RecordCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method on Cls");
                loRtn = loCls.GetAmortizationDetail(poParameter);

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
