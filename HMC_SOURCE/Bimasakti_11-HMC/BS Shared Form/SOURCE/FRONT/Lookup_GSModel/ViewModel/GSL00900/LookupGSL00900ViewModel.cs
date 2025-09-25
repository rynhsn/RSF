using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00900ViewModel : R_ViewModel<GSL00900DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00900DTO> CenterGrid = new ObservableCollection<GSL00900DTO>();

        public async Task GetCenterList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00900GetCenterListAsync();

                CenterGrid = new ObservableCollection<GSL00900DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL00900DTO> GetCenter(GSL00900ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00900DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00900GetCenterAsync(poEntity);
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
