using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00600ViewModel : R_ViewModel<GSL00600DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL00600DTO> UnitTypeCategoryGrid = new ObservableCollection<GSL00600DTO>();

        public async Task GetUnitTypeCategoryList(GSL00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00600GetUnitTypeCategoryListAsync(poParam);

                UnitTypeCategoryGrid = new ObservableCollection<GSL00600DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
