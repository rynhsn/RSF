using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01500ViewModel : R_ViewModel<GSL01500ResultDetailDTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL01500ResultDetailDTO> CashFlowDetailGrid = new ObservableCollection<GSL01500ResultDetailDTO>();

        public List<GSL01500ResultGroupDTO> CashFlowGropList = new List<GSL01500ResultGroupDTO>();

        public string CashFlowCode { get; set; } = "";

        public async Task GetCashFlowDetailList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01500GetCashDetailListAsync(CashFlowCode);

                CashFlowDetailGrid = new ObservableCollection<GSL01500ResultDetailDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCashFlowGroupList(GSL01500ParameterGroupDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01500GetCashFlowGroupListAsync();

                CashFlowGropList = new List<GSL01500ResultGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }

}
