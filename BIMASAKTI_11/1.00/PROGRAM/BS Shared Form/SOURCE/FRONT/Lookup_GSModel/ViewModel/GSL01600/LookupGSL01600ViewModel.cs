using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01600ViewModel : R_ViewModel<GSL01600DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL01600DTO> CashFlowGrpTypeGrid = new ObservableCollection<GSL01600DTO>();

        public async Task GetCashFlowGrpList(GSL01600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01600GetCashFlowGroupTypeListAsync(poParameter);

                CashFlowGrpTypeGrid = new ObservableCollection<GSL01600DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
