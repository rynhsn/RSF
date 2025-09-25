using PMT50600COMMON.Loggers;
using PMT50600COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT50600COMMON.DTOs.PMT50610;
using R_CommonFrontBackAPI;
using PMT50600COMMON.DTOs.PMT50600;
using R_BackEnd;
using R_Common;
using PMT50600BACK;
using PMT50600COMMON.DTOs;
using System.Diagnostics;
using PMT50600BACK.OpenTelemetry;

namespace PMT50600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT50610Controller : ControllerBase, IPMT50610
    {
        private LoggerPMT50610 _logger;
        private readonly ActivitySource _activitySource;
        public PMT50610Controller(ILogger<PMT50610Controller> logger)
        {
            LoggerPMT50610.R_InitializeLogger(logger);
            _logger = LoggerPMT50610.R_GetInstanceLogger();
            _activitySource = PMT50610ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50610Controller));
        }

        [HttpPost]
        public SubmitJournalResultDTO SubmitJournalProcess(SubmitJournalParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("SubmitJournalProcess");
            _logger.LogInfo("Start || SubmitJournalProcess(Controller)");
            R_Exception loException = new R_Exception();
            SubmitJournalResultDTO loRtn = new SubmitJournalResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || SubmitJournalProcess(Controller)");
                PMT50610Cls loCls = new PMT50610Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run SubmitJournalProcess(Cls) || SubmitJournalProcess(Controller)");
                loCls.SubmitJournalProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || SubmitJournalProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public RedraftJournalResultDTO RedraftJournalProcess(RedraftJournalParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RedraftJournalProcess");
            _logger.LogInfo("Start || RedraftJournalProcess(Controller)");
            R_Exception loException = new R_Exception();
            RedraftJournalResultDTO loRtn = new RedraftJournalResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || RedraftJournalProcess(Controller)");
                PMT50610Cls loCls = new PMT50610Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RedraftJournalProcess(Cls) || RedraftJournalProcess(Controller)");
                loCls.RedraftJournalProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RedraftJournalProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public GetCurrencyOrTaxRateResultDTO GetCurrencyOrTaxRate(GetCurrencyOrTaxRateParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCurrencyOrTaxRate");
            _logger.LogInfo("Start || GetCurrencyOrTaxRate(Controller)");
            R_Exception loException = new R_Exception();
            GetCurrencyOrTaxRateResultDTO loRtn = new GetCurrencyOrTaxRateResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetCurrencyOrTaxRate(Controller)");
                PMT50610Cls loCls = new PMT50610Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetCurrencyOrTaxRate(Cls) || GetCurrencyOrTaxRate(Controller)");
                loRtn.Data = loCls.GetCurrencyOrTaxRate(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCurrencyOrTaxRate(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetPaymentTermListDTO> GetPaymentTermList()
        {
            using Activity activity = _activitySource.StartActivity("GetPaymentTermList");
            _logger.LogInfo("Start || GetPaymentTermList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPaymentTermListDTO> loRtn = null;
            PMT50610Cls loCls = new PMT50610Cls();
            List<GetPaymentTermListDTO> loTempRtn = null;
            GetPaymentTermListParameterDTO loParameter = new GetPaymentTermListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetPaymentTermList(Controller)");
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.PMT50610_GET_PROPERTY_ID_STREAMING_CONTEXT);
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPaymentTermList(Cls) || GetPaymentTermList(Controller)");
                loTempRtn = loCls.GetPaymentTermList(loParameter);

                _logger.LogInfo("Run GetPaymentTermStream(Controller) || GetPaymentTermList(Controller)");
                loRtn = GetPaymentTermStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPaymentTermList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetPaymentTermListDTO> GetPaymentTermStream(List<GetPaymentTermListDTO> poParameter)
        {
            foreach (GetPaymentTermListDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT50610ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            PMT50610Cls loCls = new PMT50610Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceDelete(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT50610ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT50610ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT50610ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT50610ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                PMT50610Cls loCls = new PMT50610Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT50610ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT50610ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT50610ParameterDTO> loRtn = new R_ServiceSaveResultDTO<PMT50610ParameterDTO>();
            PMT50610Cls loCls = new PMT50610Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "NEW";
                    poParameter.Entity.Data.CREC_ID = "";
                    poParameter.Entity.Data.CREF_NO = "";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceSave(Controller)");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetCurrencyListDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity("GetCurrencyList");
            _logger.LogInfo("Start || GetCurrencyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetCurrencyListDTO> loRtn = null;
            PMT50610Cls loCls = new PMT50610Cls();
            List<GetCurrencyListDTO> loTempRtn = null;
            GetCurrencyListParameterDTO loParameter = new GetCurrencyListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetCurrencyList(Controller)");
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetCurrencyList(Cls) || GetCurrencyList(Controller)");
                loTempRtn = loCls.GetCurrencyList(loParameter);

                _logger.LogInfo("Run GetCurrencyStream(Controller) || GetCurrencyList(Controller)");
                loRtn = GetCurrencyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCurrencyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetCurrencyListDTO> GetCurrencyStream(List<GetCurrencyListDTO> poParameter)
        {
            foreach (GetCurrencyListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
