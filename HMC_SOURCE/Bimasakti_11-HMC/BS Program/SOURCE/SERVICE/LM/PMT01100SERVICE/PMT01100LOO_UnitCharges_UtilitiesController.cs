using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01100Back;
using PMT01100Common.Context._2._LOO._3._Unit___Charges._2._Utilities;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._2._LOO___Unit___Charges___Utilities;
using PMT01100Common.Interface;
using PMT01100Common.Logs;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT01100Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01100LOO_UnitCharges_UtilitiesController : ControllerBase, IPMT01100LOO_UnitCharges_Utilities
    {

        private readonly LoggerPMT01100? _loggerPMT01100;
        private readonly ActivitySource _activitySource;

        public PMT01100LOO_UnitCharges_UtilitiesController(ILogger<PMT01100LOO_UnitCharges_UtilitiesController> logger)
        {
            LoggerPMT01100.R_InitializeLogger(logger);
            _loggerPMT01100 = LoggerPMT01100.R_GetInstanceLogger();
            _activitySource = PMT01100Activity.R_InitializeAndGetActivitySource(nameof(PMT01100LOO_UnitCharges_UtilitiesController));
        }


        [HttpPost]
        public IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE()
        {
            string? lcMethod = nameof(GetComboBoxDataCCHARGES_TYPE);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesWithCultureIDParameterDTO? loDbParameterInternal;
            List<PMT01100ComboBoxDTO> loRtnTmp;
            PMT01100LOO_UnitCharges_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01100ComboBoxDTO>? loReturn = null;
            PMT01100Utilities? loUtilities = null;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01100.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CULTURE_ID = R_BackGlobalVar.CULTURE;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@PMT01100AgreementCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCCHARGES_TYPEDb(poParameterInternal: loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01100.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01100.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01100GetListStream(loRtnTmp);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01100ResponseUtilitiesCMeterNoParameterDTO> GetComboBoxDataCMETER_NO(PMT01100RequestUtilitiesCMeterNoParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetComboBoxDataCMETER_NO);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesParameterDTO? loDbParameterInternal;
            PMT01100RequestUtilitiesCMeterNoParameterDTO? loDbParameter;
            List<PMT01100ResponseUtilitiesCMeterNoParameterDTO> loRtnTmp;
            PMT01100LOO_UnitCharges_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01100ResponseUtilitiesCMeterNoParameterDTO>? loReturn = null;
            PMT01100Utilities? loUtilities = null;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();

                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01100.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter = poParameter;
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCMETER_NODb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01100.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01100.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01100.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01100GetListStream(loRtnTmp);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> GetUtilitiesList()
        {
            string? lcMethod = nameof(GetUtilitiesList);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesParameterDTO? loDbParameterInternal;
            PMT01100UtilitiesParameterUtilitiesListDTO loDbParameter;
            List<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> loRtnTmp;
            PMT01100LOO_UnitCharges_UtilitiesCls loCls;
            IAsyncEnumerable<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>? loReturn = null;
            PMT01100Utilities? loUtilities = null;

            try
            {
                _loggerPMT01100.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerPMT01100.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01100ParameterUtilitiesListContextDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01100ParameterUtilitiesListContextDTO.CDEPT_CODE);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01100ParameterUtilitiesListContextDTO.CTRANS_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01100ParameterUtilitiesListContextDTO.CREF_NO);
                    loDbParameter.CUNIT_ID = R_Utility.R_GetStreamingContext<string>(PMT01100ParameterUtilitiesListContextDTO.CUNIT_ID);
                    loDbParameter.CFLOOR_ID = R_Utility.R_GetStreamingContext<string>(PMT01100ParameterUtilitiesListContextDTO.CFLOOR_ID);
                    loDbParameter.CBUILDING_ID = R_Utility.R_GetStreamingContext<string>(PMT01100ParameterUtilitiesListContextDTO.CBUILDING_ID);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetUtilitiesListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01100.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01100.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01100.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT01100GetListStream(loRtnTmp);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>();

            try
            {
                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls in method {0}", lcMethod));
                var loCls = new PMT01100LOO_UnitCharges_UtilitiesCls();
                _loggerPMT01100.LogDebug("{@PMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(poParameter.Entity.CCOMPANY_ID))
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);
                poParameter.Entity.CTRANS_CODE = !string.IsNullOrEmpty(poParameter.Entity.CTRANS_CODE) ? poParameter.Entity.CTRANS_CODE : "802041";

                _loggerPMT01100.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerPMT01100.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }
       
        [HttpPost]
        public R_ServiceSaveResultDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> loReturn = new();
            try
            {
                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01100LOO_UnitCharges_UtilitiesCls? loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01100.LogInfo(string.Format("Checking Data From Profile, and edit if Profile has empty string or null in method {0}", lcMethod));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerPMT01100.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceDeleteResultDTO? loReturn = new();
            try
            {
                _loggerPMT01100.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT01100.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT01100.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT01100LOO_UnitCharges_UtilitiesCls? loCls = new();
                _loggerPMT01100.LogDebug("{@ObjectPMT01500UnitInfo_UtilitiesCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Perform the delete operation using the R_Delete method of Cls in method {0}", lcMethod));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT01100.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT01100.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }
   
    }
}
