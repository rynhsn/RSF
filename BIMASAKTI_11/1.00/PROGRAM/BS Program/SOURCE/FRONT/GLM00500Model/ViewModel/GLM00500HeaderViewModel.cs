using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00500Model.ViewModel
{
    public class GLM00500HeaderViewModel : R_ViewModel<GLM00500BudgetHDDTO>
    {
        private GLM00500HeaderModel _model = new GLM00500HeaderModel();
        public ObservableCollection<GLM00500BudgetHDDTO> BudgetHDList = new ObservableCollection<GLM00500BudgetHDDTO>();
        public ObservableCollection<GLM00500FunctionDTO> CurrencyTypeList = new ObservableCollection<GLM00500FunctionDTO>();

        public GLM00500BudgetHDDTO BudgetHDEntity = new GLM00500BudgetHDDTO();
        public GLM00500GSMPeriodDTO Periods = new GLM00500GSMPeriodDTO();
        public GLM00500GLSystemParamDTO SystemParams = new GLM00500GLSystemParamDTO();

        public int SelectedYear;

        public async Task Init()
        {
            var loEx = new R_Exception();

            try
            {
                // _model = new GLM00500HeaderModel();
                await GetPeriods();
                await GetSystemParams();
                await GetCurrencyTypeList();
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get list
        public async Task GetBudgetHDList(int pnYear)
        {
            var loEx = new R_Exception();
            List<GLM00500BudgetHDDTO> loResult;

            try
            {
                R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CYEAR, pnYear.ToString());
                loResult = await _model.GLM00500GetBudgetHDListStreamModel();
                BudgetHDList = new ObservableCollection<GLM00500BudgetHDDTO>(loResult);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get budget header 
        public async Task GetBudgetHD(GLM00500BudgetHDDTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00500BudgetHDDTO loResult;

            try
            {
                loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                loResult.CREC_ID = poEntity.CREC_ID;
                loResult.LALLOW_FINAL = poEntity.LALLOW_FINAL;
                BudgetHDEntity = loResult;
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //save budget header
        public async Task SaveBudgetHD(GLM00500BudgetHDDTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            GLM00500BudgetHDDTO loResult;

            try
            {
                poEntity.CYEAR = R_FrontUtility.ConvertObjectToObject<string>(SelectedYear);
                loResult = await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
                BudgetHDEntity = loResult;
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //delete budget header
        public async Task DeleteBudgetHD(GLM00500BudgetHDDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get period list
        public async Task GetPeriods()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GLM00500GetPeriodsModel();
                Periods = loResult;
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get currency type list
        public async Task GetCurrencyTypeList()
        {
            var loEx = new R_Exception();
            GLM00500ListDTO<GLM00500FunctionDTO> loResult;

            try
            {
                loResult = await _model.GLM00500GetCurrencyTypeListModel();
                CurrencyTypeList = new ObservableCollection<GLM00500FunctionDTO>(loResult.Data);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get system param
        public async Task GetSystemParams()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GLM00500GetSystemParamModel();
                SystemParams = loResult;
                SelectedYear = int.Parse(SystemParams.CSOFT_PERIOD_YY);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //set finalize budget
        public async Task<bool> SetFinalizeBudget(string pcRecId)
        {
            var loEx = new R_Exception();
            bool loReturn = false;

            try
            {
                var loParams = new GLM00500CrecParamsDTO{CREC_ID = pcRecId};
                var loResult = await _model.GLM00500FinalizeBudgetModel(loParams);
                loReturn = loResult.LRESULT;
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task<GLM00500AccountBudgetExcelDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GLM00500AccountBudgetExcelDTO loResult = null;

            try
            {
                loResult = await _model.GLM00500DownloadTemplateFileModel();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}