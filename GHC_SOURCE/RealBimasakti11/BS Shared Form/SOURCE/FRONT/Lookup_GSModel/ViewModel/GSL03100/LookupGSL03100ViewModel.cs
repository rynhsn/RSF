using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL03100ViewModel : R_ViewModel<GSL03100DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03100DTO> ExpenditureGrid = new ObservableCollection<GSL03100DTO>();
        public GSL03100ParameterDTO ExpenditureParameter = new GSL03100ParameterDTO();
        public string CategoryOption { get; set; } = "A";

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> CategoryOptionsList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("A", R_FrontUtility.R_GetMessage(typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_AllCategories")),
            new KeyValuePair<string, string>("S", R_FrontUtility.R_GetMessage(typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_SelectedCategory")),
        };
        #endregion

        public async Task GetExpenditureList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03100GetExpenditureListAsync(ExpenditureParameter);

                ExpenditureGrid = new ObservableCollection<GSL03100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03100DTO> GetExpenditure(GSL03100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03100DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03100GetExpenditureAsync(poParameter);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
