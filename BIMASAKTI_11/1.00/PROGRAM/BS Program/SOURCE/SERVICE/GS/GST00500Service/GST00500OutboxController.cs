using GST00500Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using GST00500Back;
using R_BackEnd;
using R_Common;
using GST00500Common.Logs;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Diagnostics;

namespace GST00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GST00500OutboxController : ControllerBase, IGST00500Outbox
    {
        private LoggerGST00500 _loggerGST00500;
        private readonly ActivitySource _activitySource;
        public GST00500OutboxController(ILogger<GST00500OutboxController> logger)
        {
            LoggerGST00500.R_InitializeLogger(logger);
            _loggerGST00500 = LoggerGST00500.R_GetInstanceLogger();
            _activitySource = GST00500Activity.R_InitializeAndGetActivitySource(nameof(GST00500InboxController));

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
        public List<GST00500ApprovalStatusDTO> GetApprovalStatusList()
        {
            string lcMethodName = nameof(GetApprovalStatusList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GST00500DBParameter loParameter = null;
            GST00500OutboxCls loCls = null;
            List<GST00500ApprovalStatusDTO> loRtn = null;
            try
            {
                loCls = new GST00500OutboxCls();
                loParameter = new GST00500DBParameter();
                loRtn = new List<GST00500ApprovalStatusDTO>();

                loParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_CODE);
                loParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                
                _loggerGST00500.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerGST00500.LogDebug("DbParameter {@Parameter} ", loParameter);

                _loggerGST00500.LogInfo("Call method ApproveStatusList");
                loRtn = loCls.ApproverStatusList(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGST00500.LogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();

            _loggerGST00500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GST00500DTO> ApprovalOutboxListStream()
        {
            string lcMethodName = nameof(ApprovalOutboxListStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GST00500DBParameter loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GST00500DTO> loRtn = null;
            List<GST00500DTO> loRtnTemp;
            try
            {
                loDbParameter = new GST00500DBParameter();

                loDbParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CTRANS_TYPE = "O";


                var loCls = new GST00500OutboxCls();
                _loggerGST00500.LogInfo("Call method Approval_Outbox_List");
                loRtnTemp = loCls.Approval_Outbox_List(loDbParameter);
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
