using PMT01300BACK;
using PMT01300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMT01300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT01330PopupController : ControllerBase, IPMT01330Popup
    {
        private LoggerPMT01330Popup _Logger;
        private readonly ActivitySource _activitySource;
        public PMT01330PopupController(ILogger<LoggerPMT01330Popup> logger)
        {
            //Initial and Get Logger
            LoggerPMT01330Popup.R_InitializeLogger(logger);
            _Logger = LoggerPMT01330Popup.R_GetInstanceLogger();
            _activitySource = PMT01330PopupActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT01330PopupController));
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion

        [HttpPost]
        public IAsyncEnumerable<PMT01331DTO> GetLOIChargeRevenueHDListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIChargeRevenueHDListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01331DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIChargeRevenueHDListStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIChargeRevenueHDListStream");
                PMT01331DTO loEntity = new PMT01331DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CCHARGE_SEQ_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_SEQ_NO);

                _Logger.LogInfo("Call Back Method GetAllChargeRevenueHD");
                var loCls = new PMT01330PopupCls();
                var loTempRtn = loCls.GetAllChargeRevenueHD(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIChargeRevenueHDListStream");
                loRtn = GetStreamData<PMT01331DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIChargeRevenueHDListStream");

            return loRtn;
        }

        [HttpPost]
        public PMT01300SingleResult<PMT01331DTO> SaveDeleteLOIChargeRevenueHD(PMT01300SaveDTO<PMT01331DTO> poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveDeleteLOIChargeRevenueHD");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01331DTO> loRtn = new PMT01300SingleResult<PMT01331DTO>();
            _Logger.LogInfo("Start SaveDeleteLOIChargeRevenueHD");

            try
            {
                var loCls = new PMT01330PopupCls();

                _Logger.LogInfo("Call Back Method SaveDeleteLOIChargeRevenueHD");
                loCls.SaveDeleteChargeRevenueHD(poEntity.Data, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveDeleteLOIChargeRevenueHD");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01332DTO> GetLOIChargeRevenueListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIChargeRevenueListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01332DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIChargeRevenueListStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIChargeRevenueListStream");
                PMT01332DTO loEntity = new PMT01332DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CCHARGE_SEQ_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_SEQ_NO);
                loEntity.CREVENUE_SHARING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREVENUE_SHARING_ID);

                _Logger.LogInfo("Call Back Method GetAllChargeRevenue");
                var loCls = new PMT01330PopupCls();
                var loTempRtn = loCls.GetAllChargeRevenue(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetLOIChargeRevenueListStream");
                loRtn = GetStreamData<PMT01332DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIChargeRevenueListStream");

            return loRtn;
        }

        [HttpPost]
        public PMT01300SingleResult<PMT01332DTO> SaveDeleteLOIChargeRevenue(PMT01300SaveDTO<PMT01332DTO> poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveDeleteLOIChargeRevenue");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01332DTO> loRtn = new PMT01300SingleResult<PMT01332DTO>();
            _Logger.LogInfo("Start SaveDeleteLOIChargeRevenue");

            try
            {
                var loCls = new PMT01330PopupCls();

                _Logger.LogInfo("Call Back Method SaveDeleteLOIChargeRevenue");
                loRtn.Data = loCls.SaveDeleteChargeRevenue(poEntity.Data, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveDeleteLOIChargeRevenue");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<PMT01333DTO> GetRevenueMintRentListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetAllChargeRevenue");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01333DTO> loRtn = null;
            _Logger.LogInfo("Start GetAllChargeRevenue");

            try
            {
                _Logger.LogInfo("Set Param GetAllChargeRevenue");
                PMT01333DTO loEntity = new PMT01333DTO();
                loEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loEntity.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                loEntity.CCHARGE_SEQ_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGE_SEQ_NO);
                loEntity.CREVENUE_SHARING_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREVENUE_SHARING_ID);

                _Logger.LogInfo("Call Back Method GetAllLOICharges");
                var loCls = new PMT01330PopupCls();
                var loTempRtn = loCls.GetAllRevenueMintRent(loEntity);

                _Logger.LogInfo("Call Stream Method Data GetAllChargeRevenue");
                loRtn = GetStreamData<PMT01333DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllChargeRevenue");

            return loRtn;
        }

        [HttpPost]
        public PMT01300SingleResult<PMT01333DTO> SaveRevenueMintRent(PMT01300SaveDTO<PMT01333DTO> poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveRevenueMintRent");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01333DTO> loRtn = new PMT01300SingleResult<PMT01333DTO>();
            _Logger.LogInfo("Start SaveRevenueMintRent");

            try
            {
                var loCls = new PMT01330PopupCls();

                _Logger.LogInfo("Call Back Method SaveRevenueMintRent");
                loCls.SaveRevenueMintRent(poEntity.Data, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveRevenueMintRent");

            return loRtn;
        }
    }
}