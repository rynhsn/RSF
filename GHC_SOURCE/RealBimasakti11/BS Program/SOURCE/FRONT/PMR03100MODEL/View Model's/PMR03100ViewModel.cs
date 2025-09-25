using PMR03100COMMON;
using PMR03100COMMON.DTO_s;
using PMR03100COMMON.DTO_s.General;
using PMR03100COMMON.DTO_s.Print;
using PMR03100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMR03100MODEL.View_Model_s
{
    public class PMR03100ViewModel
    {
        private PMR03100Model _model = new PMR03100Model();

        public List<PropertyDTO> PropertyList = new List<PropertyDTO>();

        public PeriodYearRangeDTO PeriodYearRange = new PeriodYearRangeDTO();

        public ParamDTO ReportParam = new ParamDTO();


        public int PickedPeriodYear { get; set; }

        public async Task InitialProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                // Get from back
                PropertyList = new List<PropertyDTO>(await _model.GetPropertyListAsync());
                PeriodYearRange = await _model.GetPeriodYearRangeRecordAsync(new PeriodYearRangeParamDTO() { CMODE = "", CYEAR = "" });
                if (PeriodYearRange == null)
                {
                    PeriodYearRange.IMAX_YEAR = 9999;
                    PeriodYearRange.IMIN_YEAR = 1;
                }

                //set default value
                PickedPeriodYear = DateTime.Now.Year;
                ReportParam.CPROPERTY_ID = PropertyList[0]?.CPROPERTY_ID ?? "";
                ReportParam.CPROPERTY_NAME = PropertyList[0]?.CPROPERTY_NAME ?? "";
                ReportParam.CREPORT_FILENAME = "";
                ReportParam.CREPORT_FILEEXT = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void Validation()
        {
            R_Exception loEx = new R_Exception();

            if (string.IsNullOrWhiteSpace(ReportParam.CPROPERTY_ID))
            {
                var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_emptyProperty");
                loEx.Add(loErr);
            }

            if (PickedPeriodYear <= 0)
            {
                var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_emptyCutOffYear");
                loEx.Add(loErr);
            }

            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }

            loEx.ThrowExceptionIfErrors();

        }

        public ParamDTO GenerateParam()
        {
            R_Exception loEx = new R_Exception();
            ParamDTO loParam = new ParamDTO();
            try
            {
                loParam.CPROPERTY_ID = ReportParam.CPROPERTY_ID;
                loParam.CPROPERTY_NAME = PropertyList.Where(x => x.CPROPERTY_ID == ReportParam.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME;
                loParam.CCUT_OFF_YEAR = PickedPeriodYear.ToString();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loParam;
        }

    }
}
