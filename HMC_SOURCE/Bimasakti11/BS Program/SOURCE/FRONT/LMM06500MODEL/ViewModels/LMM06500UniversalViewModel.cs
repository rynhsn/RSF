using LMM06500COMMON;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM06500MODEL
{
    public class LMM06500UniversalViewModel
    {
        private LMM06500UniversalModel _LMM06500UniversalModel = new LMM06500UniversalModel();
        public List<LMM06500UniversalDTO> PositionList { get; set; } = new List<LMM06500UniversalDTO>();
        public List<LMM06500UniversalDTO> GenderList { get; set; } = new List<LMM06500UniversalDTO>();

        public async Task GetPositionList(LMM06500UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM06500UniversalModel.GetPositionListAsync(poParam);

                PositionList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetGenderList(LMM06500UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM06500UniversalModel.GetGenderListAsync(poParam);
                foreach (var item in loResult)
                {
                    item.CCODE = item.CCODE.Trim();
                }

                GenderList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }

}
