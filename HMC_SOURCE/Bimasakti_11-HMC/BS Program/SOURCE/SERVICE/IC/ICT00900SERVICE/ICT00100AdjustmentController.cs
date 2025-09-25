using ICT00900BACK;
using ICT00900COMMON;
using ICT00900COMMON.DTO;
using ICT00900COMMON.Interface;
using ICT00900COMMON.Logs;
using ICT00900COMMON.Param;
using ICT00900COMMON.Utility_DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ICT00900SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ICT00100AdjustmentController : ControllerBase, IICT00900
    {
        private LoggerICT00900 _logger;
        private readonly ActivitySource _activitySource;

        public ICT00100AdjustmentController(ILogger<ICT00100AdjustmentController> logger)
        {
            //Initial and Get Logger
            LoggerICT00900.R_InitializeLogger(logger);
            _logger = LoggerICT00900.R_GetInstanceLogger();
            _activitySource = ICT00900Activity.R_InitializeAndGetActivitySource(nameof(ICT00100AdjustmentController));

        }
        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> PropertyList()
        {
            string lcMethodName = nameof(PropertyList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<PropertyDTO>? loRtn = null;
            List<PropertyDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PropertyParameterDTO();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetPropertyListDb(loDbParameter);
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

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public IAsyncEnumerable<CurrencyDTO> CurrencyList()
        {
            string lcMethodName = nameof(CurrencyList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            IAsyncEnumerable<CurrencyDTO>? loRtn = null;
            List<CurrencyDTO> loRtnTemp;
            try
            {
                var loDbParameter = new PropertyParameterDTO();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetCurrencyListDb(loDbParameter);
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

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            string lcMethodName = nameof(GetVAR_GSM_TRANSACTION_CODE);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            VarGsmTransactionCodeDTO loRtn = null;
            try
            {
                var loDbParameter = new ICT00900ParameterAdjustment();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetVAR_GSM_TRANSACTION_CODEDb(loDbParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public VarGsmCompanyInfoDTO GetVAR_GSM_COMPANY_INFO()
        {
            string lcMethodName = nameof(GetVAR_GSM_COMPANY_INFO);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            VarGsmCompanyInfoDTO loRtn = null;
            try
            {
                var loDbParameter = new ICT00900ParameterAdjustment();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);
                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = loCls.GetVAR_GSM_COMPANY_INFODb(loDbParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<ICT00900AjustmentDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            ICT00900CostAdjustmentCls loCls;

            try
            {
                loCls = new ICT00900CostAdjustmentCls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn!;
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<ICT00900AjustmentDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<ICT00900AjustmentDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<ICT00900AjustmentDetailDTO>();

            try
            {
                var loCls = new ICT00900CostAdjustmentCls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
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
        public R_ServiceSaveResultDTO<ICT00900AjustmentDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<ICT00900AjustmentDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<ICT00900AjustmentDetailDTO>? loRtn = null;
            ICT00900CostAdjustmentCls loCls;

            try
            {
                loCls = new ICT00900CostAdjustmentCls();
                loRtn = new R_ServiceSaveResultDTO<ICT00900AjustmentDetailDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_ServiceSave");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn!;
        }
        [HttpPost]
        public IAsyncEnumerable<ICT00900AdjustmentDTO> GetAdjustmentList()
        {
            string lcMethodName = nameof(GetAdjustmentList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<ICT00900AdjustmentDTO>? loRtn = null;
            List<ICT00900AdjustmentDTO> loRtnTemp;
            try
            {
                var loDbParameter = new ICT00900ParameterAdjustment();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = loCls.GetAdjustmentList(loDbParameter);
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

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.

        }
        [HttpPost]
        public ICT00900AdjustmentDTO ChangeStatusAdjustment(ICT00900ParameterChangeStatusDTO poEntity)
        {
            string lcMethodName = nameof(ChangeStatusAdjustment);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            ICT00900AdjustmentDTO loRtn = new();
            try
            {
                var loCls = new ICT00900CostAdjustmentCls();

                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poEntity);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                bool llReturn = loCls.ChangeStatus(poEntity);

                loRtn.IS_PROCESS_CHANGESTS_SUCCESS = llReturn;
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

        private async IAsyncEnumerable<T> HelperStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }

        }

    }
}
