using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02910ViewModel : R_ViewModel<GSL02910DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL02910DTO> SupplierInfoGrid = new ObservableCollection<GSL02910DTO>();

        public async Task GetSupplierInfoList(GSL02910ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02910GetSupplierInfoListAsync(poParameter);

                SupplierInfoGrid = new ObservableCollection<GSL02910DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02910DTO> GetSupplierInfo(GSL02910ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL02910DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL02910GetSupplierInfoAsync(poParameter);
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
