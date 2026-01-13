using Lookup_FACommon.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_FAModel.ViewModel.FAL00200
{
    public class LookupFAL00200ViewModel : R_ViewModel<FAL00200DTO>
    {
        private PublicLookupFAModel _model = new PublicLookupFAModel();

        public ObservableCollection<FAL00200DTO> TaxTypeGrid = new ObservableCollection<FAL00200DTO>();

        public async Task GetTaxCategoryList(FAL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(FAL00200ContextDTO.CSTATUS, poParam.CSTATUS);
                R_FrontContext.R_SetStreamingContext(FAL00200ContextDTO.CTAX_CATEGORY_ID, poParam.CTAX_CATEGORY_ID);
                var loResult = await _model.FAL00200TaxCategoryLookupAsync();
                TaxTypeGrid = new ObservableCollection<FAL00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task<FAL00200DTO> GetTaxCategory(FAL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            FAL00200DTO loData = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(FAL00200ContextDTO.CSTATUS, poParam.CSTATUS);
                R_FrontContext.R_SetStreamingContext(FAL00200ContextDTO.CTAX_CATEGORY_ID, poParam.CTAX_CATEGORY_ID);
                var loResult = await _model.FAL00200TaxCategoryLookupAsync();

                TaxTypeGrid = new ObservableCollection<FAL00200DTO>(loResult);

                loData = TaxTypeGrid.ToList().Find(x => x.CTAX_CATEGORY_ID == poParam.CTAX_CATEGORY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loData;
        }

    }
}
