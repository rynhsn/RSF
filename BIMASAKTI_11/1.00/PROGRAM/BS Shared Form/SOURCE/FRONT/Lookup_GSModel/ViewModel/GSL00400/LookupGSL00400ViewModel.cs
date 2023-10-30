using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00400ViewModel : R_ViewModel<GSL00400DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL00400DTO> JournalGroupGrid = new ObservableCollection<GSL00400DTO>();

        public async Task GetJournalGroupList(GSL00400ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00400GetJournalGroupListAsync(poParam);

                JournalGroupGrid = new ObservableCollection<GSL00400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
