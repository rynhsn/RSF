using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00700ViewModel : R_ViewModel<GSL00700DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00700DTO> DepartmentGrid = new ObservableCollection<GSL00700DTO>();

        public async Task GetDepartmentList(GSL00700ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00700GetDepartmentListAsync(poParam);

                DepartmentGrid = new ObservableCollection<GSL00700DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL00700DTO> GetDepartment(GSL00700ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSL00700DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00700GetDepartmentAsync(poParam);
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
