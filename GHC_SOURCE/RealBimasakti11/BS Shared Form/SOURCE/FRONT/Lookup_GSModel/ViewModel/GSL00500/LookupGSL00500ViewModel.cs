using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00500ViewModel : R_ViewModel<GSL00500DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00500DTO> GLAccountList = new ObservableCollection<GSL00500DTO>();

        public async Task GetGLAccountList(GSL00500ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                
                var loResult = await _model.GSL00500GetGLAccountListAsync(poParam);

                GLAccountList = new ObservableCollection<GSL00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSL00500DTO> GetGLAccount(GSL00500ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSL00500DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00500GetGLAccountAsync(poParam);

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
