using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02500Back;
using PMT02500Common.Context;
using PMT02500Common.DTO._4._Charges_Info.Db;
using PMT02500Common.DTO._4._Charges_Info.Revenue_Sharing_Process;
using PMT02500Common.Interface;
using PMT02500Common.Logs;
using PMT02500Common.Utilities;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT02500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT02500UnitCharges_Charges_RevenueMinimumRentController : ControllerBase, IPMT02500UnitCharges_Charges_RevenuMinimunRent
    {
        private readonly LoggerPMT02500? _loggerPMT02500;
        private readonly ActivitySource _activitySource;


        public PMT02500UnitCharges_Charges_RevenueMinimumRentController(ILogger<PMT02500UnitCharges_Charges_RevenueMinimumRentController> logger)
        {
            LoggerPMT02500.R_InitializeLogger(logger);
            _loggerPMT02500 = LoggerPMT02500.R_GetInstanceLogger();
            _activitySource = PMT02500Activity.R_InitializeAndGetActivitySource(nameof(PMT02500UnitCharges_Charges_RevenueMinimumRentController));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> GetRevenueMinimumRentList()
        {
            string? lcMethod = nameof(GetRevenueMinimumRentList);
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            PMT02500UtilitiesParameterDTO? loDbParameterInternal;
            PMT02500UtilitiesParameterChargesInfo_RevenueSharingDTO? loDbParameter;
            List<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> loRtnTmp;
            PMT02500UnitCharges_Charges_RevenueMinimumRentCls loCls;
            IAsyncEnumerable<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO>? loReturn = null;
            PMT02500Utilities? loUtilities = null;

            try
            {
                _loggerPMT02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();
                _loggerPMT02500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT02500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerPMT02500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementChargesInfo_RevenueMinimumRentDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementChargesInfo_RevenueMinimumRentDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementChargesInfo_RevenueMinimumRentDTO.CREF_NO);
                    loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementChargesInfo_RevenueMinimumRentDTO.CTRANS_CODE);
                    loDbParameter.CCHARGE_SEQ_NO = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementChargesInfo_RevenueMinimumRentDTO.CCHARGE_SEQ_NO);
                    loDbParameter.CREVENUE_SHARING_ID = R_Utility.R_GetStreamingContext<string>(PMT02500GetAgreementChargesInfo_RevenueMinimumRentDTO.CREVENUE_SHARING_ID);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerPMT02500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerPMT02500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerPMT02500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerPMT02500.LogDebug("{@PMT01500ChargesInfo_RevenueSharingCls}", loCls);

                _loggerPMT02500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetRevenueMinimumRentListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerPMT02500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                //loReturn = await LMM01500UtilitiesController.LMM01500GetListStream(loRtnTmp).ToListAsync();
                _loggerPMT02500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerPMT02500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.PMT02500GetListStream(loRtnTmp);
                _loggerPMT02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO>();

            try
            {
                _loggerPMT02500.LogInfo(string.Format("Initialize the loCls in method {0}", lcMethod));
                var loCls = new PMT02500UnitCharges_Charges_RevenueMinimumRentCls();
                _loggerPMT02500.LogDebug("{@PMT01500ChargesInfo_RevenueSharingCls}", loCls);

                _loggerPMT02500.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(poParameter.Entity.CCOMPANY_ID))
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT02500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT02500.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerPMT02500.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerPMT02500.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> loReturn = new();
            try
            {
                _loggerPMT02500.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT02500UnitCharges_Charges_RevenueMinimumRentCls? loCls = new();
                _loggerPMT02500.LogDebug("{@ObjectPMT01500ChargesInfo_RevenueSharingCls}", loCls);

                _loggerPMT02500.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT02500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT02500.LogInfo(string.Format("Checking Data From Profile, and edit if Profile has empty string or null in method {0}", lcMethod));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerPMT02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT02500.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT02500UnitCharges_Charges_RevenueMinimumRentDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethod)!;
            _loggerPMT02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceDeleteResultDTO? loReturn = new();
            try
            {
                _loggerPMT02500.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerPMT02500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerPMT02500.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                PMT02500UnitCharges_Charges_RevenueMinimumRentCls? loCls = new();
                _loggerPMT02500.LogDebug("{@ObjectPMT01500ChargesInfo_RevenueSharingCls}", loCls);

                _loggerPMT02500.LogInfo(string.Format("Perform the delete operation using the R_Delete method of Cls in method {0}", lcMethod));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerPMT02500.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerPMT02500.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }

    }

}
