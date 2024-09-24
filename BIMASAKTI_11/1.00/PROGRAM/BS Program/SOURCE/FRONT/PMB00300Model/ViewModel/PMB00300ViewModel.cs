using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMB00300Common;
using PMB00300Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMB00300Model.ViewModel
{
    public class PMB00300ViewModel : R_ViewModel<PMB00300RecalcDTO>
    {
        private PMB00300Model _model = new PMB00300Model();
        public ObservableCollection<PMB00300RecalcDTO> GridList = new ObservableCollection<PMB00300RecalcDTO>();
        public PMB00300RecalcDTO Entity = new PMB00300RecalcDTO();

        public List<PMB00300PropertyDTO> PropertyList = new List<PMB00300PropertyDTO>();
        public PMB00300PropertyDTO Property = new PMB00300PropertyDTO();

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<PMB00300ListDTO<PMB00300PropertyDTO>>(
                        nameof(IPMB00300.PMB00300GetPropertyList));
                PropertyList = loReturn.Data;
                Property.CPROPERTY_ID = PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : Property.CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRecalcList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CPROPERTY_ID, Property.CPROPERTY_ID);
                var loReturn =
                    await _model.GetListStreamAsync<PMB00300RecalcDTO>(
                        nameof(IPMB00300.PMB00300GetRecalcListStream));


                // for (int i = 0; i < 50; i++)
                // {
                //     loReturn.Add(new PMB00300RecalcDTO
                //     {
                //         CPROPERTY_ID = "Property ID " + i,
                //         CPROPERTY_NAME = "Property Name " + i,
                //         CTRANS_CODE = "Trans Code " + i,
                //         CDEPT_CODE = "Dept Code " + i,
                //         CDEPT_NAME = "Dept Name " + i,
                //         CREF_NO = "Ref No " + i,
                //         CTENANT_NAME = "Tenant Name " + i,
                //         IYEARS = 2023,
                //         IMONTHS = 14,
                //         IDAYS = 01,
                //         NGROSS_AREA_SIZE = 2000,
                //         NNET_AREA_SIZE = 1200,
                //         NACTUAL_AREA_SIZE = 1000,
                //         CBUILDING_ID = "Building ID " + i,
                //         CBUILDING_NAME = "Building Name " + i,
                //         CFLOOR_ID = "Floor ID " + i,
                //         CFLOOR_NAME = "Floor Name " + i,
                //         CUNIT_ID = "Unit ID " + i,
                //         CUNIT_NAME = "Unit Name " + i,
                //         CHO_PLAN_DATE = "20230101",
                //         CHO_ACTUAL_DATE = "20230101",
                //         CUPDATE_BY = "rhc",
                //         DUPDATE_DATE = DateTime.Now,
                //         CCREATE_BY = "rhc",
                //         DCREATE_DATE = DateTime.Now,
                //     });
                // }

                loReturn.ForEach(loItem =>
                {
                    loItem.DHO_PLAN_DATE = DateTime.TryParseExact(loItem.CHO_PLAN_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldStartDate)
                        ? ldStartDate
                        : (DateTime?)null;

                    loItem.DHO_ACTUAL_DATE = DateTime.TryParseExact(loItem.CHO_ACTUAL_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldEndDate)
                        ? ldEndDate
                        : (DateTime?)null;
                });

                //buat 20 dummy data untuk loReturn

                GridList = new ObservableCollection<PMB00300RecalcDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}