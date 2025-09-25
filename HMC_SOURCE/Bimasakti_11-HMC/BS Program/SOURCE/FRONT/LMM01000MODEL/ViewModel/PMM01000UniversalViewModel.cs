using PMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01000UniversalViewModel : R_ViewModel<PMM01000UniversalDTO>
    {
        private PMM01000UniversalModel _PMM01000UniversalModel = new PMM01000UniversalModel();

        public List<PMM01000UniversalDTO> StatusList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> TaxExemptionList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> WithholdingTaxList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> UsageRateModeList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> RateTypeList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> AdminFeeTypeList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> AccrualMethodList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000DTOPropety> PropertyList { get; set; } = new List<PMM01000DTOPropety>();

        public async Task GetStatusList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetStatusListAsync();

                StatusList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxExemptionList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetTaxExemptionCodeListAsync();

                TaxExemptionList = new List<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetWithHoldingTaxList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetWithholdingTaxTypeListAsync();

                WithholdingTaxList = new List<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUsageRateModelList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetUsageRateModeListAsync();

                UsageRateModeList = new List<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRateTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetRateTypeListAsync();

                RateTypeList = new List<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAdminFeeTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetAdminFeeTypeListAsync();

                AdminFeeTypeList = new List<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAccrualMethodList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetAccrualMethodListAsync();

                AccrualMethodList = new List<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetPropertyAsync();
                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
