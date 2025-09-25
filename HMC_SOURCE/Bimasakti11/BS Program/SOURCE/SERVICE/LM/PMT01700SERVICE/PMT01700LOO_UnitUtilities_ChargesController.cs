using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01700BACK;
using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges;
using PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using PMT01700COMMON.Interface;
using PMT01700COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01700LOO_UnitUtilities_ChargesController : ControllerBase, IPMT01700LOO_UnitUtilities_Charges
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;
        public PMT01700LOO_UnitUtilities_ChargesController(ILogger<PMT01700LOO_UnitUtilities_ChargesController> logger)
        {
            //Initial and Get Logger
            LoggerPMT01700.R_InitializeLogger(logger);
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_InitializeAndGetActivitySource(nameof(PMT01700LOO_UnitUtilities_ChargesController));

        }
        [HttpPost]
        public IAsyncEnumerable<PMT01700LOO_UnitCharges_ChargesListDTO> GetChargesList()
        {
            string lcMethodName = nameof(GetChargesList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700LOO_UnitCharges_ChargesListDTO> loRtn = null;
            List<PMT01700LOO_UnitCharges_ChargesListDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700LOO_UnitUtilities_ParameterDTO();
                var loCls = new PMT01700LOO_UnitUtilities_ChargesCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CPROPERTY_ID);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CDEPT_CODE);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CTRANS_CODE);
                loDbParameter.CCHARGE_MODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CCHARGE_MODE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CREF_NO);
                loDbParameter.COTHER_UNIT_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CUNIT_ID);
                loDbParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CFLOOR_ID);
                loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CBUILDING_ID);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));

                loRtnTemp = loCls.GetChargesListDb(loDbParameter);
                loUtilities = new();
                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
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
        public IAsyncEnumerable<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> GetDetailItemList()
        {
            string lcMethodName = nameof(GetDetailItemList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> loRtn = null;
            List<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700LOO_UnitUtilities_ParameterDTO();
                var loCls = new PMT01700LOO_UnitUtilities_ChargesCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CPROPERTY_ID);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CDEPT_CODE);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CTRANS_CODE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CREF_NO);
                loDbParameter.CSEQ_NO = R_Utility.R_GetStreamingContext<string>(PMT01700ContextDTO.CSEQ_NO);

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));

                loRtnTemp = loCls.GetChargesItemListDb(loDbParameter);
                loUtilities = new();
                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
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
        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetFeeMethodList()
        {
            string lcMethodName = nameof(GetFeeMethodList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700ComboBoxDTO> loRtn = null;
            List<PMT01700ComboBoxDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700BaseParameterDTO();
                var loCls = new PMT01700LOO_UnitUtilities_ChargesCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANGUAGE = R_BackGlobalVar.CULTURE;

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetFeeMethodListDb(loDbParameter);

                loUtilities = new();

                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
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
        public IAsyncEnumerable<PMT01700ComboBoxDTO> GetPeriodModeList()
        {
            string lcMethodName = nameof(GetPeriodModeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01700ComboBoxDTO> loRtn = null;
            List<PMT01700ComboBoxDTO> loRtnTemp;
            PMT01700Utilities? loUtilities = null;
            try
            {
                var loDbParameter = new PMT01700BaseParameterDTO();
                var loCls = new PMT01700LOO_UnitUtilities_ChargesCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANGUAGE = R_BackGlobalVar.CULTURE;

                _logger.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _logger.LogInfo(string.Format("Call method {0}", lcMethodName));
                loRtnTemp = loCls.GetPeriodeModeListDb(loDbParameter);

                loUtilities = new();

                _logger.LogInfo("Call method to streaming data");
                loRtn = loUtilities.PMT01700GetListStream(loRtnTemp);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            PMT01700LOO_UnitUtilities_ChargesCls loCls;
            try
            {
                loCls = new PMT01700LOO_UnitUtilities_ChargesCls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Call method R_Delete");
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
        public R_ServiceGetRecordResultDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO>();

            try
            {
                var loCls = new PMT01700LOO_UnitUtilities_ChargesCls();
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
        public R_ServiceSaveResultDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO>? loRtn = null;
            PMT01700LOO_UnitUtilities_ChargesCls loCls;

            try
            {
                loCls = new PMT01700LOO_UnitUtilities_ChargesCls();
                loRtn = new R_ServiceSaveResultDTO<PMT01700LOO_UnitCharges_ChargesDetailDTO>();
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
    }
}
