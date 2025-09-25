using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using PMT01300COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01340ViewModel : R_ViewModel<PMT01340DTO>
    {
        private PMT01340Model _PMT01340Model = new PMT01340Model();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();

        #region Property Class
        public PMT01300DTO LOI { get; set; } = new PMT01300DTO();
        public PMT01340DTO LOI_Deposit { get; set; } = new PMT01340DTO();
        public ObservableCollection<PMT01340DTO> LOIDepositGrid { get; set; } = new ObservableCollection<PMT01340DTO>();
        public List<GSL00300DTO> VAR_CURRENCY_LIST = new List<GSL00300DTO>();
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PublicLookupModel.GSL00300GetCurrencyListAsync();
                
                VAR_CURRENCY_LIST = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIDepositList(PMT01340DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01340Model.GetLOIDepositStreamAsync(poEntity);
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

                LOIDepositGrid = new ObservableCollection<PMT01340DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIDeposit(PMT01340DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01340Model.R_ServiceGetRecordAsync(poEntity);
                if (DateTime.TryParseExact(loResult.CDEPOSIT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDepositDate))
                {
                    loResult.DDEPOSIT_DATE = ldDepositDate;
                }
                else
                {
                    loResult.DDEPOSIT_DATE = null;
                }

                LOI_Deposit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveLOIDeposit(PMT01340DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                poEntity.CCHARGE_MODE = LOI.CCHARGE_MODE;

                poEntity.CDEPOSIT_DATE = poEntity.DDEPOSIT_DATE.Value.ToString("yyyyMMdd");

                var loResult = await _PMT01340Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                if (DateTime.TryParseExact(loResult.CDEPOSIT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDepositDate))
                {
                    loResult.DDEPOSIT_DATE = ldDepositDate;
                }
                else
                {
                    loResult.DDEPOSIT_DATE = null;
                }

                LOI_Deposit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteLOIDeposit(PMT01340DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                poEntity.CCHARGE_MODE = LOI.CCHARGE_MODE;

                await _PMT01340Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
