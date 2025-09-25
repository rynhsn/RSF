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
using CBT02200COMMON.DTO.CBT02210;
using R_CommonFrontBackAPI;
using CBT02200BACK;
using R_BackEnd;
using R_Common;
using CBT02200COMMON.DTO.CBT02200;
using CBT02200COMMON.DTO;

namespace CBT02200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBT02210DetailController : ControllerBase, ICBT02210Detail
    {
        private LoggerCBT02210Detail _logger;
        private readonly ActivitySource _activitySource;
        public CBT02210DetailController(ILogger<CBT02210DetailController> logger)
        {
            LoggerCBT02210Detail.R_InitializeLogger(logger);
            _logger = LoggerCBT02210Detail.R_GetInstanceLogger();
            _activitySource = CBT02210DetailActivitySourceBase.R_InitializeAndGetActivitySource(nameof(CBT02210DetailController));
        }

        [HttpPost]
        public IAsyncEnumerable<CBT02210DetailDTO> GetChequeEntryDetailList()
        {
            using Activity activity = _activitySource.StartActivity("GetChequeEntryDetailList");
            _logger.LogInfo("Start || GetChequeEntryDetailList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<CBT02210DetailDTO> loRtn = null;
            GetCBT02210DetailParameterDTO loParam = new GetCBT02210DetailParameterDTO();
            CBT02210DetailCls loCls = new CBT02210DetailCls();
            List<CBT02210DetailDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetChequeEntryDetailList(Controller)");
                loParam.CCHEQUE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CBT02210_CHEQUE_LIST_DETAIL_REC_ID_STREAMING_CONTEXT);
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetChequeEntryDetailList(Cls) || GetChequeEntryDetailList(Controller)");
                loTempRtn = loCls.GetChequeEntryDetailList(loParam);

                _logger.LogInfo("Run GetChequeEntryDetailListStream(Controller) || GetChequeEntryDetailList(Controller)");
                loRtn = GetChequeEntryDetailListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetChequeEntryDetailList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<CBT02210DetailDTO> GetChequeEntryDetailListStream(List<CBT02210DetailDTO> poParameter)
        {
            foreach (CBT02210DetailDTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CBT02210DetailParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            CBT02210DetailCls loCls = new CBT02210DetailCls();
            CBT02210DetailParameterDTO loParam = new CBT02210DetailParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");

                loParam.Data = poParameter.Entity.Data;
                loParam.CTRANS_CODE = poParameter.Entity.CTRANS_CODE;
                loParam.CDEPT_CODE = poParameter.Entity.CDEPT_CODE;
                loParam.CCURRENCY_CODE = poParameter.Entity.CCURRENCY_CODE;
                loParam.CCHEQUE_ID = poParameter.Entity.CCHEQUE_ID;
                loParam.CREF_NO = poParameter.Entity.CREF_NO;
                loParam.CREF_DATE = poParameter.Entity.CREF_DATE;
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
        public R_ServiceGetRecordResultDTO<CBT02210DetailParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CBT02210DetailParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<CBT02210DetailParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<CBT02210DetailParameterDTO>();
            CBT02210DetailParameterDTO loParam = new CBT02210DetailParameterDTO();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                CBT02210DetailCls loCls = new CBT02210DetailCls();

                loParam.Data = poParameter.Entity.Data;
                loParam.CTRANS_CODE = poParameter.Entity.CTRANS_CODE;
                loParam.CDEPT_CODE = poParameter.Entity.CDEPT_CODE;
                loParam.CCURRENCY_CODE = poParameter.Entity.CCURRENCY_CODE;
                loParam.CCHEQUE_ID = poParameter.Entity.CCHEQUE_ID;
                loParam.CREF_NO = poParameter.Entity.CREF_NO;
                loParam.CREF_DATE = poParameter.Entity.CREF_DATE;
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
        public R_ServiceSaveResultDTO<CBT02210DetailParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<CBT02210DetailParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<CBT02210DetailParameterDTO> loRtn = new R_ServiceSaveResultDTO<CBT02210DetailParameterDTO>();
            CBT02210DetailCls loCls = new CBT02210DetailCls();
            CBT02210DetailParameterDTO loParam = new CBT02210DetailParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity.Data;
                loParam.CTRANS_CODE = poParameter.Entity.CTRANS_CODE;
                loParam.CDEPT_CODE = poParameter.Entity.CDEPT_CODE;
                loParam.CCURRENCY_CODE = poParameter.Entity.CCURRENCY_CODE;
                loParam.CCHEQUE_ID = poParameter.Entity.CCHEQUE_ID;
                loParam.CREF_NO = poParameter.Entity.CREF_NO;
                loParam.CREF_DATE = poParameter.Entity.CREF_DATE;
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
        public IAsyncEnumerable<GetCenterListDTO> GetCenterList()
        {
            using Activity activity = _activitySource.StartActivity("GetCenterList");
            _logger.LogInfo("Start || GetCenterList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetCenterListDTO> loRtn = null;
            GetCenterListParameterDTO loParam = new GetCenterListParameterDTO();
            CBT02210DetailCls loCls = new CBT02210DetailCls();
            List<GetCenterListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetCenterList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetCenterList(Cls) || GetCenterList(Controller)");
                loTempRtn = loCls.GetCenterList(loParam);

                _logger.LogInfo("Run GetCenterListStream(Controller) || GetCenterList(Controller)");
                loRtn = GetCenterListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCenterList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetCenterListDTO> GetCenterListStream(List<GetCenterListDTO> poParameter)
        {
            foreach (GetCenterListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
