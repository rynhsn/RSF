using PMT01500Back;
using PMT01500Common.Context;
using PMT01500Common.DTO._5._Invoice_Plan;
using PMT01500Common.Interface;
using PMT01500Common.Logs;
using PMT01500Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace PMT01500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01500InvoicePlanController : ControllerBase, IPMT01500InvoicePlan
    {
        private readonly LoggerPMT01500? _loggerPMT01500;
        private readonly ActivitySource _activitySource;

        public PMT01500InvoicePlanController(ILogger<PMT01500InvoicePlanController> logger)
        {
            LoggerPMT01500.R_InitializeLogger(logger);
            _loggerPMT01500 = LoggerPMT01500.R_GetInstanceLogger();
            _activitySource = PMT01500Activity.R_InitializeAndGetActivitySource(nameof(PMT01500AgreementListController));
        }

        [HttpPost]
        public PMT01500InvoicePlanHeaderDTO GetInvoicePlanHeader(PMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetInvoicePlanHeader);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01500UtilitiesParameterDTO? loDbParameterInternal;
            PMT01500GetHeaderParameterDTO? loDbParameter;
            PMT01500InvoicePlanHeaderDTO? loReturn = null;
            PMT01500InvoicePlanCls loCls;

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
                    loDbParameter = poParameter;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500InvoicePlanCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetInvoicePlanHeaderDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT01500.LogDebug("{@ObjectReturnTemporary}", loReturn);

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
        public IAsyncEnumerable<PMT01500InvoicePlanChargesListDTO> GetInvoicePlanChargeList()
        {
            string? lcMethod = nameof(GetInvoicePlanChargeList);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01500UtilitiesParameterDTO? loDbParameterInternal;
            PMT01500GetHeaderParameterDTO? loDbParameter;
            List<PMT01500InvoicePlanChargesListDTO> loRtnTmp;
            PMT01500InvoicePlanCls loCls;
            IAsyncEnumerable<PMT01500InvoicePlanChargesListDTO>? loReturn = null;
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
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CREF_NO);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CTRANS_CODE);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500InvoicePlanCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetInvoicePlanChargeListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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
        public IAsyncEnumerable<PMT01500InvoicePlanListDTO> GetInvoicePlanList()
        {
            string? lcMethod = nameof(GetInvoicePlanList);
            _loggerPMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01500UtilitiesParameterDTO? loDbParameterInternal;
            PMT01500GetHeaderParameterDTO? loDbParameter;
            List<PMT01500InvoicePlanListDTO> loRtnTmp;
            PMT01500InvoicePlanCls loCls;
            IAsyncEnumerable<PMT01500InvoicePlanListDTO>? loReturn = null;
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
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CREF_NO);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01500GetHeaderParameterContextConstantDTO.CTRANS_CODE);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01500.LogDebug("{@PMT01500InvoicePlanCls}", loCls);

                _loggerPMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetInvoicePlanListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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
