using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using GLM00500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00500Model.ViewModel
{
    public class GLM00500DetailViewModel : R_ViewModel<GLM00500BudgetDTDTO>
    {
        private GLM00500DetailModel _model = new GLM00500DetailModel();

        public ObservableCollection<GLM00500BudgetDTGridDTO> BudgetDTList =
            new ObservableCollection<GLM00500BudgetDTGridDTO>();

        public ObservableCollection<GLM00500FunctionDTO> RoundingMethodList =
            new ObservableCollection<GLM00500FunctionDTO>();

        public ObservableCollection<GLM00500BudgetWeightingDTO> BudgetWeightingList =
            new ObservableCollection<GLM00500BudgetWeightingDTO>();

        public GLM00500BudgetHDDTO BudgetHDEntity = new GLM00500BudgetHDDTO();
        public GLM00500BudgetDTDTO BudgetDTEntity = new GLM00500BudgetDTDTO();
        public GLM00500GenerateAccountBudgetDTO GenerateAccountBudget = new GLM00500GenerateAccountBudgetDTO();

        public GLM00500PeriodCountDTO PeriodCount = new GLM00500PeriodCountDTO();
        public GLM00500GSMCompanyDTO Company = new GLM00500GSMCompanyDTO();
        public string SelectedAccountType { get; set; } = "N";

        public List<KeyValuePair<string, string>> CGLACCOUNT_TYPE { get; } = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("N", "Normal Account"),
            new KeyValuePair<string, string>("N", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "NormalAccount")),
            // new KeyValuePair<string, string>("S", "Statistic Account")
            new KeyValuePair<string, string>("S", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "StatisticAccount"))
        };

        //list 01 - 12
        public List<KeyValuePair<string, string>> CPERIOD { get; } = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("01", "01"),
            new KeyValuePair<string, string>("02", "02"),
            new KeyValuePair<string, string>("03", "03"),
            new KeyValuePair<string, string>("04", "04"),
            new KeyValuePair<string, string>("05", "05"),
            new KeyValuePair<string, string>("06", "06"),
            new KeyValuePair<string, string>("07", "07"),
            new KeyValuePair<string, string>("08", "08"),
            new KeyValuePair<string, string>("09", "09"),
            new KeyValuePair<string, string>("10", "10"),
            new KeyValuePair<string, string>("11", "11"),
            new KeyValuePair<string, string>("12", "12")
        };

        public List<KeyValuePair<string, string>> CINPUT_METHOD { get; } = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("MN", "Manually"),
            new KeyValuePair<string, string>("MN", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Manually")),
            // new KeyValuePair<string, string>("MO", "Monthly"),
            new KeyValuePair<string, string>("MO", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Monthly")),
            // new KeyValuePair<string, string>("AN", "Annually")
            new KeyValuePair<string, string>("AN", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Annually"))
        };

        public List<KeyValuePair<string, string>> CDIST_METHOD { get; } = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("EV", "Evenly"),
            new KeyValuePair<string, string>("EV", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Evenly")),
            // new KeyValuePair<string, string>("BW", "Budget Weighting")
            new KeyValuePair<string, string>("BW", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "BudgetWeighting"))
        };

        #region popup generate

        private GLM00500HeaderModel _headerModel = new GLM00500HeaderModel();
        public List<GLM00500BudgetHDDTO> BudgetHDList = new List<GLM00500BudgetHDDTO>();
        public GLM00500GSMPeriodDTO Periods = new GLM00500GSMPeriodDTO();
        public GLM00500GLSystemParamDTO SystemParams = new GLM00500GLSystemParamDTO();
        public int SelectedYear;

        public List<KeyValuePair<string, string>> CBY { get; } = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("P", "Percentage"),
            new KeyValuePair<string, string>("P", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Percentage")),
            // new KeyValuePair<string, string>("V", "Value")
            new KeyValuePair<string, string>("V", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Value"))
        };

        public List<KeyValuePair<string, string>> CUPDATE_METHOD { get; } = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("C", "Clear All Existing Data"),
            new KeyValuePair<string, string>("C", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ClearAllExistingData")),
            // new KeyValuePair<string, string>("S", "Skip Existing Data"),
            new KeyValuePair<string, string>("S", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "SkipExistingData")),
            // new KeyValuePair<string, string>("R", "Replace Existing Data")
            new KeyValuePair<string, string>("R", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ReplaceExistingData"))
        };

        public List<KeyValuePair<string, string>> CBASED_ON { get; } = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("EB", "Existing Budget"),
            new KeyValuePair<string, string>("EB", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ExistingBudget")),
            // new KeyValuePair<string, string>("AV", "Actual Value")
            new KeyValuePair<string, string>("AV", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ActualValue"))
        };

        #endregion

        //init 
        public async Task Init(object poEntity)
        {
            BudgetHDEntity = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(poEntity);
            await GetRoundingMethodList();
            await GetBudgetWeightingList();
            await GetPeriodCount();
            await GetCompany();
        }

        private async Task GetRoundingMethodList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GLM00500GetRoundingMethodListModel();
                // RoundingMethodList = new ObservableCollection<GLM00500FunctionDTO>(loResult.Data);
                //order by ccode
                RoundingMethodList = new ObservableCollection<GLM00500FunctionDTO>(loResult.Data.OrderBy(x => x.CCODE));
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBudgetWeightingList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GLM00500GetBudgetWeightingListStreamModel();
                BudgetWeightingList = new ObservableCollection<GLM00500BudgetWeightingDTO>(loResult);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get list
        public async Task GetBudgetDTList(string pcBudgetId, string pcAccountType)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CBUDGET_ID, pcBudgetId);
                R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CGLACCOUNT_TYPE, pcAccountType);
                var loResult = await _model.GLM00500GetBudgetDTListStreamModel();
                BudgetDTList = new ObservableCollection<GLM00500BudgetDTGridDTO>(loResult);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get record
        public async Task GetBudgetDT(GLM00500BudgetDTDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                BudgetDTEntity = await _model.R_ServiceGetRecordAsync(poEntity);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //save record
        public async Task SaveBudgetDT(GLM00500BudgetDTDTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                BudgetDTEntity = await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //delete record
        public async Task DeleteBudgetDT(GLM00500BudgetDTDTO poEntity)
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

        //get period count
        public async Task GetPeriodCount()
        {
            var loEx = new R_Exception();

            try
            {
                var loParams = new GLM00500YearParamsDTO { CYEAR = BudgetHDEntity.CYEAR };
                PeriodCount = await _model.GLM00500GetPeriodCountModel(loParams);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //get company
        public async Task GetCompany()
        {
            var loEx = new R_Exception();

            try
            {
                Company = await _model.GLM00500GetGSMCompanyModel();
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GLM00500BudgetCalculateDTO> CalculateBudget(GLM00500BudgetHDDTO poHeader,
            GLM00500BudgetDTDTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00500BudgetCalculateDTO loResult = null;

            try
            {
                var loParam = new GLM00500CalculateParamDTO()
                {
                    INO_PERIOD = PeriodCount.INO_PERIOD,
                    CCURRENCY_TYPE = poHeader.CCURRENCY_TYPE,
                    NBUDGET = poEntity.NBUDGET,
                    CROUNDING_METHOD = poEntity.CROUNDING_METHOD,
                    CDIST_METHOD = poEntity.CDIST_METHOD,
                    CBW_CODE = poEntity.CBW_CODE
                };

                loResult = await _model.GLM00500BudgetCalculateModel(loParam);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }


        #region popup generate

        public async Task GetBudgetHDList(int pnYear)
        {
            var loEx = new R_Exception();
            List<GLM00500BudgetHDDTO> loResult;

            try
            {
                R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CYEAR, pnYear.ToString());
                loResult = await _headerModel.GLM00500GetBudgetHDListStreamModel();
                BudgetHDList = loResult;
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriods()
        {
            var loEx = new R_Exception();
            GLM00500GSMPeriodDTO loResult;

            try
            {
                loResult = await _headerModel.GLM00500GetPeriodsModel();
                Periods = loResult;
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParams()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _headerModel.GLM00500GetSystemParamModel();
                SystemParams = loResult;
                SelectedYear = int.Parse(SystemParams.CSOFT_PERIOD_YY);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        public async Task<bool> GenerateBudget(GLM00500GenerateAccountBudgetDTO result)
        {
            var loEx = new R_Exception();
            var loResult = new GLM00500ReturnDTO();

            try
            {
                loResult = await _model.GLM00500GenerateBudgetModel(result);
            }
            catch (R_Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult.LRESULT;
        }
    }
}