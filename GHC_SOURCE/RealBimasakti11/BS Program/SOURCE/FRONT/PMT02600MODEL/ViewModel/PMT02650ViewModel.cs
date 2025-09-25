using PMT02600COMMON.DTOs.PMT02610;
using PMT02600COMMON.DTOs.PMT02650;
using PMT02600COMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.Helper;
using R_BlazorFrontEnd.Helpers;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02650ViewModel : R_ViewModel<PMT02650DTO>
    {
        private PMT02650Model loModel = new PMT02650Model();
        private PMT02610Model loAgreementModel = new PMT02610Model();

        #region Property Class
        public TabParameterDTO loTabParameter = new TabParameterDTO();
        public PMT02610DTO loHeader { get; set; } = new PMT02610DTO();
        public PMT02650AmountDTO loSeqNoAmount { get; set; } = new PMT02650AmountDTO();
        public PMT02650ChargeDTO loCharge { get; set; } = new PMT02650ChargeDTO();
        public ObservableCollection<PMT02650ChargeDTO> loChargeList { get; set; } = new ObservableCollection<PMT02650ChargeDTO>();
        //public PMT02650DTO loInvoicePlan { get; set; } = new PMT02650DTO();
        public ObservableCollection<PMT02650DTO> loInvoicePlanList { get; set; } = new ObservableCollection<PMT02650DTO>();
        #endregion

        public async Task GetHeaderAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await loAgreementModel.R_ServiceGetRecordAsync(new PMT02610ParameterDTO()
                {
                    Data = new PMT02610DTO()
                    {
                        CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                        CDEPT_CODE = loTabParameter.CDEPT_CODE,
                        CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE,
                        CREF_NO = loTabParameter.CREF_NO
                    }
                });

                loResult.Data.DSTART_DATE = DateTime.ParseExact(loResult.Data.CSTART_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loResult.Data.DEND_DATE = DateTime.ParseExact(loResult.Data.CEND_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                loHeader = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementChargeList()
        {
            var loEx = new R_Exception();
            PMT02650ParameterDTO loParam = null;

            try
            {
                loParam = R_FrontUtility.ConvertObjectToObject<PMT02650ParameterDTO>(loHeader);
                loParam.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                var loResult = await loModel.GetAgreementChargeStreamAsync(loParam);

                loChargeList = new ObservableCollection<PMT02650ChargeDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementInvoicePlanList()
        {
            var loEx = new R_Exception();
            PMT02650ParameterDTO loParam = null;

            try
            {
                loParam = R_FrontUtility.ConvertObjectToObject<PMT02650ParameterDTO>(loHeader);
                loParam.CCHARGES_ID = loCharge.CCHARGES_ID;
                loParam.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                loParam.CREC_ID = loTabParameter.CREC_ID;
                var loResult = await loModel.GetAgreementInvoicePlanStreamAsync(loParam);
                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldInvoicePlanDate))
                    {
                        x.DSTART_DATE = ldInvoicePlanDate;
                    }
                    else
                    {
                        x.DSTART_DATE = null;
                    }
                    if (DateTime.TryParseExact(x.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldExpiredDate))
                    {
                        x.DEND_DATE = ldExpiredDate;
                    }
                    else
                    {
                        x.DEND_DATE = null;
                    }
                });

                loInvoicePlanList = new ObservableCollection<PMT02650DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
