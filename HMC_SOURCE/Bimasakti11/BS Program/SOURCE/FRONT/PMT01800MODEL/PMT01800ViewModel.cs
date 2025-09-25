using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMT01800COMMON;
using PMT01800COMMON.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT01800Model
{
    public class PMT01800ViewModel : R_ViewModel<PMT01800DTO>
    {
        private Model.PMT01800Model _PMT01800Model = new Model.PMT01800Model();

        public ObservableCollection<PMT01800DTO> loGridList = new ObservableCollection<PMT01800DTO>();
        public ObservableCollection<PMT01800DTO> LoGridListDetail = new ObservableCollection<PMT01800DTO>();

        public List<PMT01800PropertyDTO> PropertyList { get; set; } = new List<PMT01800PropertyDTO>();
        public List<PMT01800DTO> DataGrid { get; set; } = new List<PMT01800DTO>();

        public PMT01800InitialProcessDTO loInitSpinnerYear = new PMT01800InitialProcessDTO();


        public DateTime? OfferDate;
        public string CValue = "";
        public string TransCode = "";
        public string DeptCode = "";
        public string RefNo = "";
        public string BuildingName = "";
        public string buildingId = "";

        public string OfferDateString;
        public string LOC = "";
        public string LOO = "";
        public string propertyValue = "";

        public bool Test = false;


        public async Task GetProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01800Model.GetPropertySreamAsync();
                PropertyList = loResult.Data;
                propertyValue = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGridListHeader()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CPROPERTY_ID, propertyValue);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CFROM_DATE, OfferDateString);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CLOC_TRANS_CODE,
                    string.IsNullOrWhiteSpace(LOC) ? "802053" : LOC);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CLOO_TRANS_CODE,
                    string.IsNullOrWhiteSpace(LOO) ? "802043" : LOO);

                var loReturn = await _PMT01800Model.GetPMT01800StreamAsync();
                // for (int a = 1; a < 2; a++)
                // {
                //     loReturn.Data.Add(new PMT01800DTO()
                //     {
                //         CDEPT_NAME = "CBUILDING_NAME" + a,
                //         CREF_NO = "CFLOOR_NAME" + a,
                //         CREF_DATE = "20230908",
                //         CFOLLOW_UP_DATE = "20230908",
                //         CTENANT_NAME = "CUNIT_NAME" + a,
                //         CSALESMAN_NAME = "CUNIT_TYPE_NAME" + a,
                //         CORIGINAL_REF_NO = "12341231",
                //         CTRANS_STATUS_DESC = "NGROSS_AREA_SIZE" + a,
                //         CAGREEMENT_STATUS_DESC = "NNET_AREA_SIZE" + a
                //
                //     });
                // }
                    if (loReturn.Data.Count != 0)
                    {
                        loReturn.Data.ForEach(x =>
                        {
                            x.CREF_DATE_DISPLAY = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.AssumeUniversal, out var ldRefDate)
                                ? ldRefDate
                                : (DateTime?)null;
                            x.CFOLLOW_UP_DATE_DISPLAY =
                                DateTime.TryParseExact(x.CFOLLOW_UP_DATE, "yyyyMMdd", CultureInfo.InvariantCulture,
                                    DateTimeStyles.AssumeUniversal, out var ldFollowUpDate)
                                    ? ldFollowUpDate
                                    : (DateTime?)null;
                        });
                    }

                loGridList = new ObservableCollection<PMT01800DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGridListDetail()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CPROPERTY_ID, propertyValue);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CTRANS_CODE, TransCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CDEPT_CODE, DeptCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CREF_NO, RefNo);
                var loReturn = await _PMT01800Model.GetPMT01800DetailStreamAsync();
                // for (int a = 1; a < 50; a++)
                // {
                //     loReturn.Data.Add(new PMT01800DTO()
                //     {
                //         CBUILDING_NAME = "CBUILDING_NAME" + a,
                //         CFLOOR_NAME = "CFLOOR_NAME" + a,
                //         CUNIT_NAME = "CUNIT_NAME" + a,
                //         CUNIT_TYPE_NAME = "CUNIT_TYPE_NAME" + a,
                //         NGROSS_AREA_SIZE = a,
                //         NNET_AREA_SIZE = a
                //
                //
                //     });
                // }

                LoGridListDetail = new ObservableCollection<PMT01800DTO>(loReturn.Data);
                if (loGridList.Count == 0)
                {
                    Test = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSpinnerYear()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _PMT01800Model.GetInitDayStreamAsync();
                loInitSpinnerYear = (loReturn);
                // loInitialProcessParameter.CSOFT_PERIOD_YY = (yearPeriod.ToString());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}