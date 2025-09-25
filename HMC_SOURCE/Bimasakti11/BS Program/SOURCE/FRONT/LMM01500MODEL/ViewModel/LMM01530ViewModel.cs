using LMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01530ViewModel : R_ViewModel<LMM01530DTO>
    {
        private LMM01530Model _LMM01530Model = new LMM01530Model();

        public ObservableCollection<LMM01530DTO> OtherChargesGrid { get; set; } = new ObservableCollection<LMM01530DTO>();

        public LMM01530DTO OtherCharges = new LMM01530DTO();

        public string PropertyValueContext = "";
        public string InvGrpCode { get; set; } = "";
        public string InvGrpName { get; set; } = "";

        public async Task GetOtherChargesList(LMM01530DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01530Model.GetAllOtherChargerListAsync(poParam.CPROPERTY_ID, poParam.CINVGRP_CODE);

                OtherChargesGrid = new ObservableCollection<LMM01530DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetOtherCharges(LMM01530DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = PropertyValueContext;
                poParam.CINVGRP_CODE = InvGrpCode;
                var loResult = await _LMM01530Model.R_ServiceGetRecordAsync(poParam);
                loResult.CINVGRP_NAME = InvGrpName;

                OtherCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveOtherCharges(LMM01530DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CPROPERTY_ID = PropertyValueContext;
                    poNewEntity.CINVGRP_CODE = InvGrpCode;
                }
                var loResult = await _LMM01530Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                OtherCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteOtherCharges(LMM01530DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01530Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
