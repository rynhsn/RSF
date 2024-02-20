using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Model.ViewModel
{
    public class LMT03500ViewModel : R_ViewModel<LMT03500PropertyDTO>
    {
        private LMT03500InitModel _model = new LMT03500InitModel();
        public List<LMT03500PropertyDTO> PropertyList = new List<LMT03500PropertyDTO>();
        public List<LMT03500TransCodeDTO> TransCodeList = new List<LMT03500TransCodeDTO>();

        public string PropertyId = string.Empty;
        public string TransCodeId = string.Empty;

        public async Task Init()
        {
            await GetPropertyList();
            await GetTransCodeList();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<LMT03500ListDTO<LMT03500PropertyDTO>>(
                        nameof(ILMT03500Init.LMT03500GetPropertyList));
                PropertyList = loReturn.Data;
                PropertyId = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCodeList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<LMT03500ListDTO<LMT03500TransCodeDTO>>(
                        nameof(ILMT03500Init.LMT03500GetTransCodeList));
                TransCodeList = loReturn.Data;
            }
            catch (System.Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}