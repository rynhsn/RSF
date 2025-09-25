using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMModel.ViewModel.PML01200
{
    public class LookupLML01200ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01200DTO> InvoiceGroupList = new ObservableCollection<LML01200DTO>();


        public async Task GetInvoiceGroupList(LML01200ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CINVGRP_CODE, poParam.CINVGRP_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CACTIVE_TYPE, poParam.CACTIVE_TYPE);

                var loResult = await _model.PML01200InvoiceGroupListAsync();
                InvoiceGroupList = new ObservableCollection<LML01200DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01200DTO> GetInvoiceGroup(LML01200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01200DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.PML01200GetInvoiceGroupAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
    }
}
