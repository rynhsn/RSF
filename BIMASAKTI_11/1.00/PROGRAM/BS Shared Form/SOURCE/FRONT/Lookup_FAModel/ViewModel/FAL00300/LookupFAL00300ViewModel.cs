using Lookup_FACommon.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_FAModel.ViewModel.FAL00200
{
    public class LookupFAL00300ViewModel : R_ViewModel<FAL00300DTO>
    {
        private PublicLookupFAModel _model = new PublicLookupFAModel();

        public ObservableCollection<FAL00300DTO> AssetGrid = new ObservableCollection<FAL00300DTO>();

        public async Task GetTaxCategoryList(FAL00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(FAL00300ContextDTO.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(FAL00300ContextDTO.CASSET_CODE, poParam.CASSET_CODE);
                var loResult = await _model.FAL00300AssetLookupAsync();
                AssetGrid = new ObservableCollection<FAL00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task<FAL00300DTO> GetTaxCategory(FAL00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            FAL00300DTO loData = null;

            try
            {
                if (poParam.CTRANS_CODE == null)
                {
                    poParam.CTRANS_CODE = "";
                }
                if (poParam.CASSET_CODE == null)
                {
                    poParam.CASSET_CODE = "";
                }
                R_FrontContext.R_SetStreamingContext(FAL00300ContextDTO.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(FAL00300ContextDTO.CASSET_CODE, poParam.CASSET_CODE);
                var loResult = await _model.FAL00300AssetLookupAsync();

                AssetGrid = new ObservableCollection<FAL00300DTO>(loResult);

                loData = AssetGrid.ToList().Find(x => x.CASSET_CODE == poParam.CASSET_CODE);
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
