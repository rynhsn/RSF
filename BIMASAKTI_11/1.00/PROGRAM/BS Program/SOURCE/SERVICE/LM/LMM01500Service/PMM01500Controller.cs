using PMM01500BACK;
using PMM01500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM01500Controller : ControllerBase, IPMM01500
    {
        private LoggerPMM01500 _Logger;
        private readonly ActivitySource _activitySource;

        public PMM01500Controller(ILogger<LoggerPMM01500> logger)
        {
            //Initial and Get Logger
            LoggerPMM01500.R_InitializeLogger(logger);
            _Logger = LoggerPMM01500.R_GetInstanceLogger();
            _activitySource = PMM01500ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01500Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01501DTO> GetInvoiceGrpList()
        {
            using Activity activity = _activitySource.StartActivity("GetInvoiceGrpList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01501DTO> loRtn = null;
            List<PMM01501DTO> loTempRtn = null;
            var loParameter = new PMM01501ParameterDTO();
            _Logger.LogInfo("Start GetInvoiceGrpList");

            try
            {
                var loCls = new PMM01500Cls();
                loTempRtn = new List<PMM01501DTO>();

                _Logger.LogInfo("Set Param GetInvoiceGrpList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetAllInvoiceGrp");
                loTempRtn = loCls.GetAllInvoiceGrp(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetInvoiceGrpList");
                loRtn = GetInvoiceGrpListStream<PMM01501DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInvoiceGrpList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetInvoiceGrpListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01500DTOPropety> GetProperty()
        {
            using Activity activity = _activitySource.StartActivity("GetProperty");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01500DTOPropety> loRtn = null;
            var loParameter = new PMM01500PropertyParameterDTO();
            _Logger.LogInfo("Start GetProperty");

            try
            {
                var loCls = new PMM01500Cls();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetProperty");
                loRtn = GetInvoiceGrpListStream<PMM01500DTOPropety>(loResult);
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
        public PMM01500SingleResult<PMM01500DTO> PMM01500ActiveInactive(PMM01500DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("PMM01500ActiveInactive");
            R_Exception loException = new R_Exception();
            PMM01500SingleResult<PMM01500DTO> loRtn = new PMM01500SingleResult<PMM01500DTO>();
            PMM01500Cls loCls = new PMM01500Cls();
            _Logger.LogInfo("Start PMM01500ActiveInactive");

            try
            {
                _Logger.LogInfo("Set Param PMM01500ActiveInactive");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method PMM01500ActiveInactive");
                loCls.PMM01500ActiveInactiveSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End PMM01500ActiveInactive");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01502DTO> PMM01500LookupBank()
        {
            using Activity activity = _activitySource.StartActivity("PMM01500LookupBank");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01502DTO> loRtn = null;
            var loParameter = new PMM01502DTO();
            _Logger.LogInfo("Start PMM01500LookupBank");

            try
            {
                var loCls = new PMM01500Cls();

                _Logger.LogInfo("Set Param PMM01500LookupBank");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _Logger.LogInfo("Call Back Method PMM01530LookupBank");
                var loResult = loCls.PMM01530LookupBank(loParameter);

                _Logger.LogInfo("Call Stream Method Data PMM01500LookupBank");
                loRtn = GetInvoiceGrpListStream<PMM01502DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End PMM01500LookupBank");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete PMM01500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete PMM01500");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01500Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMM01500Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete PMM01500");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01500DTO> R_ServiceGetRecord(
            R_ServiceGetRecordParameterDTO<PMM01500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMM01500DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01500DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMM01500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord PMM01500");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01500Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMM01500Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMM01500");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMM01500DTO> loRtn = new R_ServiceSaveResultDTO<PMM01500DTO>();
            _Logger.LogInfo("Start ServiceSave PMM01500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave PMM01500");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01500Cls();

                _Logger.LogInfo("Call Back Method R_Save PMM01500Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMM01500");

            return loRtn;
        }

        [HttpPost]
        public PMM01500SingleResult<bool> CheckDataTabTemplateBank(PMM01500DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("CheckDataTabTemplateBank");
            R_Exception loException = new R_Exception();
            PMM01500SingleResult<bool> loRtn = new PMM01500SingleResult<bool>();
            PMM01500Cls loCls = new PMM01500Cls();
            _Logger.LogInfo("Start CheckDataTabTemplateBank");

            try
            {
                _Logger.LogInfo("Set Param CheckDataTabTemplateBank");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _Logger.LogInfo("Call Back Method CheckDataTabTemplateBank");
                loRtn.Data = loCls.ValidateCheckDataTab2(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End CheckDataTabTemplateBank");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01500StampRateDTO> GetStampRateList()
        {
            using Activity activity = _activitySource.StartActivity("GetStampRateList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01500StampRateDTO> loRtn = null;
            _Logger.LogInfo("Start GetStampRateList");

            try
            {
                var loCls = new PMM01500Cls();

                _Logger.LogInfo("Set Param GetStampRateList");
                string lcPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetAllStampRate");
                var loResult = loCls.GetAllStampRate(lcPropertyId);

                _Logger.LogInfo("Call Stream Method Data GetStampRateList");
                loRtn = GetInvoiceGrpListStream<PMM01500StampRateDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetStampRateList");

            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<PMM01500DTOInvTemplate> GetInvoiceTemplate()
        {
            using Activity activity = _activitySource.StartActivity("GetInvoiceTemplate");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01500DTOInvTemplate> loRtn = null;
            _Logger.LogInfo("Start GetInvoiceTemplate");

            try
            {
                var loCls = new PMM01500Cls();

                _Logger.LogInfo("Set Param GetInvoiceTemplate");
                string lcPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetInvoiceTemplate");
                var loResult = loCls.GetInvoiceTemplate(lcPropertyId);

                _Logger.LogInfo("Call Stream Method Data GetInvoiceTemplate");
                loRtn = GetInvoiceGrpListStream<PMM01500DTOInvTemplate>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInvoiceTemplate");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01500UniversalDTO> GetAllUniversalList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllUniversalList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetAllUniversalList");

            try
            {
                _Logger.LogInfo("Set Param GetAllAgreementChargeCallUnitList");
                string lcParameter = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPARAMETER);

                _Logger.LogInfo("Call Back Method GetUniversalList");
                var loCls = new PMM01500Cls();
                var loTempRtn = loCls.GetUniversalList(lcParameter);

                _Logger.LogInfo("Call Stream Method Data GetAllUniversalList");
                loRtn = GetInvoiceGrpListStream<PMM01500UniversalDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllUniversalList");

            return loRtn;
        }
    }
}