using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GST00500Common;
using R_CommonFrontBackAPI;
using GST00500Back;
using R_BackEnd;
using R_Common;
using GST00500Common.Logs;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GST00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GST00500DraftController : ControllerBase, IGST00500Draft
    {
        private LoggerGST00500 _loggerGST00500;
        private readonly ActivitySource _activitySource;
        public GST00500DraftController(ILogger<GST00500DraftController> logger)
        {
            LoggerGST00500.R_InitializeLogger(logger);
            _loggerGST00500 = LoggerGST00500.R_GetInstanceLogger();
            _activitySource = GST00500Activity.R_InitializeAndGetActivitySource(nameof(GST00500DraftController));

        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GST00500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GST00500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IAsyncEnumerable<GST00500DTO> ApprovalDraftListStream()
        {
            string lcMethodName = nameof(ApprovalDraftListStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GST00500DBParameter loDbParameter;
            IAsyncEnumerable<GST00500DTO> loRtn = null;
            List<GST00500DTO> loRtnTemp;
            try
            {
                loDbParameter = new GST00500DBParameter();
                loDbParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CTRANS_TYPE = "D";

                var loCls = new GST00500DraftCls();
                _loggerGST00500.LogInfo("Call method Approval_Draft_List");
                loRtnTemp = loCls.Approval_Draft_List(loDbParameter);
                loRtn = GetApprovalOutboxList(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGST00500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerGST00500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        private async IAsyncEnumerable<GST00500DTO> GetApprovalOutboxList(List<GST00500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
