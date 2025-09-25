using PMM01000BACK;
using PMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM01000UniversalController : ControllerBase, IPMM01000Universal
    {
        private LoggerPMM01000Universal _Logger;
        private readonly ActivitySource _activitySource;
        public PMM01000UniversalController(ILogger<LoggerPMM01000Universal> logger)
        {
            //Initial and Get Logger
            LoggerPMM01000Universal.R_InitializeLogger(logger);
            _Logger = LoggerPMM01000Universal.R_GetInstanceLogger();
            _activitySource = PMM01000UniversalActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01000UniversalController));
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetChargesTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetChargesTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetChargesTypeList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetChargesTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllChargesType");
                var loResult = loCls.GetAllChargesType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetChargesTypeList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetChargesTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetStatusList()
        {
            using Activity activity = _activitySource.StartActivity("GetStatusList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetStatusList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetStatusList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllStatus");
                var loResult = loCls.GetAllStatus(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetStatusList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetStatusList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetTaxExemptionCodeList()
        {
            using Activity activity = _activitySource.StartActivity("GetTaxExemptionCodeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetTaxExemptionCodeList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetTaxExemptionCodeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllTaxExemptionCode");
                var loResult = loCls.GetAllTaxExemptionCode(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetTaxExemptionCodeList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTaxExemptionCodeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetWithholdingTaxTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetWithholdingTaxTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetWithholdingTaxTypeList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetWithholdingTaxTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllWithholdingTaxType");
                var loResult = loCls.GetAllWithholdingTaxType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetWithholdingTaxTypeList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetWithholdingTaxTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetUsageRateModeList()
        {
            using Activity activity = _activitySource.StartActivity("GetUsageRateModeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetUsageRateModeList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetUsageRateModeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllUsageRateMode");
                var loResult = loCls.GetAllUsageRateMode(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetUsageRateModeList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetUsageRateModeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetRateTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetRateTypeList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetRateTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllRateType");
                var loResult = loCls.GetAllRateType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateTypeList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetAdminFeeTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetAdminFeeTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetAdminFeeTypeList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetAdminFeeTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAdminFeeType");
                var loResult = loCls.GetAdminFeeType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetAdminFeeTypeList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAdminFeeTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000UniversalDTO> GetAccrualMethodList()
        {
            using Activity activity = _activitySource.StartActivity("GetAccrualMethodList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetAccrualMethodList");

            try
            {
                var loCls = new PMM01000UniversalCls();

                var poParam = new PMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetAccrualMethodList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllAccrualMethod");
                var loResult = loCls.GetAllAccrualMethod(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetAccrualMethodList");
                loRtn = GetStream<PMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAccrualMethodList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01000DTOPropety> GetProperty()
        {
            using Activity activity = _activitySource.StartActivity("GetProperty");
            var loEx = new R_Exception();
            _Logger.LogInfo("Start GetProperty");

            IAsyncEnumerable<PMM01000DTOPropety> loRtn = null;
            var loParameter = new PMM01000PropertyParameterDTO();

            try
            {
                var loCls = new PMM01000UniversalCls();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Logger.LogInfo("Call Stream Method For Stream Data GetProperty");
                loRtn = GetStream<PMM01000DTOPropety>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetProperty");

            return loRtn;
        }

        [HttpPost]
        public PMM01000AllResultInit GetAllInitLMM01000List()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitLMM01000List");
            var loEx = new R_Exception();
            _Logger.LogInfo("Start GetAllInitLMM01000List");

            PMM01000AllResultInit loRtn = new PMM01000AllResultInit();
            var loPropertyParameter = new PMM01000PropertyParameterDTO();
            var loUniversalParam = new PMM01000UniversalDTO();

            try
            {
                var loCls = new PMM01000UniversalCls();

                _Logger.LogInfo("Set Param");
                loPropertyParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loPropertyParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loUniversalParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loUniversalParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetProperty");
                loRtn.PropertyList = loCls.GetProperty(loPropertyParameter);
                loRtn.TaxExemptionCodeList = loCls.GetAllTaxExemptionCode(loUniversalParam);
                loRtn.WithholdingTaxTypeList = loCls.GetAllWithholdingTaxType(loUniversalParam);
                loRtn.AccrualMethodTypeList = loCls.GetAllAccrualMethod(loUniversalParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitLMM01000List");

            return loRtn;
        }
    }
}