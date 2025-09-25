using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02900ViewModel : R_ViewModel<GSL02900DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL02900DTO> SupplierGrid = new ObservableCollection<GSL02900DTO>();
        public GSL02900ParameterDTO SupplierParameter = new GSL02900ParameterDTO();
        public string SearchText { get; set; } = "";
        public async Task GetSupplierList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02900GetSupplierListAsync(SupplierParameter);

                SupplierGrid = new ObservableCollection<GSL02900DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02900DTO> GetSupplier(GSL02900ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL02900DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL02900GetSupplierAsync(poParameter);
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
