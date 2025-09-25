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
    public class LookupGSL03000ViewModel : R_ViewModel<GSL03000DTO>
    {

        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03000DTO> ProductGrid = new ObservableCollection<GSL03000DTO>();
        public GSL03000ParameterDTO ProductParameter = new GSL03000ParameterDTO();
        public string CategoryOption { get; set; } = "A";

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> CategoryOptionsList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("A", R_FrontUtility.R_GetMessage(typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_AllCategories")),
            new KeyValuePair<string, string>("S", R_FrontUtility.R_GetMessage(typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_SelectedCategory")),
        };
        #endregion

        public async Task GetProductList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03000GetProductListAsync(ProductParameter);

                ProductGrid = new ObservableCollection<GSL03000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03000DTO> GetProduct(GSL03000ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03000DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03000GetProductAsync(poParameter);
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
