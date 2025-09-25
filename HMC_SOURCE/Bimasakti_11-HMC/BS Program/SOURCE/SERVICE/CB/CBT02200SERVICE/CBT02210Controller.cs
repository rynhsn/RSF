using CBT02200BACK.OpenTelemetry;
using CBT02200COMMON.Logger;
using CBT02200COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBT02200COMMON.DTO.CBT02200;
using R_CommonFrontBackAPI;
using CBT02200COMMON.DTO.CBT02210;
using CBT02200BACK;
using R_BackEnd;
using R_Common;

namespace CBT02200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT02210Controller : ControllerBase, ICBT02210
    {
        private LoggerCBT02210 _logger;
        private readonly ActivitySource _activitySource;
        public CBT02210Controller(ILogger<CBT02210Controller> logger)
        {
            LoggerCBT02210.R_InitializeLogger(logger);
            _logger = LoggerCBT02210.R_GetInstanceLogger();
            _activitySource = CBT02210ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(CBT02210Controller));
        }

        [HttpPost]
        public UpdateStatusResultDTO UpdateStatus(UpdateStatusParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("UpdateStatus");
            _logger.LogInfo("Start || UpdateStatus(Controller)");
            R_Exception loException = new R_Exception();
            UpdateStatusResultDTO loRtn = new UpdateStatusResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || UpdateStatus(Controller)");
                CBT02210Cls loCls = new CBT02210Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run UpdateStatus(Cls) || UpdateStatus(Controller)");
                loCls.UpdateStatus(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || UpdateStatus(Controller)");
            return loRtn;
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CBT02210ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            CBT02210Cls loCls = new CBT02210Cls();
            CBT02210ParameterDTO loParam = new CBT02210ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");

                loParam.Data = poParameter.Entity.Data;
                loParam.CTRANS_CODE = poParameter.Entity.CTRANS_CODE;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParam.CACTION = "DELETE";

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(loParam);
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
        public R_ServiceGetRecordResultDTO<CBT02210ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CBT02210ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<CBT02210ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<CBT02210ParameterDTO>();
            CBT02210ParameterDTO loParam = new CBT02210ParameterDTO();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                CBT02210Cls loCls = new CBT02210Cls();

                loParam.Data = poParameter.Entity.Data;
                loParam.CTRANS_CODE = poParameter.Entity.CTRANS_CODE;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(loParam);
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
        public R_ServiceSaveResultDTO<CBT02210ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<CBT02210ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<CBT02210ParameterDTO> loRtn = new R_ServiceSaveResultDTO<CBT02210ParameterDTO>();
            CBT02210Cls loCls = new CBT02210Cls();
            CBT02210ParameterDTO loParam = new CBT02210ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity.Data;
                loParam.CTRANS_CODE = poParameter.Entity.CTRANS_CODE;
                loParam.CCALLER_TRANS_CODE = poParameter.Entity.CCALLER_TRANS_CODE;
                loParam.CCALLER_REF_NO = poParameter.Entity.CCALLER_REF_NO;
                loParam.CCALLER_ID = poParameter.Entity.CCALLER_ID;
                loParam.CGLACCOUNT_NO = poParameter.Entity.CGLACCOUNT_NO;
                loParam.CCASH_FLOW_GROUP_CODE = poParameter.Entity.CCASH_FLOW_GROUP_CODE;
                loParam.CCASH_FLOW_CODE = poParameter.Entity.CCASH_FLOW_CODE;
                loParam.LPAGE = poParameter.Entity.LPAGE;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    loParam.CACTION = "NEW";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    loParam.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(loParam, poParameter.CRUDMode);
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
        public RefreshCurrencyRateResultDTO RefreshCurrencyRate(RefreshCurrencyRateParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RefreshCurrencyRate");
            _logger.LogInfo("Start || RefreshCurrencyRate(Controller)");
            R_Exception loException = new R_Exception();
            RefreshCurrencyRateResultDTO loRtn = new RefreshCurrencyRateResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || RefreshCurrencyRate(Controller)");
                CBT02210Cls loCls = new CBT02210Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run RefreshCurrencyRate(Cls) || RefreshCurrencyRate(Controller)");
                loRtn.Data = loCls.RefreshCurrencyRate(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RefreshCurrencyRate(Controller)");
            return loRtn;
        }
    }
}
