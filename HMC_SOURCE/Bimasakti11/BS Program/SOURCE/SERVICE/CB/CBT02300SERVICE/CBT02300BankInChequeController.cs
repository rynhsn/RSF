using CBT02300BACK;
using CBT02300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT02300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT02300BankInChequeController : ControllerBase, ICBT02300BankInCheque
    {
        private LoggerCBT02300 _loggerCBT02300;
        private readonly ActivitySource _activitySource;
        public CBT02300BankInChequeController(ILogger<CBT02300BankInChequeController> logger)
        {
            //Initial and Get Logger
            LoggerCBT02300.R_InitializeLogger(logger);
            _loggerCBT02300 = LoggerCBT02300.R_GetInstanceLogger();
            _activitySource = CBT02300Activity.R_InitializeAndGetActivitySource(nameof(CBT02300BankInChequeController));
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<CBT02300ChequeInfoDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CBT02300ChequeInfoDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource?.StartActivity(lcMethodName)!;
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<CBT02300ChequeInfoDTO>();

            try
            {
                var loCls = new CBT02300Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerCBT02300.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerCBT02300.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerCBT02300.LogInfo("END process method R_ServiceGetRecord on Controller");

            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<CBT02300ChequeInfoDTO> R_ServiceSave(R_ServiceSaveParameterDTO<CBT02300ChequeInfoDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CBT02300ChequeInfoDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IAsyncEnumerable<CBT02300BankInChequeDTO> BankInChequeListStream()
        {
            string lcMethodName = nameof(BankInChequeListStream);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethodName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            CBT02300DBFilterListParamDTO loDbParameter;
            IAsyncEnumerable<CBT02300BankInChequeDTO>? loRtn = null;
            List<CBT02300BankInChequeDTO> loRtnTemp;

            try
            {
                loDbParameter = new CBT02300DBFilterListParamDTO();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; ;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                loDbParameter.CTRANS_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_TYPE);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loDbParameter.CCB_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCB_CODE);
                loDbParameter.CCB_ACCOUNT_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCB_ACCOUNT_NO);
                loDbParameter.CDATE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDATE);

                _loggerCBT02300.LogInfo("Get Parameter BankInChequeList on Controller");
                _loggerCBT02300.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                var loCls = new CBT02300Cls();
                _loggerCBT02300.LogInfo("Call method BankInChequeList");
                loRtnTemp = loCls.BankInChequeList(loDbParameter);
                _loggerCBT02300.LogInfo("Call method  to streaming data");
                loRtn = HelperStream(loRtnTemp);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerCBT02300.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerCBT02300.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public CBT02300ChequeInfoDTO BankInChequeInfo(CBT02300DBParamDetailDTO paramDetailDTO)
        {
            string lcMethodName = nameof(CBT02300ChequeInfoDTO);
            using Activity activity = _activitySource?.StartActivity(lcMethodName)!;
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new CBT02300ChequeInfoDTO();

            try
            {
                var loCls = new CBT02300Cls();
                paramDetailDTO.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                paramDetailDTO.CUSER_ID = R_BackGlobalVar.USER_ID;
                paramDetailDTO.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerCBT02300.LogInfo("Call method R_GetRecord");
                loRtn = loCls.BankInChequeInfo(paramDetailDTO);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerCBT02300.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerCBT02300.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }
        private async IAsyncEnumerable<T> HelperStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
