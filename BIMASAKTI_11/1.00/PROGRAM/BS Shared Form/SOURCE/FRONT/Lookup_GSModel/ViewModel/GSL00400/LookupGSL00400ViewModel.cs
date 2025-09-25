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
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

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
        public async Task<GSL00400DTO> GetJournalGroup(GSL00400ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSL00400DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00400GetJournalGroupAsync(poParam);
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
