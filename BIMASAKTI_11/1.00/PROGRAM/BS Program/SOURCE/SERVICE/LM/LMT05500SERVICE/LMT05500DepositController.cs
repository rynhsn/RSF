using PMT05500COMMON.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT05500COMMON.DTO;
using R_CommonFrontBackAPI;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using PMT05500COMMON.Logs;
using PMT05500Back;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Reflection.Metadata;

namespace PMT05500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMT05500DepositController : ControllerBase, ILMT05500Deposit
    {
        private LoggerLMT05500 _loggerLMT5500;
        private readonly ActivitySource _activitySource;
        public LMT05500DepositController(ILogger<LMT05500DepositController> logger)
        {
            //Initial and Get Logger
            LoggerLMT05500.R_InitializeLogger(logger);
            _loggerLMT5500 = LoggerLMT05500.R_GetInstanceLogger();
            _activitySource = LMT05500Activity.R_InitializeAndGetActivitySource(nameof(LMT05500DepositController));
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMT05500DepositInfoDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMT05500DepositInfoDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT5500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMT05500DepositInfoDTO>();

            try
            {
                var loCls = new LMT05500DepositCls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT5500.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT5500.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLMT5500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));


            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<LMT05500DepositInfoDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMT05500DepositInfoDTO> poParameter)
        {

            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT5500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMT05500DepositInfoDTO> loRtn = null;
            LMT05500DepositCls loCls;

            try
            {
                loCls = new LMT05500DepositCls();
                loRtn = new R_ServiceSaveResultDTO<LMT05500DepositInfoDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT5500.LogInfo("Call method R_ServiceSave");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT5500.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _loggerLMT5500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMT05500DepositInfoDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT5500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            LMT05500DepositCls loCls;

            try
            {
                loCls = new LMT05500DepositCls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT5500.LogInfo("Call method R_Delete");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT5500.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _loggerLMT5500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }
        [HttpPost]
        public LMT05500DepositHeaderDTO DepositHeader(LMT05500DBParameter poParam)
        {
            string lcMethodName = nameof(DepositHeader);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT5500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            
            R_Exception loException = new R_Exception();
            LMT05500DepositHeaderDTO loRtn = null;
            LMT05500DepositCls loCls;

            try
            {
                loCls = new LMT05500DepositCls();
                loRtn = new LMT05500DepositHeaderDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT5500.LogInfo("Call method GetDepositHeader");
                loRtn = loCls.GetDepositHeader(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT5500.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMT5500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMT05500DepositListDTO> DepositListStream()
        {
            string lcMethodName = nameof(DepositListStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT5500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMT05500DBParameter loDbParameter;
            IAsyncEnumerable<LMT05500DepositListDTO> loRtn = null;
            List<LMT05500DepositListDTO> loRtnTemp;

            try
            {
                loDbParameter = new LMT05500DBParameter();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; ;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_CODE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                loDbParameter.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_MODE);
                loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBUILDING_ID);
                loDbParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFLOOR_ID);
                loDbParameter.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_ID);


                loDbParameter.LINVOICED = R_Utility.R_GetStreamingContext<bool>(ContextConstant.LINVOICED);

                _loggerLMT5500.LogInfo("Get Parameter DepositListStream on Controller");
                _loggerLMT5500.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                var loCls = new LMT05500DepositCls();
                _loggerLMT5500.LogInfo("Call method DepositListStream");
                loRtnTemp = loCls.GetDepositList(loDbParameter);
                _loggerLMT5500.LogInfo("Call method  to streaming data");
                loRtn = HelperStream(loRtnTemp);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT5500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerLMT5500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMT05500DepositDetailListDTO> DepositDetailListStream()
        {
            string lcMethodName = nameof(DepositDetailListStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT5500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMT05500DBParameter loDbParameter;
            IAsyncEnumerable<LMT05500DepositDetailListDTO> loRtn = null;
            List<LMT05500DepositDetailListDTO> loRtnTemp;

            try
            {
                loDbParameter = new LMT05500DBParameter();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; ;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_CODE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loDbParameter.CSEQ_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEQ_NO);

                _loggerLMT5500.LogInfo("Get Parameter DepositListDetailStream on Controller");
                _loggerLMT5500.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                var loCls = new LMT05500DepositCls();
                _loggerLMT5500.LogInfo("Call method DepositDetailListStream2");
                loRtnTemp = loCls.GetDepositDetailList(loDbParameter);
                _loggerLMT5500.LogInfo("Call method  to streaming data");
                loRtn = HelperStream(loRtnTemp);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT5500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerLMT5500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
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
