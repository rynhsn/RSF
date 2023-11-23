using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01800ViewModel : R_ViewModel<GSL01800DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL01800DTO> CategoryGrid = new ObservableCollection<GSL01800DTO>();

        public async Task GetCategoryList(GSL01800DTOParameter poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01800GetCategoryListAsync(poParameter);

                CategoryGrid = new ObservableCollection<GSL01800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
