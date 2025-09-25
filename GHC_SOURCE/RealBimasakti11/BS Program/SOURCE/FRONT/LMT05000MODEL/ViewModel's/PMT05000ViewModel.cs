using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;
using PMT05000FrontResources;
using System.Linq;
using R_BlazorFrontEnd;
using System.Globalization;
using System.Security.Cryptography;
using R_BlazorFrontEnd.Helpers;

namespace PMT05000MODEL.ViewModel_s
{
    public class PMT05000ViewModel
    {
        private PMT05000Model _initModel = new PMT05000Model();

        private PMT05001Model _model = new PMT05001Model();

        public ObservableCollection<PropertyDTO> _PropertyList { get; set; } = new ObservableCollection<PropertyDTO>();

        public ObservableCollection<GSB_CodeInfoDTO> _ChargeTypeList { get; set; } = new ObservableCollection<GSB_CodeInfoDTO>();

        public ObservableCollection<GSPeriodDT_DTO> _MonthPeriodList { get; set; } = new ObservableCollection<GSPeriodDT_DTO>();

        public ObservableCollection<AgreementTypeDTO> _AgreementTypeList { get; set; } = new ObservableCollection<AgreementTypeDTO>();

        public ObservableCollection<AgreementChrgDiscDetailDTO> _AgreementChrgDiscDetailList { get; set; } = new ObservableCollection<AgreementChrgDiscDetailDTO>();

        public AgreementChrgDiscParamDTO _AgreementChrgDiscProcessParam { get; set; } = new AgreementChrgDiscParamDTO();

        public AgreementChrgDiscListParamDTO _AgreementChrgDiscListParam { get; set; }

        public string _monthPeriod = "";

        public int _yearPeriod = DateTime.Now.Year;

        public string _discountName = "", _chargesName = "";

        #region init

        public async Task InitAsync(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _PropertyList = new ObservableCollection<PropertyDTO>(await GetPropertyList());
                _ChargeTypeList = new ObservableCollection<GSB_CodeInfoDTO>(await GetGSBCodeList());
                _MonthPeriodList = new ObservableCollection<GSPeriodDT_DTO>(await GetPeriodDTList());
                _AgreementTypeList = new ObservableCollection<AgreementTypeDTO>
                {
                    new AgreementTypeDTO{CAGREEMENT_TYPE="A", CAGREEMENT_TYPE_DISPLAY=poParamLocalizer["_labelAgreementTypeAll"]},
                    new AgreementTypeDTO{CAGREEMENT_TYPE="O", CAGREEMENT_TYPE_DISPLAY=poParamLocalizer["_labelAgreementTypeOwner"] },
                    new AgreementTypeDTO{CAGREEMENT_TYPE="L", CAGREEMENT_TYPE_DISPLAY=poParamLocalizer["_labelAgreementTypeLease"]},
                };

                //set default
                _AgreementChrgDiscProcessParam.CPROPERTY_ID = _PropertyList.Count > 0 ? _PropertyList.FirstOrDefault().CPROPERTY_ID : "";
                _AgreementChrgDiscProcessParam.CCHARGES_ID = _ChargeTypeList.Count > 0 ? _ChargeTypeList.FirstOrDefault().CCODE : "";
                _monthPeriod = _MonthPeriodList.Count > 0 ? DateTime.Now.ToString("MM") : "";
                _AgreementChrgDiscProcessParam.CAGREEMENT_TYPE = _AgreementTypeList.FirstOrDefault().CAGREEMENT_TYPE;
                _AgreementChrgDiscProcessParam.CCHARGES_TYPE = "";
                _AgreementChrgDiscProcessParam.LALL_BUILDING = true;
                _AgreementChrgDiscProcessParam.CBUILDING_ID = "";
                _AgreementChrgDiscProcessParam.CBUILDING_NAME = "";
                _AgreementChrgDiscProcessParam.CCHARGES_ID = "";
                _AgreementChrgDiscProcessParam.CDISCOUNT_CODE = "";
                _AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = "";

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<PropertyDTO>> GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = new List<PropertyDTO>();
            try
            {
                loRtn = new List<PropertyDTO>();
                loRtn = await _initModel.GetPropertyListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<List<GSB_CodeInfoDTO>> GetGSBCodeList()
        {
            R_Exception loEx = new R_Exception();
            List<GSB_CodeInfoDTO> loRtn = null;
            try
            {
                loRtn = await _initModel.GetGSBCodeInfoAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<List<GSPeriodDT_DTO>> GetPeriodDTList()
        {
            R_Exception loEx = new R_Exception();
            List<GSPeriodDT_DTO> loRtn = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CYEAR, _yearPeriod.ToString());
                loRtn = await _initModel.GetGSPeriodDTAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }

        #endregion

        public async Task GetAgreementChrgDiscListAsync(string pcCompanyId)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CPROPERTY_ID, _AgreementChrgDiscProcessParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CCHARGES_TYPE, _AgreementChrgDiscProcessParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CCHARGES_ID, _AgreementChrgDiscProcessParam.CCHARGES_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CDISCOUNT_CODE, _AgreementChrgDiscProcessParam.CDISCOUNT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CDISCOUNT_TYPE, _AgreementChrgDiscProcessParam.CDISCOUNT_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CINV_PRD, _yearPeriod.ToString() + _monthPeriod);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.LALL_BUILDING, _AgreementChrgDiscProcessParam.LALL_BUILDING);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CBUILDING_ID, _AgreementChrgDiscProcessParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CAGREEMENNT_TYPE, _AgreementChrgDiscProcessParam.CAGREEMENT_TYPE);

