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
        private GLM00500HeaderModel _model = new();
        public ObservableCollection<GLM00500BudgetHDDTO> BudgetHDList = new();
        public ObservableCollection<GLM00500FunctionDTO> CurrencyTypeList = new();

        public GLM00500BudgetHDDTO BudgetHDEntity = new();
        public GLM00500GSMPeriodDTO Periods = new();
        public GLM00500GLSystemParamDTO SystemParams = new();

        public int SelectedYear;

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

        //set finalize budget
        public async Task SetFinalizeBudget(string pcRecId)
        {
            var loEx = new R_Exception();

            try
            {
                GLM00500CrecParamsDTO loParams = new(){CREC_ID = pcRecId};
                await _model.GLM00500FinalizeBudgetModel(loParams);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationBudgetHD(GLM00500BudgetHDDTO param)
        {
            var loEx = new R_Exception();
            if (param.CBUDGET_NO is null or "") loEx.Add(new Exception("Budget No. is required!"));
            if (param.CBUDGET_NAME is null or "") loEx.Add(new Exception("Budget Name is required!"));
            if (param.CCURRENCY_TYPE is null or "") loEx.Add(new Exception("Please select Currency Type!"));
            loEx.ThrowExceptionIfErrors();
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

            return loResult;
        }
    }
}