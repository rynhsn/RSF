using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01600ViewModel : R_ViewModel<GSL01600DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL01600DTO> CashFlowGrpTypeGrid = new ObservableCollection<GSL01600DTO>();

        public async Task GetCashFlowGrpList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01600GetCashFlowGroupTypeListAsync();

                CashFlowGrpTypeGrid = new ObservableCollection<GSL01600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL01600DTO> GetCashFlowGrp(GSL01600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01600DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL01600GetCashFlowGroupTypeAsync(poEntity);
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
