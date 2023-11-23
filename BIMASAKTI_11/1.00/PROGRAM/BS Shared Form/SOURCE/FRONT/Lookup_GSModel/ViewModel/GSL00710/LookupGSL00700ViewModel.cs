using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00710ViewModel : R_ViewModel<GSL00710DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL00710DTO> DepartmentPropertyGrid = new ObservableCollection<GSL00710DTO>();

        public async Task GetDepartmentPropertyList(GSL00710ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00710GetDepartmentPropertyListAsync(poParam);

                DepartmentPropertyGrid = new ObservableCollection<GSL00710DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
