using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01100Back;
using PMT01100Common.Context._1._Unit_List;
using PMT01100Common.Context._2._LOO._1._Offer_List;
using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using PMT01100Common.Interface;
using PMT01100Common.Logs;
using PMT01100Common.Utilities.Db;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01100Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT01100LOO_OfferListController : ControllerBase, IPMT01100LOO_OfferList
    {
        private readonly LoggerPMT01100? _loggerPMT01100;
        private readonly ActivitySource _activitySource;

        public PMT01100LOO_OfferListController(ILogger<PMT01100LOO_OfferListController> logger)
        {
            LoggerPMT01100.R_InitializeLogger(logger);
            _loggerPMT01100 = LoggerPMT01100.R_GetInstanceLogger();
            _activitySource = PMT01100Activity.R_InitializeAndGetActivitySource(nameof(PMT01100LOO_OfferListController));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01100LOO_OfferList_OfferListDTO> GetOfferList()
        {

            string? lcMethod = nameof(GetOfferList);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesParameterDTO? loDbParameterInternal;
            PMT01100UtilitiesParameterOfferListDTO loDbParameter;
            List<PMT01100LOO_OfferList_OfferListDTO> loRtnTmp;
            PMT01100LOO_OfferListCls loCls;
            IAsyncEnumerable<PMT01100LOO_OfferList_OfferListDTO>? loReturn = null;
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
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01100UtilitiesParameterOfferListContextDTO.CPROPERTY_ID);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01100UtilitiesParameterOfferListContextDTO.CTRANS_CODE);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@PMT01100UnitListCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetOfferListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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
        public IAsyncEnumerable<PMT01100LOO_OfferList_UnitListDTO> GetUnitList()
        {
            string? lcMethod = nameof(GetUnitList);
            _loggerPMT01100.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT01100UtilitiesParameterDTO? loDbParameterInternal;
            PMT01100UtilitiesParameterGetUnitListDTO loDbParameter;
            List<PMT01100LOO_OfferList_UnitListDTO> loRtnTmp;
            PMT01100LOO_OfferListCls loCls;
            IAsyncEnumerable<PMT01100LOO_OfferList_UnitListDTO>? loReturn = null;
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
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT01100UtilitiesParameterGetUnitListContextDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT01100UtilitiesParameterGetUnitListContextDTO.CDEPT_CODE);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT01100UtilitiesParameterGetUnitListContextDTO.CTRANS_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT01100UtilitiesParameterGetUnitListContextDTO.CREF_NO);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT01100.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT01100.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT01100.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT01100.LogDebug("{@PMT01100UnitListCls}", loCls);

                _loggerPMT01100.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetUnitListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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

    }
}
