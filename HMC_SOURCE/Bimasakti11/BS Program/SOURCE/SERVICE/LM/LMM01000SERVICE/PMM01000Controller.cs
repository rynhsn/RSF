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
    public class PMM01000Controller : ControllerBase, IPMM01000
    {
        private LoggerPMM01000 _PMM01000logger;
        private readonly ActivitySource _activitySource;

        public PMM01000Controller(ILogger<LoggerPMM01000> logger)
        {
            //Initial and Get Logger
            LoggerPMM01000.R_InitializeLogger(logger);
            _PMM01000logger = LoggerPMM01000.R_GetInstanceLogger();
            _activitySource = PMM01000ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01000Controller));
        }

        private async IAsyncEnumerable<T> GetChargesUtilityListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01002DTO> GetChargesUtilityList()
        {
            using Activity activity = _activitySource.StartActivity("GetChargesUtilityList");
            var loEx = new R_Exception();
            _PMM01000logger.LogInfo("Start GetChargesUtilityList");

            IAsyncEnumerable<PMM01002DTO> loRtn = null;
            var loParameter = new PMM01002DTO();
            List<PMM01002DTO> loTempRtn = null;

            try
            {
                var loCls = new PMM01000Cls();
                loTempRtn = new List<PMM01002DTO>();

                //Set Param
                _PMM01000logger.LogInfo("Set Param GetChargesUtilityList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _PMM01000logger.LogInfo("Call Back Method GetAllChargesUtility");
                loTempRtn = loCls.GetAllChargesUtility(loParameter);

                _PMM01000logger.LogInfo("Call Stream Method For Stream Data GetChargesUtilityList");
                loRtn = GetChargesUtilityListStream<PMM01002DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _PMM01000logger.LogInfo("End GetChargesUtilityList");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM01000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            _PMM01000logger.LogInfo("Start ServiceGetRecord PMM01000");

            R_ServiceGetRecordResultDTO<PMM01000DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01000DTO>();

            try
            {
                _PMM01000logger.LogInfo("Set Param Entity ServiceGetRecord PMM01000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01000Cls();

                _PMM01000logger.LogInfo("Call Back Method R_GetRecord PMM01000Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _PMM01000logger.LogInfo("End R_GetRecord PMM01000");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            _PMM01000logger.LogInfo("Start ServiceSave PMM01000");

            R_ServiceSaveResultDTO<PMM01000DTO> loRtn = new R_ServiceSaveResultDTO<PMM01000DTO>();

            try
            {
                _PMM01000logger.LogInfo("Set Param Entity ServiceSave PMM01000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01000Cls();

                _PMM01000logger.LogInfo("Call Back Method R_Save PMM01000Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _PMM01000logger.LogInfo("End ServiceSave PMM01000");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            _PMM01000logger.LogInfo("Start ServiceDelete PMM01000");

            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                _PMM01000logger.LogInfo("Set Param Entity ServiceDelete PMM01000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01000Cls();

                _PMM01000logger.LogInfo("Call Back Method R_Delete PMM01000Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _PMM01000logger.LogInfo("End ServiceDelete PMM01000");

            return loRtn;
        }

        [HttpPost]
        public PMM01000DTO PMM01000ActiveInactive(PMM01000DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("PMM01000ActiveInactive");
            R_Exception loException = new R_Exception();
            _PMM01000logger.LogInfo("Start PMM01000ActiveInactive");

            PMM01000DTO loRtn = new PMM01000DTO();

            try
            {
                PMM01000Cls loCls = new PMM01000Cls();

                _PMM01000logger.LogInfo("Set Param PMM01000ActiveInactive");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _PMM01000logger.LogInfo("Call Back Method PMM01000ChangeStatusSP");
                loCls.PMM01000ChangeStatusSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _PMM01000logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _PMM01000logger.LogInfo("End PMM01000ActiveInactive");

            return loRtn;
        }

        [HttpPost]
        public PMM01003DTO PMM01000CopyNewCharges(PMM01003DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("PMM01000CopyNewCharges");
            R_Exception loException = new R_Exception();
            _PMM01000logger.LogInfo("Start PMM01000CopyNewCharges");

            PMM01003DTO loRtn = new PMM01003DTO();

            try
            {
                PMM01000Cls loCls = new PMM01000Cls();

                _PMM01000logger.LogInfo("Set Param PMM01000CopyNewCharges");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _PMM01000logger.LogInfo("Set Param PMM01000CopySource");
                loCls.PMM01000CopySource(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _PMM01000logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _PMM01000logger.LogInfo("End PMM01000CopyNewCharges");

            return loRtn;
        }

        [HttpPost]
        public PMM01000Record<PMM01000BeforeDeleteDTO> ValidateBeforeDelete(PMM01000DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("ValidateBeforeDelete");
            R_Exception loException = new R_Exception();
            _PMM01000logger.LogInfo("Start ValidateBeforeDelete");
            PMM01000Record<PMM01000BeforeDeleteDTO> loRtn = new PMM01000Record<PMM01000BeforeDeleteDTO>();

            try
            {
                PMM01000Cls loCls = new PMM01000Cls();

                _PMM01000logger.LogInfo("Set Param ValidateBeforeDelete");
                loRtn.Data =  loCls.ValidateBeforeDelete(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _PMM01000logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _PMM01000logger.LogInfo("End ValidateBeforeDelete");

            return loRtn;
        }
    }
}