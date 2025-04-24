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
    public class LookupGSL03200ViewModel : R_ViewModel<GSL03200DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03200DTO> ProductAllocationGrid = new ObservableCollection<GSL03200DTO>();
        public GSL03200ParameterDTO ProductAllocationParameter = new GSL03200ParameterDTO();
        public async Task GetProductAllocationList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03200GetProductAllocationListAsync(ProductAllocationParameter);
                loResult.ForEach(x =>
                {
                    x.CGLACCOUNT_NO_NAME = x.CGLACCOUNT_NO + " - " + x.CGLACCOUNT_NAME;
                });

                ProductAllocationGrid = new ObservableCollection<GSL03200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03200DTO> GetProductAllocation(GSL03200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03200DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03200GetProductAllocationAsync(poParameter);
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
