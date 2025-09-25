using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02630;
using PMT02600COMMON.DTOs.Helper;
using PMT02600COMMON.DTOs.PMT02610;
using PMT02600COMMON.DTOs;
using Lookup_GSModel;
using Lookup_GSCOMMON.DTOs;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02630ViewModel : R_ViewModel<PMT02630DTO>
    {
        private PMT02630Model loModel = new PMT02630Model();
        private PMT02610Model loAgreementModel = new PMT02610Model();
        private PublicLookupModel loGSLModel = new PublicLookupModel();

        #region Property Class
        public TabParameterDTO loTabParameter = new TabParameterDTO();
        public PMT02610DTO loHeader { get; set; } = new PMT02610DTO();
        public PMT02630DTO loDeposit { get; set; } = new PMT02630DTO();
        public ObservableCollection<PMT02630DTO> loDepositList { get; set; } = new ObservableCollection<PMT02630DTO>();
        public List<GSL00300DTO> VAR_CURRENCY_LIST = new List<GSL00300DTO>();
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await loGSLModel.GSL00300GetCurrencyListAsync();

                VAR_CURRENCY_LIST = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

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

        public async Task GetAgreementDepositList(PMT02630DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                var loResult = await loModel.GetAgreementDepositStreamAsync(poEntity);
                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CDEPOSIT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDepositDate))
                    {
                        x.DDEPOSIT_DATE = ldDepositDate;
                    }
                    else
                    {
                        x.DDEPOSIT_DATE = null;
                    }
                });

                loDepositList = new ObservableCollection<PMT02630DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementDeposit(PMT02630DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                var loResult = await loModel.R_ServiceGetRecordAsync(poEntity);
                if (DateTime.TryParseExact(loResult.CDEPOSIT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDepositDate))
                {
                    loResult.DDEPOSIT_DATE = ldDepositDate;
                }
                else
                {
                    loResult.DDEPOSIT_DATE = null;
                }

                loDeposit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveAgreementDeposit(PMT02630DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDEPOSIT_DATE = poEntity.DDEPOSIT_DATE.Value.ToString("yyyyMMdd");
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;

                var loResult = await loModel.R_ServiceSaveAsync(poEntity, poCRUDMode);

                if (DateTime.TryParseExact(loResult.CDEPOSIT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDepositDate))
                {
                    loResult.DDEPOSIT_DATE = ldDepositDate;
                }
                else
                {
                    loResult.DDEPOSIT_DATE = null;
                }

                loDeposit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteAgreementDeposit(PMT02630DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await loModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
