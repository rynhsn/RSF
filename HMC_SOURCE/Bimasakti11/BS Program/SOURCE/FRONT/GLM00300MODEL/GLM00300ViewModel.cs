using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using GLM00300Common;
using GLM00300Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00300Model
{
    public class GLM00300ViewModel : R_ViewModel<GLM00300DTO>
    {
        private GLM00300Model _model = new GLM00300Model();

        public ObservableCollection<GLM00300DTO> BudgetWeightingList { get; set; } = new ObservableCollection<GLM00300DTO>();
        public ObservableCollection<CurrencyCodeDTO> CurrencyCode = new ObservableCollection<CurrencyCodeDTO>();
        public GLM00300DTO BudgetWeighting = new GLM00300DTO();

        //public CurrencyCodeDTO Currency = new CurrencyCodeDTO();
        public string CBASE_CURRENCY_CODE = "";
        public string CLOCAL_CURRENCY_CODE = "";

        public string CBW_NAME_DISPLAY = "";
        public DateTime GetTimeNow = DateTime.Now;
        
        public async Task GetBudgetWeightingNameList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetBudgetWeightingNameListAsync();
                var loResult = R_FrontUtility.ConvertCollectionToCollection<GLM00300DTO>(loReturn.Data);
                BudgetWeightingList = new ObservableCollection<GLM00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBudgetWeightingById(GLM00300DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                BudgetWeighting = loResult;
                BudgetWeighting.CBW_NAME_DISPLAY = CBW_NAME_DISPLAY;
                BudgetWeighting.CREC_ID = poEntity.CREC_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyCodeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetCurrencyCodeListAsync();
                CurrencyCode = new ObservableCollection<CurrencyCodeDTO>(loReturn.Data);
                loReturn.Data.ForEach(currencyCode =>
                {
                    CBASE_CURRENCY_CODE = currencyCode.CBASE_CURRENCY_CODE;
                    CLOCAL_CURRENCY_CODE = currencyCode.CLOCAL_CURRENCY_CODE;
                });

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        public async Task SaveBudgetWeighting(GLM00300DTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
                BudgetWeighting = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteBudgetWeighting(GLM00300DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
