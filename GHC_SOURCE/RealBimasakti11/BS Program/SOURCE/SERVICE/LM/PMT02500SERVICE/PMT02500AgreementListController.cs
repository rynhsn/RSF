using PMT02500Back;
using PMT02500Common.Context;
using PMT02500Common.DTO._1._AgreementList;
using PMT02500Common.DTO._1._AgreementList.Upload;
using PMT02500Common.Interface;
using PMT02500Common.Logs;
using PMT02500Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using PMT02500Common.Utilities.Db;

namespace PMT02500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT02500AgreementListController : ControllerBase, IPMT02500AgreementList
    {
        private readonly LoggerPMT02500? _loggerPMT01500;
        private readonly ActivitySource _activitySource;

        public PMT02500AgreementListController(ILogger<PMT02500AgreementListController> logger)
        {
            LoggerPMT02500.R_InitializeLogger(logger);
            _loggerPMT01500 = LoggerPMT02500.R_GetInstanceLogger();
            _activitySource = PMT02500Activity.R_InitializeAndGetActivitySource(nameof(PMT02500AgreementListController));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02500AgreementListOriginalDTO> GetAgreementList()
        {
            string? lcMethod = nameof(GetAgreementList);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT02500UtilitiesParameterDTO? loDbParameterInternal;
            PMT02500UtilitiesParameterDbGetAgreementListDTO? loDbParameter;
            List<PMT02500AgreementListOriginalDTO> loRtnTmp;
            PMT02500AgreementListCls loCls;
            IAsyncEnumerable<PMT02500AgreementListOriginalDTO>? loReturn = null;
            PMT02500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new PMT02500UtilitiesParameterDbGetAgreementListDTO();
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementListContextDTO.CPROPERTY_ID);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementListContextDTO.CTRANS_CODE);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetAgreementListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT02500GetListStream(loRtnTmp);
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
        public IAsyncEnumerable<PMT02500ComboBoxDTO> GetComboBoxDataCTransStatus()
        {
            string? lcMethod = nameof(GetComboBoxDataCTransStatus);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT02500UtilitiesWithCultureIDParameterDTO? loDbParameterInternal;
            List<PMT02500ComboBoxDTO> loRtnTmp;
            PMT02500AgreementListCls loCls;
            IAsyncEnumerable<PMT02500ComboBoxDTO>? loReturn = null;
            PMT02500Utilities? loUtilities = null;

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
                _loggerPMT01500.LogDebug("{@PMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCTransStatusDb(poParameterInternal: loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT02500GetListStream(loRtnTmp);
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
        public IAsyncEnumerable<PMT02500PropertyListDTO> GetPropertyList()
        {
            string? lcMethod = nameof(GetPropertyList);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT02500UtilitiesParameterDTO? loDbParameterInternal;
            List<PMT02500PropertyListDTO> loRtnTmp;
            PMT02500AgreementListCls loCls;
            IAsyncEnumerable<PMT02500PropertyListDTO>? loReturn = null;
            PMT02500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerPMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetPropertyListDb(poParameterInternal: loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT02500GetListStream(loRtnTmp);
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

            return loReturn!;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02500VarGsmTransactionCodeDTO> GetVarGsmTransactionCode()
        {
            string? lcMethod = nameof(GetVarGsmTransactionCode);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            List<PMT02500VarGsmTransactionCodeDTO> loRtnTmp;
            PMT02500AgreementListCls loCls;
            IAsyncEnumerable<PMT02500VarGsmTransactionCodeDTO>? loReturn = null;
            PMT02500Utilities? loUtilities = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetVarGsmTransactionCodeDb(pcCCOMPANY_ID: R_BackGlobalVar.COMPANY_ID, pcCTRANS_CODE: "802030");
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT02500GetListStream(loRtnTmp);
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
        public PMT02500ChangeStatusDTO GetChangeStatus(PMT02500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetChangeStatus);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;

            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMT02500UtilitiesParameterDTO? loDbParameterInternal;
            PMT02500GetHeaderParameterDTO? loDbParameter;
            PMT02500AgreementListCls? loCls;
            PMT02500ChangeStatusDTO? loReturn = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerPMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                    loDbParameter.CPROPERTY_ID = poParameter.CPROPERTY_ID;
                    loDbParameter.CDEPT_CODE = poParameter.CDEPT_CODE;
                    loDbParameter.CREF_NO = poParameter.CREF_NO;
                    loDbParameter.CTRANS_CODE = poParameter.CTRANS_CODE;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetChangeStatusDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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
        public PMT02500ProcessResultDTO ProcessChangeStatus(PMT02500ChangeStatusParameterDTO poEntity)
        {
            string? lcMethod = nameof(ProcessChangeStatus);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            var loEx = new R_Exception();
            var loRtn = new PMT02500ProcessResultDTO();
            PMT02500ChangeStatusParameterDTO loDbPar = poEntity;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerPMT01500.LogInfo(string.Format("Initialize the loCls object as a new instance in method {0}", lcMethod));
                var loCls = new PMT02500AgreementListCls();
                _loggerPMT01500.LogDebug("{@ObjectPMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("ProcessChangeStatusDb method of PMT01500AgreementListCls in method {0}", lcMethod));
                loRtn = loCls.ProcessChangeStatusDb(poParameter: loDbPar);
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
        public PMT02500SelectedAgreementGetUnitDescriptionDTO GetSelectedAgreementGetUnitDescription(PMT02500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetSelectedAgreementGetUnitDescription);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;

            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            PMT02500UtilitiesParameterDTO? loDbParameterInternal;
            PMT02500GetHeaderParameterDTO? loDbParameter;
            PMT02500AgreementListCls? loCls;
            PMT02500SelectedAgreementGetUnitDescriptionDTO? loReturn = null;

            try
            {
                _loggerPMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerPMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                    loDbParameter.CPROPERTY_ID = poParameter.CPROPERTY_ID;
                    loDbParameter.CDEPT_CODE = poParameter.CDEPT_CODE;
                    loDbParameter.CREF_NO = poParameter.CREF_NO;
                    loDbParameter.CTRANS_CODE = poParameter.CTRANS_CODE;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetSelectedAgreementGetUnitDescriptionDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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
        public IAsyncEnumerable<PMT02500UnitListOriginalDTO> GetUnitList()
        {
            string? lcMethod = nameof(GetUnitList);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT02500UtilitiesParameterDTO? loDbParameterInternal;
            PMT02500GetHeaderParameterDTO? loDbParameter;
            List<PMT02500UnitListOriginalDTO> loRtnTmp;
            PMT02500AgreementListCls loCls;
            IAsyncEnumerable<PMT02500UnitListOriginalDTO>? loReturn = null;
            PMT02500Utilities? loUtilities = null;

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
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT02500GetHeaderParameterContextConstantDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT02500GetHeaderParameterContextConstantDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT02500GetHeaderParameterContextConstantDTO.CREF_NO);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT02500GetHeaderParameterContextConstantDTO.CTRANS_CODE);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500AgreementListCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetUnitListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerPMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT02500GetListStream(loRtnTmp);
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
        public PMT02500TemplateFileUploadDTO DownloadTemplateFile()
        {

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(nameof(DownloadTemplateFile));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            R_Exception? loEx = new R_Exception();
            PMT02500TemplateFileUploadDTO? loRtn = new PMT02500TemplateFileUploadDTO();

            try
            {
                Assembly? loAsm = Assembly.Load("BIMASAKTI_PM_API");

                var lcResourceFile = "BIMASAKTI_PM_API.Template.LeaseManager.xlsx";
                using (Stream? resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;

        }

    }
}
