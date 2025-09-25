using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMT01800COMMON;
using PMT01800COMMON.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMT01810Model
{
    public class PMT01810ViewModel : R_ViewModel<PMT01810DTO>
    {
        private PMT01800Model.Model.PMT01810Model _PMT01810Model = new PMT01800Model.Model.PMT01810Model();

        public ObservableCollection<PMT01810DTO> loGridList = new ObservableCollection<PMT01810DTO>();
        public ObservableCollection<PMT01810DTO> LoGridListDetail = new ObservableCollection<PMT01810DTO>();


        public DateTime? OfferDate = DateTime.Now;
        public string TransCode = "";
        public string DeptCode = "";
        public string RefNo = "";
        public string BuildingName;

        public string OfferDateString;
        public string LOC = "";
        public string LOO = "";
        public string propertyValue = "";


        public async Task GetGridListHeader(PMT01800DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT01800.CLOC_TRANS_CODE,
                    string.IsNullOrWhiteSpace(LOC) ? "802053" : LOC);

                var loReturn = await _PMT01810Model.GetPMT01810StreamAsync();
                // for (int a = 1; a < 50; a++)
                // {
                //     loReturn.Data.Add(new PMT01810DTO()
                //     {
                //         CCOMPANY_ID = "CCOMPANY_ID" + a,
                //         CTRANS_CODE = "CTRANS_CODE" + a,
                //         CDEPT_CODE = "CDEPT_CODE" + a,
                //         CTENANT_ID = "CTENANT_ID" + a,
                //         CSALESMAN_ID = "CSALESMAN_ID" + a,
                //         CTRANS_STATUS = "CTRANS_STATUS" + a,
                //         CAGREEMENT_STATUS = "CAGREEMENT_STATUS" + a,
                //         CDEPT_NAME = "CDEPT_NAME" + a,
                //         CREF_NO = "CREF_NO" + a,
                //         CREF_DATE = "20230511",
                //         CTENANT_NAME = "CTENANT_NAME" + a,
                //         CSALESMAN_NAME = "CSALESMAN_NAME" + a,
                //         CTRANS_STATUS_DESC = "CTRANS_STATUS_DESC" + a,
                //         CAGREEMENT_STATUS_DESC = "CAGREEMENT_STATUS_DESC" + a,
                //
                //     });
                // }
                loReturn.Data.ForEach(x =>
                {
                    x.CREF_DATE_DISPLAY = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate)
                        ? ldRefDate
                        : (DateTime?)null;
                });

                loGridList = new ObservableCollection<PMT01810DTO>(loReturn.Data);
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
                var loReturn = await _PMT01810Model.GetPMT01810DetailStreamAsync();
                // for (int a = 1; a < 50; a++)
                // {
                //     loReturn.Data.Add(new PMT01810DTO()
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

                LoGridListDetail = new ObservableCollection<PMT01810DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Delete(PMT01810DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                // poEntity.CPROPERTY_ID
                poEntity.CDEPT_CODE = DeptCode;
                poEntity.CTRANS_CODE = string.IsNullOrEmpty(LOC) ? "802053" : LOC;

                poEntity.CREF_NO = RefNo;
                await _PMT01810Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}