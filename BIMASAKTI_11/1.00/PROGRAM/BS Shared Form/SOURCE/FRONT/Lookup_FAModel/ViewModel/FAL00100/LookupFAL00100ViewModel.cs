using Lookup_FACommon.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_FAModel.ViewModel.FAL00100
{
    public class LookupFAL00100ViewModel : R_ViewModel<FAL00100DTO>
    {
        private PublicLookupFAModel _model = new PublicLookupFAModel();

        public ObservableCollection<FAL00100DTO> TaxTypeGrid = new ObservableCollection<FAL00100DTO>();

        public async Task GetTaxTypeList(FAL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(FAL00100ContextDTO.CSTATUS, poParam.CSTATUS);
                R_FrontContext.R_SetStreamingContext(FAL00100ContextDTO.CTAX_TYPE_ID, poParam.CTAX_TYPE_ID);
                var loResult = await _model.FAL00100TaxTypeLookupAsync();
                TaxTypeGrid = new ObservableCollection<FAL00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task<FAL00100DTO> GetTaxType(FAL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            FAL00100DTO loData = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(FAL00100ContextDTO.CSTATUS, poParam.CSTATUS);
                R_FrontContext.R_SetStreamingContext(FAL00100ContextDTO.CTAX_TYPE_ID, poParam.CTAX_TYPE_ID);
                var loResult = await _model.FAL00100TaxTypeLookupAsync();

                TaxTypeGrid = new ObservableCollection<FAL00100DTO>(loResult);

                loData = TaxTypeGrid.ToList().Find(x => x.CTAX_TYPE_ID == poParam.CTAX_TYPE_ID);
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
