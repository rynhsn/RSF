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
    public class ICT00900Controller : ControllerBase, ICT00900BACK.IICT00900
    {
        private LoggerICT00900 _logger;
        private readonly ActivitySource _activitySource;

        public ICT00900Controller(ILogger<ICT00900Controller> logger)
        {
            //Initial and Get Logger
            LoggerICT00900.R_InitializeLogger(logger);
            _logger = LoggerICT00900.R_GetInstanceLogger();
            _activitySource = ICT00900Activity.R_InitializeAndGetActivitySource(nameof(ICT00900Controller));

        }
        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> PropertyList()
        {
            return GetPropertyStream();
        }
        private async IAsyncEnumerable<PropertyDTO> GetPropertyStream()
        {
            string lcMethodName = nameof(GetPropertyStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;

            try
            {
                var loDbParameter = new PropertyParameterDTO();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = await loCls.GetPropertyListDb(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            foreach (PropertyDTO item in loRtnTemp)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<CurrencyDTO> CurrencyList()
        {
            return GetCurrencyStream();
        }
        private async IAsyncEnumerable<CurrencyDTO> GetCurrencyStream()
        {
            string lcMethodName = nameof(GetCurrencyStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            List<CurrencyDTO> loRtnTemp = null;

            try
            {
                var loDbParameter = new PropertyParameterDTO();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = await loCls.GetCurrencyListDb(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            foreach (CurrencyDTO item in loRtnTemp)
            {
                yield return item;
            }
        }
        [HttpPost]
        public async Task<VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODE()
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
                loRtn = await loCls.GetVAR_GSM_TRANSACTION_CODEDb(loDbParameter);
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
        public async Task<VarGsmCompanyInfoDTO> GetVAR_GSM_COMPANY_INFO()
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
                loRtn = await loCls.GetVAR_GSM_COMPANY_INFODb(loDbParameter);
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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<ICT00900AjustmentDetailDTO> poParameter)
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
                await loCls.R_DeleteAsync(poParameter.Entity);
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
        public async Task<R_ServiceGetRecordResultDTO<ICT00900AjustmentDetailDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<ICT00900AjustmentDetailDTO> poParameter)
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
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
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
        public async Task<R_ServiceSaveResultDTO<ICT00900AjustmentDetailDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<ICT00900AjustmentDetailDTO> poParameter)
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
                loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
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
            return GetAdjustmentStream();
        }
        private async IAsyncEnumerable<ICT00900AdjustmentDTO> GetAdjustmentStream()
        {
            string lcMethodName = nameof(GetAdjustmentList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            var loEx = new R_Exception();
            List<ICT00900AdjustmentDTO> loRtnTemp = null;

            try
            {
                var loDbParameter = new ICT00900ParameterAdjustment();
                var loCls = new ICT00900CostAdjustmentCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo("Call method on Cls");
                loRtnTemp = await loCls.GetAdjustmentList(loDbParameter);
                _logger.LogInfo("Call method to streaming data");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            foreach (ICT00900AdjustmentDTO item in loRtnTemp)
            {
                yield return item;
            }
        }
        [HttpPost]
        public async Task<ICT00900AdjustmentDTO> ChangeStatusAdjustment(ICT00900ParameterChangeStatusDTO poEntity)
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
                bool llReturn = await loCls.ChangeStatus(poEntity);

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

        [HttpPost]
        public async Task<ICT00900AjustmentDetailDTO> GetProdBalanceInfo(ICT00900AjustmentDetailDTO poEntity)
        {
            string lcMethodName = nameof(GetProdBalanceInfo);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            ICT00900AjustmentDetailDTO loRtn = new();
            try
            {
                var loCls = new ICT00900CostAdjustmentCls();

                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poEntity);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn = await loCls.GetProdBalanceInfoDb(poEntity);
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
        public async Task<ICT00900GenericRecord<ICSystemParameterDTO>> GetICSystemParam(BaseDTO poEntity)
        {
            string lcMethodName = nameof(GetICSystemParam);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            ICT00900GenericRecord<ICSystemParameterDTO> loRtn = new();
            try
            {
                var loCls = new ICT00900CostAdjustmentCls();

                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogDebug("DbParameter {@Parameter} ", poEntity);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn.Data = await loCls.GetICSyctemParameter(poEntity);
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
        public async Task<ICT00900GenericRecord<LastCurrencyRateDTO>> GetLastCurrency(LastCurrencyRateDTO poEntity)
        {
            string lcMethodName = nameof(GetLastCurrency);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            ICT00900GenericRecord<LastCurrencyRateDTO> loRtn = new();
            try
            {
                var loCls = new ICT00900CostAdjustmentCls();

                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogDebug("DbParameter {@Parameter} ", poEntity);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtn.Data = await loCls.GetLastCurrency(poEntity);
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
        public async Task<ICT00900AdjustmentDTO> SubmitAdjustment(ICT00900ParameterChangeStatusDTO poEntity)
        {
            string lcMethodName = nameof(SubmitAdjustment);
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
                bool llReturn = await loCls.SubmitCostAdjustment(poEntity);

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
    }
}
