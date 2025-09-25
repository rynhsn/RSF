using PMM01000COMMON;
using PMM01000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01002ViewModel : R_ViewModel<PMM01000PrintParamDTO>
    {
        private PMM01000UniversalModel _PMM01000UniversalModel = new PMM01000UniversalModel();

        public List<PMM01000UniversalDTO> RateTypeList = new List<PMM01000UniversalDTO>();


        public PMM01000DTOPropety Property = new PMM01000DTOPropety();
        // Status Radio Button
        public List<RadioButton> ShortByList { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ChargesId")},
            new RadioButton { Id = "02", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ChargesName") },
        };

        public async Task GetRateTypeList(PMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetChargesTypeListAsync();

                RateTypeList = new List<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
