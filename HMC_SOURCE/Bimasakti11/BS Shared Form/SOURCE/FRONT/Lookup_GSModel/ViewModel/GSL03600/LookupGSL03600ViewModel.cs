using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL03600ViewModel : R_ViewModel<GSL03600DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03600DTO> CompanyGrid = new ObservableCollection<GSL03600DTO>();
        public GSL03600ParameterDTO CompanyParameter = new GSL03600ParameterDTO();
        public async Task GetCompanyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03600GetCompanyListAsync(CompanyParameter);

                CompanyGrid = new ObservableCollection<GSL03600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03600DTO> GetCompany(GSL03600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03600DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03600GetCompanyAsync(poParameter);
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