                var loRtn = await _model.GetAgreementChargesDiscountListAsync() ?? new List<AgreementChrgDiscDetailDTO>();

                foreach (var loData in loRtn)
                {
                    loData.CCOMPANY_ID = pcCompanyId;
                    loData.CPROPERTY_ID = _AgreementChrgDiscProcessParam.CPROPERTY_ID;
                    loData.CLINK_DEPT_CODE = loData.CDEPT_CODE;
                    loData.CLINK_REF_NO= loData.CREF_NO;
                    loData.CLINK_TRANS_CODE= loData.CTRANS_CODE;
                    loData.NCHARGES_AMOUNT= loData.NCHARGE_AMOUNT;
                    loData.NCHARGES_DISCOUNT = loData.NCHARGE_DISCOUNT;
                    loData.NNET_CHARGES = loData.NNET_CHARGE;
                    loData.DSTART_DATE = DateTime.TryParseExact(loData.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate) ? ldStartDate : null as DateTime?;
                    loData.DEND_DATE = DateTime.TryParseExact(loData.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate) ? ldEndDate : null as DateTime?;
                    loData.CALREADY_HAVE_DISCOUNT = loData.LALREADY_HAVE_DISCOUNT ? "*" : "";
                    loData.CFLOOR_DISPLAY = $"({loData.CBUILDING_NAME ?? ""}){loData.CFLOOR_NAME ?? ""}";
                }
                _AgreementChrgDiscDetailList = new ObservableCollection<AgreementChrgDiscDetailDTO>(loRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessAgreementChrgDiscAsync(string pcAction)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _AgreementChrgDiscProcessParam.CACTION = pcAction;
                var loSelectedDetail = R_FrontUtility.ConvertCollectionToCollection<AgreementChrgDiscDetailBulkProcessDTO>(_AgreementChrgDiscDetailList.Where(x => x.LSELECTED).ToList());
                _AgreementChrgDiscProcessParam.AgreementChrgDiscDetail = loSelectedDetail.ToList();

                var loParam = _AgreementChrgDiscProcessParam;
                loParam.CINV_PERIOD_YEAR = _yearPeriod.ToString();
                loParam.CINV_PERIOD_MONTH = _monthPeriod;
                await _model.ProcessAgreementChargeDiscountAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
