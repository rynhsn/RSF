using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using PMT00500COMMON;
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

namespace PMT00500MODEL
{
    public class PMT00540ViewModel : R_ViewModel<PMT00540DTO>
    {
        private PMT00540Model _PMT00540Model = new PMT00540Model();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();

        #region Property Class
        public PMT00500DTO LOI { get; set; } = new PMT00500DTO();
        public PMT00540DTO LOI_Deposit { get; set; } = new PMT00540DTO();
        public ObservableCollection<PMT00540DTO> LOIDepositGrid { get; set; } = new ObservableCollection<PMT00540DTO>();
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
        public async Task GetLOIDepositList(PMT00540DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00540Model.GetLOIDepositStreamAsync(poEntity);
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

                LOIDepositGrid = new ObservableCollection<PMT00540DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIDeposit(PMT00540DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00540Model.R_ServiceGetRecordAsync(poEntity);
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
        public async Task SaveLOIDeposit(PMT00540DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                poEntity.CCHARGE_MODE = LOI.CCHARGE_MODE;

                poEntity.CDEPOSIT_DATE = poEntity.DDEPOSIT_DATE.Value.ToString("yyyyMMdd");

                var loResult = await _PMT00540Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

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
        public async Task DeleteLOIDeposit(PMT00540DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                poEntity.CCHARGE_MODE = LOI.CCHARGE_MODE;

                await _PMT00540Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
