using PMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01530ViewModel : R_ViewModel<PMM01530DTO>
    {
        private PMM01530Model _PMM01530Model = new PMM01530Model();

        public ObservableCollection<PMM01530DTO> OtherChargesGrid { get; set; } = new ObservableCollection<PMM01530DTO>();

        public PMM01530DTO OtherCharges = new PMM01530DTO();

        public string PropertyValueContext = "";
        public string InvGrpCode { get; set; } = "";
        public string InvGrpName { get; set; } = "";

        public async Task GetOtherChargesList(PMM01530DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01530Model.GetAllOtherChargerListAsync(poParam.CPROPERTY_ID, poParam.CINVGRP_CODE);

                OtherChargesGrid = new ObservableCollection<PMM01530DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetOtherCharges(PMM01530DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyValueContext;
                poParam.CINVGRP_CODE = InvGrpCode;
                var loResult = await _PMM01530Model.R_ServiceGetRecordAsync(poParam);
                loResult.CINVGRP_NAME = InvGrpName;

                OtherCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveOtherCharges(PMM01530DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CPROPERTY_ID = PropertyValueContext;
                    poNewEntity.CINVGRP_CODE = InvGrpCode;
                }
                var loResult = await _PMM01530Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                OtherCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteOtherCharges(PMM01530DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMM01530Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
