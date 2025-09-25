using GST00500Back;
using GST00500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using System.Data.Common;
using GST00500Common.Logs;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GST00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GST00500InboxController : ControllerBase, IGST00500
    {
        private LoggerGST00500 _loggerGST00500;
        private readonly ActivitySource _activitySource;
        public GST00500InboxController(ILogger<GST00500InboxController> logger)
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
        public GST00500UserNameDTO GetUserName()
        {
            string lcMethodName = nameof(GetUserName);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GST00500UserNameDTO loReturn = null;
            var loParameter = new GST00500DBParameter();
            var loCls = new GST00500Cls();
            try
            {
                loReturn = new GST00500UserNameDTO();
                loParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerGST00500.LogInfo("Call method GetUserName");

                loReturn = loCls.GetUserName(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGST00500.LogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();

            _loggerGST00500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }

        [HttpPost]
        public GST00500RejectListDTO ReasonRejectList()
        {
            string lcMethodName = nameof(ReasonRejectList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            GST00500RejectListDTO loRtn = null;
            var loParameter = new GST00500DBParameter();
            var loCls = new GST00500Cls();
            try
            {
                loRtn = new GST00500RejectListDTO();
                loParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerGST00500.LogInfo("Call method GetReasonRejectList");
                var loResult = loCls.GetReasonRejectList(loParameter);
                loRtn.Data = loResult;
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
        public IAsyncEnumerable<GST00500DTO> ApprovalInboxListStream()
        {
            string lcMethodName = nameof(ApprovalInboxListStream);
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
                loDbParameter.CTRANS_TYPE = "I";

                var loCls = new GST00500Cls();
                _loggerGST00500.LogInfo("Call method Approval_Inbox_List");
                loRtnTemp = loCls.Approval_Inbox_List(loDbParameter);
                loRtn = GetApprovalInboxList(loRtnTemp);
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

        private async IAsyncEnumerable<GST00500DTO> GetApprovalInboxList(List<GST00500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}