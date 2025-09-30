using PMT00100COMMON.Booking;
using PMT00100COMMON.ChangeUnit;
using PMT00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT00100MODEL.ViewModel
{
    public class PMT00100ChangeUnitViewModel : R_ViewModel<PMT00100ChangeUnitDTO>
    {
        private PMT00100ChangeUnitModel _model = new PMT00100ChangeUnitModel();
        public PMT00100ChangeUnitDTO ChangeUnitValue = new PMT00100ChangeUnitDTO();
        public PMT00100ChangeUnitDTO ChangeUnitValueToSave = new PMT00100ChangeUnitDTO();
        public PMT00100ChangeUnitDTO Parameter = new PMT00100ChangeUnitDTO();

        public async Task<PMT00100ChangeUnitDTO> GetEntity(PMT00100ChangeUnitDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMT00100ChangeUnitDTO loResult = new PMT00100ChangeUnitDTO();
            try
            {
                loResult = await _model.GetAgreementUnitInfoDetailAsync(poEntity);
                ChangeUnitValue = loResult;
                ChangeUnitValue.CTENANT_ID = Parameter.CTENANT_ID;
                ChangeUnitValue.CTENANT_NAME = Parameter.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task ServiceSave(PMT00100ChangeUnitDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                //poNewEntity.CREF_DATE = ConvertDateTimeToStringFormat(poNewEntity.DREF_DATE);
                //poNewEntity.CHO_PLAN_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHO_PLAN_DATE);
                await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region Validation
        public void ValidationFieldEmpty(PMT00100ChangeUnitDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(poEntity.CBUILDING_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationBuilding");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CFLOOR_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationFloor");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CUNIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationUnit");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CBILLING_RULE_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationBillingRule");
                    loEx.Add(loErr);
                }
                if (poEntity.NACTUAL_PRICE < 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationActualPrice");
                    loEx.Add(loErr);
                }
                if (poEntity.NBOOKING_FEE < 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT00100_Class), "ValidationBookingFee");
                    loEx.Add(loErr);
                }            

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

    }
}
