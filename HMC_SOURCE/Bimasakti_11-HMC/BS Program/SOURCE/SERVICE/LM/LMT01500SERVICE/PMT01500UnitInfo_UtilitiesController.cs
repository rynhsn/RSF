using PMT01500Back;
using PMT01500Common.Context;
using PMT01500Common.DTO._3._Unit_Info;
using PMT01500Common.Interface;
using PMT01500Common.Logs;
using PMT01500Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT01500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01500UnitInfo_UtilitiesController : ControllerBase, IPMT01500UnitInfo_Utilities
    {
        private readonly LoggerPMT01500? _loggerPMT01500;
        private readonly ActivitySource _activitySource;

        public PMT01500UnitInfo_UtilitiesController(ILogger<PMT01500UnitInfo_UtilitiesController> logger)
        {
            LoggerPMT01500.R_InitializeLogger(logger);
            _loggerPMT01500 = LoggerPMT01500.R_GetInstanceLogger();
            _activitySource = PMT01500Activity.R_InitializeAndGetActivitySource(nameof(PMT01500AgreementListController));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01500UnitInfoUnit_UtilitiesListDTO> GetUnitInfoList()
        {
            string? lcMethod = nameof(GetUnitInfoList);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01500UtilitiesParameterDTO? loDbParameterInternal;
            PMT01500GetUnitInfo_UtilitiesParameterDTO? loDbParameter;
            List<PMT01500UnitInfoUnit_UtilitiesListDTO> loRtnTmp;
            PMT01500UnitInfo_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01500UnitInfoUnit_UtilitiesListDTO>? loReturn = null;
            PMT01500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();

                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01500GetUnitInfo_UtilitiesParameterContextDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01500GetUnitInfo_UtilitiesParameterContextDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01500GetUnitInfo_UtilitiesParameterContextDTO.CREF_NO);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01500GetUnitInfo_UtilitiesParameterContextDTO.CTRANS_CODE);
                    loDbParameter.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(PMT01500GetUnitInfo_UtilitiesParameterContextDTO.CUNIT_ID);
                    loDbParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT01500GetUnitInfo_UtilitiesParameterContextDTO.CFLOOR_ID);
                    loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT01500GetUnitInfo_UtilitiesParameterContextDTO.CBUILDING_ID);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetUnitInfoListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01500GetListStream(loRtnTmp);
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE()
        {
            string? lcMethod = nameof(GetComboBoxDataCCHARGES_TYPE);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01500UtilitiesWithCultureIDParameterDTO? loDbParameterInternal;
            List<PMT01500ComboBoxDTO> loRtnTmp;
            PMT01500UnitInfo_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01500ComboBoxDTO>? loReturn = null;
            PMT01500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CULTURE_ID = R_BackGlobalVar.CULTURE;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCCHARGES_TYPEDb(poParameterInternal: loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01500GetListStream(loRtnTmp);
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01500ComboBoxStartInvoicePeriodYearDTO> GetComboBoxDataCSTART_INV_PRDYear()
        {
            string? lcMethod = nameof(GetComboBoxDataCSTART_INV_PRDYear);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            List<PMT01500ComboBoxStartInvoicePeriodYearDTO> loRtnTmp;
            PMT01500UnitInfo_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01500ComboBoxStartInvoicePeriodYearDTO>? loReturn = null;
            PMT01500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCSTART_INV_PRDYearDb(pcCCOMPANY_ID: R_BackGlobalVar.COMPANY_ID);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01500GetListStream(loRtnTmp);
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT01500UnitInfoUnit_UtilitiesDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01500UnitInfoUnit_UtilitiesDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT01500UnitInfoUnit_UtilitiesDetailDTO>();

            try
            {
                _loggerPMT01500.LogInfo(string.Format("Initialize the loCls in method {0}", lcMethod));
                var loCls = new PMT01500UnitInfo_UtilitiesCls();
                _loggerPMT01500.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(poParameter.Entity.CCOMPANY_ID))
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01500.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerPMT01500.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01500UnitInfoUnit_UtilitiesDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01500UnitInfoUnit_UtilitiesDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT01500UnitInfoUnit_UtilitiesDetailDTO> loReturn = new();
            try
            {
                _loggerPMT01500.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01500UnitInfo_UtilitiesCls? loCls = new();
                _loggerPMT01500.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01500.LogInfo(string.Format("Checking Data From Profile, and edit if Profile has empty string or null in method {0}", lcMethod));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01500UnitInfoUnit_UtilitiesDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceDeleteResultDTO? loReturn = new();
            try
            {
                _loggerPMT01500.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01500.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01500UnitInfo_UtilitiesCls? loCls = new();
                _loggerPMT01500.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Perform the delete operation using the R_Delete method of Cls in method {0}", lcMethod));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01500ComboBoxStartInvoicePeriodMonthDTO> GetComboBoxDataCSTART_INV_PRDMonth()
        {
            string? lcMethod = nameof(GetComboBoxDataCSTART_INV_PRDYear);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            List<PMT01500ComboBoxStartInvoicePeriodMonthDTO> loRtnTmp;
            PMT01500UnitInfo_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01500ComboBoxStartInvoicePeriodMonthDTO>? loReturn = null;
            PMT01500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCSTART_INV_PRDMonthDb(pcCCOMPANY_ID: R_BackGlobalVar.COMPANY_ID);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01500GetListStream(loRtnTmp);
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01500ComboBoxCMeterNoDTO> GetComboBoxDataCMETER_NO(PMT01500GetUnitInfo_UtilitiesCMeterNoParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetComboBoxDataCMETER_NO);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01500UtilitiesParameterDTO? loDbParameterInternal;
            PMT01500GetUnitInfo_UtilitiesCMeterNoParameterDTO? loDbParameter;
            List<PMT01500ComboBoxCMeterNoDTO> loRtnTmp;
            PMT01500UnitInfo_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01500ComboBoxCMeterNoDTO>? loReturn = null;
            PMT01500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();

                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter = poParameter;
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCMETER_NODb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01500GetListStream(loRtnTmp);
                _loggerPMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
