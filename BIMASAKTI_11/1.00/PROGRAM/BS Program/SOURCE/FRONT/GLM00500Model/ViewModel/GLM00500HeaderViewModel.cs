using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GLM00500Model.ViewModel
{
    public class GLM00500HeaderViewModel : R_ViewModel<GLM00500BudgetHDDTO>
    {
        private GLM00500HeaderModel _model = new();
        public ObservableCollection<GLM00500BudgetHDDTO> BudgetHDList = new();
        public ObservableCollection<GLM00500FunctionDTO> CurrencyTypeList = new();

        public GLM00500BudgetHDDTO BudgetHDEntity = new();
        public GLM00500GSMPeriodDTO Periods = new();
        public GLM00500GLSystemParamDTO SystemParams = new();
        
        public GLM00500FunctionDTO CurrencyType = new();
        
        public int SelectedYear = 0;
        public int DefaultYear = 0;

        public async Task Init()
        {
            // _model = new GLM00500HeaderModel();
            await GetPeriods();
            await GetSystemParams();
            await GetCurrencyTypeList();
        }
        //get list
        public async Task GetBudgetHDList(int pnYear)
        {
            var loEx = new R_Exception();
            GLM00500ListDTO<GLM00500BudgetHDDTO> loResult;

            try
            {
                R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CYEAR, pnYear.ToString());
                loResult = await _model.GLM00500GetBudgetHDListModel();
                BudgetHDList = new ObservableCollection<GLM00500BudgetHDDTO>(loResult.Data);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get budget header 
        public async Task GetBudgetHDEntity(GLM00500BudgetHDDTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00500BudgetHDDTO loResult;

            try
            {
                loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                BudgetHDEntity = loResult;
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        //save budget header
        public async Task SaveBudgetHDEntity(GLM00500BudgetHDDTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            GLM00500BudgetHDDTO loResult;

            try
            {
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
        public async Task DeleteBudgetHDEntity(GLM00500BudgetHDDTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00500BudgetHDDTO loResult;

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
            GLM00500GSMPeriodDTO loResult;

            try
            {
                loResult = await _model.GLM00500GetPeriodsModel();
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
        
    }
}