using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMB00300Common;
using PMB00300Common.DTOs;
using PMB00300Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMB00300Model.ViewModel
{
    public class PMB00300ViewModelPopupView : R_ViewModel<PMB00300RecalcChargesDTO>
    {
        private PMB00300Model _model = new PMB00300Model();

        public ObservableCollection<PMB00300RecalcChargesDTO> GridChargesList =
            new ObservableCollection<PMB00300RecalcChargesDTO>();

        public ObservableCollection<PMB00300RecalcRuleDTO> GridRuleList =
            new ObservableCollection<PMB00300RecalcRuleDTO>();

        public PMB00300RecalcDTO Header = new PMB00300RecalcDTO();
        public PMB00300RecalcChargesDTO ChargesEntity = new PMB00300RecalcChargesDTO();

        public async Task GetRecalcChargesList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CPROPERTY_ID, Header.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CDEPT_CODE, Header.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CTRANS_CODE, Header.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CREF_NO, Header.CREF_NO);

                var loReturn =
                    await _model.GetListStreamAsync<PMB00300RecalcChargesDTO>(
                        nameof(IPMB00300.PMB00300GetRecalcChargesListStream));

                // for (int i = 0; i < 50; i++)
                // {
                //     loReturn.Add(new PMB00300RecalcChargesDTO
                //     {
                //         CSEQ_NO = "Seq No " + i,
                //         CCHARGES_TYPE = "Charges Type " + i,
                //         CCHARGES_TYPE_NAME = "Charges Type Name " + i,
                //         CCHARGES_ID = "Charges ID " + i,
                //         CCHARGES_NAME = "Charges Name " + i,
                //         CSTART_DATE = "20230101",
                //         CEND_DATE = "20230101",
                //         IINTERVAL = 1,
                //         CPERIOD_MODE = "Period Mode " + i,
                //         CPERIOD_MODE_DESCRIPTION = "Period Mode Description " + i,
                //         NBASE_AMOUNT = 1000,
                //         NMONTHLY_AMOUNT = 1000,
                //         NTOTAL_AMOUNT = 1000,
                //         CUPDATE_BY = "rhc",
                //         DUPDATE_DATE = DateTime.Now,
                //         CCREATE_BY = "rhc",
                //         DCREATE_DATE = DateTime.Now,
                //     });
                // }


                loReturn.ForEach(loItem =>
                {
                    loItem.DSTART_DATE = DateTime.TryParseExact(loItem.CSTART_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldStartDate)
                        ? ldStartDate
                        : (DateTime?)null;

                    loItem.DEND_DATE = DateTime.TryParseExact(loItem.CEND_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldEndDate)
                        ? ldEndDate
                        : (DateTime?)null;
                });

                //buat 20 dummy data untuk loReturn

                GridChargesList = new ObservableCollection<PMB00300RecalcChargesDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRecalcRuleList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CPROPERTY_ID, Header.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CDEPT_CODE, Header.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CTRANS_CODE, Header.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CREF_NO, Header.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CBUILDING_ID, Header.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CFLOOR_ID, Header.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CUNIT_ID, Header.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(PMB00300ContextConstant.CCHARGES_ID, ChargesEntity.CCHARGES_ID);

                var loReturn =
                    await _model.GetListStreamAsync<PMB00300RecalcRuleDTO>(
                        nameof(IPMB00300.PMB00300GetRecalcRuleListStream));

                // for (int i = 0; i < 2; i++)
                // {
                //     loReturn.Add(new PMB00300RecalcRuleDTO
                //     {
                //         CSEQ_NO = "Seq No " + i,
                //         CSTART_DATE = "20230101",
                //         CEND_DATE = "20230101",
                //         CPERIOD = "Period " + i,
                //         NAMOUNT_BEFORE = 1000,
                //         NTAX_AMOUNT_BEFORE = 1000,
                //         NAFTER_TAX_AMOUNT_BEFORE = 1000,
                //         NBOOKING_AMOUNT_BEFORE = 1000,
                //         NTOTAL_AMOUNT_BEFORE = 1000,
                //         NAMOUNT_AFTER = 1000,
                //         NTAX_AMOUNT_AFTER = 1000,
                //         NAFTER_TAX_AMOUNT_AFTER = 1000,
                //         NBOOKING_AMOUNT_AFTER = 1000,
                //         NTOTAL_AMOUNT_AFTER = 1000,
                //     });
                // }

                loReturn.ForEach(loItem =>
                {
                    loItem.DSTART_DATE = DateTime.TryParseExact(loItem.CSTART_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldStartDate)
                        ? ldStartDate
                        : (DateTime?)null;

                    loItem.DEND_DATE = DateTime.TryParseExact(loItem.CEND_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldEndDate)
                        ? ldEndDate
                        : (DateTime?)null;
                    
                    if (!string.IsNullOrEmpty(loItem.CPERIOD))
                    {
                        loItem.CPERIOD_DISPLAY = loItem.CPERIOD.Substring(0, 4) + "-" + loItem.CPERIOD.Substring(4, 2);;
                    }
                });


                GridRuleList = new ObservableCollection<PMB00300RecalcRuleDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<bool> RecalcBillingRuleProcess()
        {
            var loEx = new R_Exception();
            var loReturn = false;
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMB00300RecalcProcessParam>(Header);
                // var loResult = await _model.GetAsync<PMB00300SingleDTO<bool>, PMB00300RecalcProcessParam>(
                // nameof(IPMB00300.PMB00300GetRecalcRuleListStream), loParam);

                var loResult = await _model.PMB00300RecalcBillingRuleProcessModel(loParam);
                loReturn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
    }
}