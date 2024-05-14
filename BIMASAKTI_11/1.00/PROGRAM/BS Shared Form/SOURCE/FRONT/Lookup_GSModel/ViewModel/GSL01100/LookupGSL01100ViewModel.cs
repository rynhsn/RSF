using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01100ViewModel : R_ViewModel<GSL01100DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL01100DTO> UserApprovalGrid = new ObservableCollection<GSL01100DTO>();

        public async Task GetUserApprovalList(GSL01100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _model.GSL01100GetUserApprovalListAsync(poParameter);

                UserApprovalGrid = new ObservableCollection<GSL01100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL01100DTO> GetUserApproval(GSL01100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL01100DTO loRtn = null;
            try
            {

                var loResult = await _modelRecord.GSL01100GetUserApprovalAsync(poParameter);
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
