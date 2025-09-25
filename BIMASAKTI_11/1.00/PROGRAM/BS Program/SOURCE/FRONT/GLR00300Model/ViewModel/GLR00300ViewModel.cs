using GLR00300Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLR00300FrontResources;
using Lookup_PMCOMMON.DTOs.PopUpSaveAs;

namespace GLR00300Model.ViewModel
{
    public class GLR00300ViewModel : R_ViewModel<GLR00300DTO>
    {
        private GLR00300Model _modelGLR00300Model = new GLR00300Model();

        public ObservableCollection<GLR00300DTO> TrialBalanceList =
            new ObservableCollection<GLR00300DTO>();
        public ObservableCollection<GLR00300DTO> PrintMethodList =
            new ObservableCollection<GLR00300DTO>();
        public ObservableCollection<GLR00300BudgetNoDTO> BudgetNoList =
            new ObservableCollection<GLR00300BudgetNoDTO>();
        public ObservableCollection<GLR00300GetPeriod> GetPeriodList =
            new ObservableCollection<GLR00300GetPeriod>();

        public GLR00300InitialProcess InitialProcess = new GLR00300InitialProcess();
        public GLR00300DTO CurrencyType = new GLR00300DTO();
        public List<GLR00300DTO> CurrencyTypeList { get; set; } = new List<GLR00300DTO>
            { new GLR00300DTO { CCODE = "L", CNAME = "Local Currency" },
              new GLR00300DTO { CCODE = "B", CNAME = "Base Currency" } };
        public List<GLR00300DTO> JournalAdustModeList { get; set; } = new List<GLR00300DTO>
        { new GLR00300DTO { CCODE = "S", CNAME = "Split" },
            new GLR00300DTO { CCODE = "M", CNAME = "Merged" } };

        public GLR00300GetAllCenter FromCenter = new GLR00300GetAllCenter();
        public GLR00300GetAllCenter ToCenter = new GLR00300GetAllCenter();

        public string TrialBalanceTypeValue = "N";
        public string CurrencyTypeValue = "L";
        public string JournalAdjustModeValue = "S";
        public int PeriodYear;
        public string PeriodNo = "01";
        public bool _lPrintByCenter = false;
        public string PrintMethodValue = "00";
        public bool _lPrintBudget = false;
        public string BudgetNoValue = "";
        public bool _lIsTrialBalanceTypeIsNormal = true;   
        public async Task GetTrialBalance()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLR00300Model.GetTrialBalanceTypeAsyncModel();
                TrialBalanceList = new ObservableCollection<GLR00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetInitialProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLR00300Model.GetInitialProcessAsyncModel();
                InitialProcess = loResult;
                PeriodYear = int.Parse(loResult.CSOFT_PERIOD_YY);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public void ValidationFieldEmpty()
        {
            var loEx = new R_Exception();
            try
            {
                #region ValidationEmpty

                if (string.IsNullOrEmpty(TrialBalanceTypeValue))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLR00300_Class), "Error_00301");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(PeriodNo))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLR00300_Class), "Error_00302");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(FromCenter.CCENTER_CODE) && _lPrintByCenter)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLR00300_Class), "Error_00303");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(ToCenter.CCENTER_CODE) && _lPrintByCenter)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLR00300_Class), "Error_00304");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(PrintMethodValue))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLR00300_Class), "Error_00305");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(BudgetNoValue) && _lPrintBudget)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLR00300_Class), "Error_00306");
                    loEx.Add(loErr);
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public async Task GetPrintMethod()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLR00300Model.GetPrintMethodTypeAsyncModel();
                PrintMethodList = new ObservableCollection<GLR00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetBudgetNo()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcPeriodYear = PeriodYear.ToString();
                var loParam = new GLR00300DBParameterDTO();
                loParam.PERIOD_YEAR = lcPeriodYear;
                loParam.CURRENCY_TYPE = CurrencyTypeValue;

                var loResult = await _modelGLR00300Model.GetBudgetNoAsyncModel(loParam);
                BudgetNoList = new ObservableCollection<GLR00300BudgetNoDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetPeriod()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcPeriodYear = PeriodYear.ToString();
                var loParam = new GLR00300DBParameterDTO();
                loParam.PERIOD_YEAR = lcPeriodYear;

                var loResult = await _modelGLR00300Model.GetPeriodAsyncoModel(loParam);
                GetPeriodList = new ObservableCollection<GLR00300GetPeriod>(loResult.Data);

                var loPeriodNo = GetPeriodList.ToList().FirstOrDefault()?.CPERIOD_NO;
                if (loPeriodNo != null)
                    PeriodNo = loPeriodNo;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
