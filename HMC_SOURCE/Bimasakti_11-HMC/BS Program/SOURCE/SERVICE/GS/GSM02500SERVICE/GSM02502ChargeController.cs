using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02502ChargeController : ControllerBase, IGSM02502Charge
    {
        private LoggerGSM02502Charge _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502ChargeController(ILogger<GSM02502ChargeController> logger)
        {
            LoggerGSM02502Charge.R_InitializeLogger(logger);
            _logger = LoggerGSM02502Charge.R_GetInstanceLogger();
            _activitySource = GSM02502ChargeActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02502ChargeController));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02502ChargeDTO> GetChargeList()
        {
            using Activity activity = _activitySource.StartActivity("GetChargeList");
            _logger.LogInfo("Start || GetChargeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02502ChargeDTO> loRtn = null;
            GetChargeListDTO loParam = new GetChargeListDTO();
            GSM02502ChargeCls loCls = new GSM02502ChargeCls();
            List<GSM02502ChargeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetChargeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetChargeList(Cls) || GetChargeList(Controller)");
                loTempRtn = loCls.GetChargeList(loParam);

                _logger.LogInfo("Run GetChargeStream(Controller) || GetChargeList(Controller)");
                loRtn = GetChargeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetChargeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02502ChargeDTO> GetChargeStream(List<GSM02502ChargeDTO> poParameter)
        {
            foreach (GSM02502ChargeDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02502ChargeParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02502ChargeCls loCls = new GSM02502ChargeCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CACTION = "DELETE";

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
        public R_ServiceGetRecordResultDTO<GSM02502ChargeParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02502ChargeParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02502ChargeParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02502ChargeParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02502ChargeCls loCls = new GSM02502ChargeCls();

                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceSaveResultDTO<GSM02502ChargeParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02502ChargeParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02502ChargeParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02502ChargeParameterDTO>();
            GSM02502ChargeCls loCls = new GSM02502ChargeCls();
            
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                //loParam.Data = poParameter.Entity;
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_CHARGE_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_UNIT_TYPE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM02502_CHARGE_UNIT_TYPE_CATEGORY_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
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
        public IAsyncEnumerable<GSM02502ChargeComboboxDTO> GetChargeComboBoxList()
        {
            using Activity activity = _activitySource.StartActivity("GetChargeComboBoxList");
            _logger.LogInfo("Start || GetChargeComboBoxList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02502ChargeComboboxDTO> loRtn = null;
            GSM02502ChargeComboboxParameterDTO loParam = new GSM02502ChargeComboboxParameterDTO();
            GSM02502ChargeCls loCls = new GSM02502ChargeCls();
            List<GSM02502ChargeComboboxDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetChargeComboBoxList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_CLASS_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_CHARGE_COMBOBOX_CLASS_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetChargeComboBoxList(Cls) || GetChargeComboBoxList(Controller)");
                loTempRtn = loCls.GetChargeComboBoxList(loParam);

                _logger.LogInfo("Run GetChargeComboBoxStream(Controller) || GetChargeComboBoxList(Controller)");
                loRtn = GetChargeComboBoxStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetChargeComboBoxList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02502ChargeComboboxDTO> GetChargeComboBoxStream(List<GSM02502ChargeComboboxDTO> poParameter)
        {
            foreach (GSM02502ChargeComboboxDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
