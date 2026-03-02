using FAM00200Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Collections.ObjectModel;

namespace FAM00200Model.ViewModel
{
    public class FAM00200ViewModel : R_ViewModel<FAM00200DTO>
    {
        private FAM00200Model _FAM00200Model = new FAM00200Model();

        #region Property Class
        public List<FAM00200GSBCodeDTO> TaxTypeTypeList { get; set; } = new List<FAM00200GSBCodeDTO>();
        public ObservableCollection<FAM00200DTO> TaxTypeGrid { get; set; } = new ObservableCollection<FAM00200DTO>();
        public FAM00200DTO TaxType { get; set; } = new FAM00200DTO();
        public string Property_ID { get; set; }
        #endregion

        #region Combo Box Helper List
        public List<KeyValuePair<bool, string>> ActiveInactiveList { get; } = new List<KeyValuePair<bool, string>>()
        {
            new KeyValuePair<bool, string>(true, R_FrontUtility.R_GetMessage(typeof(FAM00200FrontResources.Resources_Dummy_Class), "_Active")),
            new KeyValuePair<bool, string>(false, R_FrontUtility.R_GetMessage(typeof(FAM00200FrontResources.Resources_Dummy_Class), "_Inactive")),
        };

        public int IMIN_YEAR = 0;
        public int IMAX_YEAR = 100;

        #endregion

        public async Task GetListTaxType()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _FAM00200Model.GetListTaxTypeAsync();

                TaxTypeGrid = new ObservableCollection<FAM00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxType(FAM00200DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _FAM00200Model.GetTaxTypeAsync(poEntity);

                TaxType = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTaxType(FAM00200DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new FAM00200SaveParameterDTO { Entity = poEntity, CRUDMode = poCRUDMode };
                var loResult = await _FAM00200Model.SaveTaxTypeAsync(loParam);

                TaxType = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
