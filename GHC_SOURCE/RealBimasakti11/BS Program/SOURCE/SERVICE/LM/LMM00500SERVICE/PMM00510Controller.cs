using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM00500Back;
using PMM00500Common;
using PMM00500Common.DTOs;
using PMM00500Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMM00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM00510Controller : ControllerBase, IPMM00510
    {
        private LoggerPMM00500 _loggerPMM00510;
        private readonly ActivitySource _activitySource;

        public PMM00510Controller(ILogger<LoggerPMM00500> logger)
        {
            LoggerPMM00500.R_InitializeLogger(logger);
            _loggerPMM00510 = LoggerPMM00500.R_GetInstanceLogger();
            _activitySource = PMM00500Activity.R_InitializeAndGetActivitySource(nameof(PMM00510Controller));
        }

        #region CRUD

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM00510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM00510DTO> poParameter)
        {
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start R_ServiceGetRecord PMM00510");
            var loRtn = new R_ServiceGetRecordResultDTO<PMM00510DTO>();
            PMM00500ParameterDB loDbPar;
            try
            {
                var loCls = new PMM00510Cls();
                loDbPar = new PMM00500ParameterDB();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CCULTURE = R_BackGlobalVar.CULTURE;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.R_Display");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End R_ServiceGetRecord PMM00510");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM00510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM00510DTO> poParameter)
        {
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start R_ServiceSave PMM00510");
            var loRtn = new R_ServiceSaveResultDTO<PMM00510DTO>();
            PMM00500ParameterDB loDbPar;

            try
            {
                loDbPar = new PMM00500ParameterDB();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = poParameter.Entity.CPROPERTY_ID;
                loDbPar.CCHARGE_TYPE_ID = poParameter.Entity.CCHARGE_TYPE_ID;
                var loCls = new PMM00510Cls();

                _loggerPMM00510.LogDebug("Go To PMM00510Cls.R_Saving");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End R_ServiceSave PMM00510");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM00510DTO> poParameter)
        {
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start R_ServiceDelete PMM00510");
            var loRtn = new R_ServiceDeleteResultDTO();
            var loCls = new PMM00510Cls();
            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.R_Deleting");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End R_ServiceDelete PMM00510");
            return loRtn;
        }

        #endregion CRUD

        #region GetList

        [HttpPost]
        public IAsyncEnumerable<PropertyListStreamChargeDTO> GetPropertyListCharges()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetPropertyListCharges PMM00510");
            IAsyncEnumerable<PropertyListStreamChargeDTO> loRtn = null;
            PMM00510Cls loCls;
            PMM00500ParameterDB loDbPar;

            try
            {
                loDbPar = new PMM00500ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new PMM00510Cls();
                _loggerPMM00510.LogInfo("Go To PMM00510Cls.PropertyListDB");
                var loRtnTmp = loCls.PropertyListDB(loDbPar);
                _loggerPMM00510.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetPropertyStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End GetPropertyListCharges PMM00510");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM00500GridDTO> GetAllChargesList()
        {
            var loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetAllChargesList PMM00510");
            IAsyncEnumerable<PMM00500GridDTO> loRtn = null;
            PMM00500ParameterDB loDbPar;
            PMM00510Cls loCls;
            List<PMM00500GridDTO> loRtnTmp;
            try
            {
                loDbPar = new PMM00500ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbPar.CCHARGE_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_TYPE_ID);
                loCls = new PMM00510Cls();

                _loggerPMM00510.LogDebug("Go To PMM00510Cls.GetChargesList");
                loRtnTmp = loCls.GetChargesList(loDbPar);
                _loggerPMM00510.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetAllChargeListStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End GetAllChargesList PMM00510");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<ChargesTaxTypeDTO> GetChargesTaxType()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetChargesTaxType PMM00510");
            IAsyncEnumerable<ChargesTaxTypeDTO> loRtn = null;
            List<ChargesTaxTypeDTO> loRtnTmp;
            PMM00500ParameterDB loDbPar;
            PMM00510Cls loCls;

            try
            {
                loDbPar = new PMM00500ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CCULTURE = R_BackGlobalVar.CULTURE;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new PMM00510Cls();

                _loggerPMM00510.LogDebug("Go To PMM00510Cls.TaxTypeListDB");
                loRtnTmp = loCls.TaxTypeListDB(loDbPar);
                _loggerPMM00510.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetChargeTaxTypeStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End GetChargesTaxType PMM00510");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<ChargesTaxCodeDTO> GetChargesTaxCode()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetChargesTaxCode PMM00510");
            IAsyncEnumerable<ChargesTaxCodeDTO> loRtn = null;
            List<ChargesTaxCodeDTO> loRtnTmp;
            PMM00500ParameterDB loDbPar;
            PMM00510Cls loCls;
            try
            {
                loDbPar = new PMM00500ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CCULTURE = R_BackGlobalVar.CULTURE;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new PMM00510Cls();
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.TaxCodeListDB");
                loRtnTmp = loCls.TaxCodeListDB(loDbPar);
                _loggerPMM00510.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetChargeTaxCodeStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End GetChargesTaxCode PMM00510");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<AccurualDTO> GetAccrualList()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetAccrualList PMM00510");
            IAsyncEnumerable<AccurualDTO> loRtn = null;
            List<AccurualDTO> loRtnTmp;
            PMM00500ParameterDB loDbPar;
            PMM00510Cls loCls;
            try
            {
                loDbPar = new PMM00500ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CCULTURE = R_BackGlobalVar.CULTURE;
                loCls = new PMM00510Cls();
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.AccrualListDB");
                loRtnTmp = loCls.AccrualListDB(loDbPar);
                _loggerPMM00510.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetRecurringStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End UnitChargesPrint PMM00510");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMM00510DTO> UnitChargesPrint()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start UnitChargesPrint PMM00510");
            IAsyncEnumerable<PMM00510DTO> loRtn = null;
            List<PMM00510DTO> loRtnTmp;
            PrintParamDTO loDbPar;
            PMM00510Cls loCls;

            try
            {
                loDbPar = new PrintParamDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                loCls = new PMM00510Cls();
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.GetPrintParam");
                loRtnTmp = loCls.GetPrintParam(loDbPar);
                _loggerPMM00510.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetAllPrintParamListStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End UnitChargesPrint PMM00510");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<UnitChargesTypeDTO> GetUnitChargeType()
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start GetUnitChargeType PMM00510");
            IAsyncEnumerable<UnitChargesTypeDTO> loRtn = null;
            List<UnitChargesTypeDTO> loRtnTmp;
            PMM00500ParameterDB loDbPar;
            PMM00510Cls loCls;
            try
            {
                loDbPar = new PMM00500ParameterDB();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CCULTURE = R_BackGlobalVar.CULTURE;
                loCls = new PMM00510Cls();
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.UnitChargesTypeListDB");
                loRtnTmp = loCls.UnitChargesTypeListDB(loDbPar);
                _loggerPMM00510.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);
                loRtn = GetUnitChargesTypeStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End UnitChargesPrint PMM00510");
            return loRtn;
        }

        #endregion GetList

        [HttpPost]
        public ActiveInactiveDTO RSP_LM_ACTIVE_INACTIVE_Method(PMM00510DTO loDbData)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start RSP_LM_ACTIVE_INACTIVE_Method PMM00510");
            PMM00500ParameterDB loParam = null;
            ActiveInactiveDTO loRtn = null;
            PMM00510Cls loCls = null;
            try
            {
                loCls = new PMM00510Cls();
                loRtn = new ActiveInactiveDTO();
                loParam = new PMM00500ParameterDB();
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.RSP_LM_ACTIVE_INACTIVE_UnitChargesMethod");
                loCls.RSP_GS_ACTIVE_INACTIVE_UnitChargesMethod(loParam, loDbData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerPMM00510.LogInfo("End RSP_LM_ACTIVE_INACTIVE_Method PMM00510");
            return loRtn;
        }

        [HttpPost]
        public CopyNewProcessListDTO CopyNewProcess(PMM00510DTO poData)
        {
            R_Exception loEx = new R_Exception();
            _loggerPMM00510.LogInfo("Start CopyNewProcess PMM00510");
            PMM00500ParameterDB loParam = new PMM00500ParameterDB();
            CopyNewProcessListDTO loRtn = new CopyNewProcessListDTO();
            PMM00510Cls loCls = new PMM00510Cls();
            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMM00510.LogDebug("Go To PMM00510Cls.CopyNewProcess");
                loCls.CopyNewProcess(loParam, poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
            {
                _loggerPMM00510.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();

            _loggerPMM00510.LogInfo("End CopyNewProcess PMM00510");
            return loRtn;
        }

        #region Helper

        private async IAsyncEnumerable<PropertyListStreamChargeDTO> GetPropertyStream(List<PropertyListStreamChargeDTO> poParameter)
        {
            foreach (PropertyListStreamChargeDTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<ChargesTaxTypeDTO> GetChargeTaxTypeStream(List<ChargesTaxTypeDTO> poParameter)
        {
            foreach (ChargesTaxTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<ChargesTaxCodeDTO> GetChargeTaxCodeStream(List<ChargesTaxCodeDTO> poParameter)
        {
            foreach (ChargesTaxCodeDTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<AccurualDTO> GetRecurringStream(List<AccurualDTO> poParameter)
        {
            foreach (AccurualDTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<UnitChargesTypeDTO> GetUnitChargesTypeStream(List<UnitChargesTypeDTO> poParameter)
        {
            foreach (UnitChargesTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<PMM00500GridDTO> GetAllChargeListStream(List<PMM00500GridDTO> poParameter)
        {
            foreach (PMM00500GridDTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<PMM00510DTO> GetAllPrintParamListStream(List<PMM00510DTO> poParameter)
        {
            foreach (PMM00510DTO item in poParameter)
            {
                yield return item;
            }
        }

        #endregion Helper
    }
}