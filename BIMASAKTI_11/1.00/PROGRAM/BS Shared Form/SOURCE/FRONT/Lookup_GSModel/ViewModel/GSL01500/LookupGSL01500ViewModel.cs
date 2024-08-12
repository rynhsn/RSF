using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01500ViewModel : R_ViewModel<GSL01500ResultDetailDTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

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
                loResult.Add(new GSL01500ResultGroupDTO { CCASH_FLOW_GROUP_CODE = "", CCASH_FLOW_GROUP_NAME = "" });

                CashFlowGropList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSL01500DTO> GetCashFlow(GSL01500ParameterGroupDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL01500DTO loRtn = null;
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSL01500ParameterDetailDTO>(poParameter);
                var loResult = await _modelRecord.GSL01500GetCashDetailAsync(loParam);
                loRtn = R_FrontUtility.ConvertObjectToObject<GSL01500DTO>(loResult);
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
