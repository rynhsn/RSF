using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Model.ViewModel
{
    public class PMT03500UpdateMeterViewModel : R_ViewModel<PMT03500UtilityMeterDetailDTO>
    {
        private PMT03500UpdateMeterModel _model = new PMT03500UpdateMeterModel();
        private PMT03500UtilityUsageModel _modelUtility = new PMT03500UtilityUsageModel();

        public PMT03500UpdateMeterHeader Header = new PMT03500UpdateMeterHeader();

        public ObservableCollection<PMT03500UtilityMeterDTO> GridList =
            new ObservableCollection<PMT03500UtilityMeterDTO>();

        public PMT03500UtilityMeterDetailDTO Entity = new PMT03500UtilityMeterDetailDTO();

        public EPMT03500UtilityUsageType UtilityType;

        public List<PMT03500YearDTO> StartInvYearList = new List<PMT03500YearDTO>();
        public List<PMT03500PeriodDTO> StartInvMonthList = new List<PMT03500PeriodDTO>();
        public List<PMT03500FunctDTO> UtilityTypeList = new List<PMT03500FunctDTO>();
        public List<PMT03500MeterNoDTO> MeterNoList = new List<PMT03500MeterNoDTO>();

        public string DisplayEC = "d-none";

        public string DisplayWG = "d-none";
        public string CSTART_INV_PRD_YEAR { get; set; }
        public string CSTART_INV_PRD_MONTH { get; set; }
        public bool LOTHER_UNIT { get; set; } = false;

        public async Task Init(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                Header.CPROPERTY_ID = poParam.ToString();
                await GetPeriodList();
                await GetYearList();
                await GetUtilityTypeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetList()
        {
            var loEx = new R_Exception();
            try
            {
                // var loParam = (PMT03500UpdateMeterHeader)poParam;
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID, Header.CPROPERTY_ID ?? "");
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CBUILDING_ID, Header.CBUILDING_ID ?? "");

                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CDEPT_CODE, Header.CDEPT_CODE ?? "");
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CTRANS_CODE, Header.CTRANS_CODE ?? "");
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CREF_NO, Header.CREF_NO ?? "");
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CUNIT_ID, Header.CUNIT_ID ?? "");
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CFLOOR_ID, Header.CFLOOR_ID ?? "");
                // R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CTENANT_ID, "");
                var loReturn =
                    await _model.GetListStreamAsync<PMT03500UtilityMeterDTO>(nameof(IPMT03500UpdateMeter
                        .PMT03500GetUtilityMeterListStream));
                GridList = new ObservableCollection<PMT03500UtilityMeterDTO>(loReturn);

                //dummy data grid list, 10 data
                // for (int i = 0; i < 10; i++)
                // {
                //     GridList.Add(new PMT03500UtilityMeterDTO
                //     {
                //         CUTILITY_TYPE = "01",
                //         CUTILITY_TYPE_NAME = "Electricity",
                //         CCHARGES_ID = "C000"+i,
                //         CCHARGES_NAME = "Electricity",
                //         CMETER_NO = "M000"+i,
                //         CPROPERTY_ID = "P000"+i,
                //         CBUILDING_ID = "B000"+i,
                //         CFLOOR_ID = "F000"+i,
                //         CUNIT_ID = "U000"+i,
                //         CREF_NO = "R000"+i,
                //         CSTATUS = "A",
                //         CUPDATE_BY = "U000"+i,
                //         DUPDATE_DATE = DateTime.Now,
                //         CCREATE_BY = "C000"+i,
                //         DCREATE_DATE = DateTime.Now
                //     });
                // }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRecord(PMT03500UtilityMeterDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UtilityMeterDetailParam()
                {
                    // CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    // CBUILDING_ID = poEntity.CBUILDING_ID,
                    // CUNIT_ID = poEntity.CUNIT_ID,
                    // CCHARGES_TYPE = poEntity.CCHARGES_TYPE,
                    // CCHARGES_ID = poEntity.CCHARGES_ID

                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CTRANS_CODE = poEntity.CTRANS_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CUNIT_ID = poEntity.CUNIT_ID,
                    CFLOOR_ID = poEntity.CFLOOR_ID,
                    CBUILDING_ID = poEntity.CBUILDING_ID,
                    CCHARGES_TYPE = poEntity.CCHARGES_TYPE,
                    CCHARGES_ID = poEntity.CCHARGES_ID,
                    CSEQ_NO = poEntity.CSEQ_NO
                };

                var loReturn =
                    await _model
                        .GetAsync<PMT03500SingleDTO<PMT03500UtilityMeterDetailDTO>, PMT03500UtilityMeterDetailParam>(
                            nameof(IPMT03500UpdateMeter.PMT03500GetUtilityMeterDetail), loParam);
                Entity = loReturn.Data;

                if (!string.IsNullOrEmpty(Entity.CSTART_INV_PRD))
                {
                    CSTART_INV_PRD_YEAR = Entity.CSTART_INV_PRD.Substring(0, 4);
                    CSTART_INV_PRD_MONTH = Entity.CSTART_INV_PRD.Substring(4, 2);
                }

                if (Entity.CCHARGES_TYPE == "01" || Entity.CCHARGES_TYPE == "02")
                {
                    UtilityType = EPMT03500UtilityUsageType.EC;
                }
                else if (Entity.CCHARGES_TYPE == "03" || Entity.CCHARGES_TYPE == "04")
                {
                    UtilityType = EPMT03500UtilityUsageType.WG;
                }

                DisplayEC = UtilityType == EPMT03500UtilityUsageType.EC ? "d-block" : "d-none";
                DisplayWG = UtilityType == EPMT03500UtilityUsageType.WG ? "d-block" : "d-none";

                // Entity.DSTART_DATE_UPDATE = DateTime.ParseExact("yyyyMMdd", Entity.CSTART_DATE, CultureInfo.InvariantCulture);

                if (DateTime.TryParseExact(Entity.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldStartDate))
                {
                    Entity.DSTART_DATE_UPDATE = ldStartDate;
                }
                else
                {
                    Entity.DSTART_DATE_UPDATE = null;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementTenant(PMT03500UpdateMeterHeader poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500AgreementUtilitiesParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CBUILDING_ID = poParam.CBUILDING_ID,
                    CUNIT_ID = poParam.CUNIT_ID,
                    CFLOOR_ID = poParam.CFLOOR_ID,
                    LOTHER_UNIT = LOTHER_UNIT
                };

                var loReturn =
                    await _model
                        .GetAsync<PMT03500SingleDTO<PMT03500AgreementUtilitiesDTO>, PMT03500AgreementUtilitiesParam>(
                            nameof(IPMT03500UpdateMeter.PMT03500GetAgreementUtilities), loParam);

                if (loReturn.Data != null)
                {
                    Header.CPROPERTY_ID = loReturn.Data.CPROPERTY_ID;
                    Header.CDEPT_CODE = loReturn.Data.CDEPT_CODE;
                    Header.CTRANS_CODE = loReturn.Data.CTRANS_CODE;
                    Header.CREF_NO = loReturn.Data.CREF_NO;
                    Header.CFLOOR_ID = loReturn.Data.CFLOOR_ID;
                    Header.CBUILDING_ID = loReturn.Data.CBUILDING_ID;
                    Header.CTENANT_ID = loReturn.Data.CTENANT_ID;
                    Header.CTENANT_NAME = loReturn.Data.CTENANT_NAME;
                }
                else
                {
                    Header.CREF_NO = "";
                    Header.CTENANT_ID = "";
                    Header.CTENANT_NAME = "";
                }

                // Header = R_FrontUtility.ConvertObjectToObject<PMT03500UpdateMeterHeader>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateMeterNo(PMT03500UtilityMeterDetailDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UpdateChangeMeterNoParam
                {
                    // EUTILITY_TYPE = UtilityType,
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CREF_NO = poParam.CREF_NO,
                    CUNIT_ID = poParam.CUNIT_ID,
                    // CTENANT_ID = poParam.CTENANT_ID,
                    CDEPT_CODE = poParam.CDEPT_CODE,
                    CTRANS_CODE = poParam.CTRANS_CODE,
                    CFLOOR_ID = poParam.CFLOOR_ID,
                    CBUILDING_ID = poParam.CBUILDING_ID,
                    CCHARGES_TYPE = poParam.CCHARGES_TYPE,
                    CMETER_NO = poParam.CMETER_NO,
                    NBLOCK1_START = poParam.NBLOCK1_START,
                    NBLOCK2_START = poParam.NBLOCK2_START,
                    NMETER_START = poParam.NMETER_START,
                    CSTART_INV_PRD = poParam.CSTART_INV_PRD,
                    CSTART_DATE = poParam.DSTART_DATE_UPDATE?.ToString("yyyyMMdd")
                };

                await _model.GetAsync<PMT03500UtilityMeterDetailDTO, PMT03500UpdateChangeMeterNoParam>(
                    nameof(IPMT03500UpdateMeter.PMT03500UpdateMeterNo), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ChangeMeterNo(PMT03500UtilityMeterDetailDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500UpdateChangeMeterNoParam
                {
                    CPROPERTY_ID = poParam.CPROPERTY_ID,
                    CDEPT_CODE = poParam.CDEPT_CODE,
                    CTRANS_CODE = poParam.CTRANS_CODE,
                    CREF_NO = poParam.CREF_NO,
                    CUNIT_ID = poParam.CUNIT_ID,
                    CFLOOR_ID = poParam.CFLOOR_ID,
                    CBUILDING_ID = poParam.CBUILDING_ID,
                    CTENANT_ID = poParam.CTENANT_ID,
                    CCHARGES_TYPE = poParam.CCHARGES_TYPE,
                    CCHARGES_ID = poParam.CCHARGES_ID,
                    CMETER_NO = poParam.CMETER_NO,
                    NMETER_END = poParam.NMETER_END,
                    NBLOCK1_END = poParam.NBLOCK1_END,
                    NBLOCK2_END = poParam.NBLOCK2_END,
                    CTO_METER_NO = poParam.CTO_METER_NO,
                    NMETER_START = poParam.NTO_METER_START,
                    NBLOCK1_START = poParam.NTO_BLOCK1_START,
                    NBLOCK2_START = poParam.NTO_BLOCK2_START,
                    CSTART_INV_PRD = poParam.CSTART_INV_PRD,
                    CSTART_DATE = poParam.DSTART_DATE_CHANGE?.ToString("yyyyMMdd"),

                    // CPROPERTY_ID = poParam.CPROPERTY_ID,
                    // CREF_NO = poParam.CREF_NO,
                    // CUNIT_ID = poParam.CUNIT_ID,
                    // CTENANT_ID = poParam.CTENANT_ID,
                    // CCHARGES_TYPE = poParam.CCHARGES_TYPE,
                    // CMETER_NO = poParam.CMETER_NO,
                    // CTO_METER_NO = poParam.CTO_METER_NO,
                    // IMETER_START = poParam.IMETER_START,
                    // IMETER_END = poParam.IMETER_END,
                    // IBLOCK1_START = poParam.IBLOCK1_START,
                    // IBLOCK1_END = poParam.IBLOCK1_END,
                    // IBLOCK2_START = poParam.IBLOCK2_START,
                    // IBLOCK2_END = poParam.IBLOCK2_END,
                    // CSTART_INV_PRD = poParam.CSTART_INV_PRD,
                    // CSTART_DATE = poParam.DSTART_DATE_CHANGE.ToString()
                };

                await _model.GetAsync<PMT03500UtilityMeterDetailDTO, PMT03500UpdateChangeMeterNoParam>(
                    nameof(IPMT03500UpdateMeter.PMT03500ChangeMeterNo), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodList()
        {
            var loEx = new R_Exception();
            try
            {
                var lcYear = DateTime.Now;
                var loParam = new PMT03500PeriodParam { CYEAR = lcYear.Year.ToString() };

                var loReturn =
                    await _modelUtility.GetAsync<PMT03500ListDTO<PMT03500PeriodDTO>, PMT03500PeriodParam>(
                        nameof(IPMT03500UtilityUsage.PMT03500GetPeriodList), loParam);
                StartInvMonthList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetYearList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _modelUtility.GetAsync<PMT03500ListDTO<PMT03500YearDTO>>(
                        nameof(IPMT03500UtilityUsage.PMT03500GetYearList));
                StartInvYearList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodRangeList()
        {
            var loEx = new R_Exception();
            try
            {
                var ldDate = DateTime.Now;
                var loParam = new PMT03500PeriodRangeParam()
                {
                    CFROM_PERIOD = ldDate.ToString("yyyyMM"),
                    CTO_PERIOD = ldDate.AddMonths(1).ToString("yyyyMM")
                    // CFROM_PERIOD = "202410",
                    // CTO_PERIOD = "202503"
                };

                var loReturn =
                    await _model.GetAsync<PMT03500ListDTO<PMT03500PeriodRangeDTO>, PMT03500PeriodRangeParam>(
                        nameof(IPMT03500UpdateMeter.PMT03500GetPeriodRangeList), loParam);
                // ambil list dari loReturn.data.ccyear untuk dimasukkan ke StartInvYearList kemudian group by year
                StartInvYearList = loReturn.Data.GroupBy(x => x.CCYEAR).Select(x => new PMT03500YearDTO
                {
                    CYEAR = x.Key
                }).ToList();

                //ambil list dari loReturn.data.cperiod_no untuk dimasukkan ke StartInvMonthList
                StartInvMonthList = loReturn.Data.Select(x => new PMT03500PeriodDTO
                {
                    CPERIOD_NO = x.CPERIOD_NO
                }).ToList();

                CSTART_INV_PRD_YEAR = StartInvYearList.FirstOrDefault()?.CYEAR;
                CSTART_INV_PRD_MONTH = StartInvMonthList.FirstOrDefault()?.CPERIOD_NO;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetMeterNoList(PMT03500UtilityMeterDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT03500MeterNoParam>(poParam);
                //var loParam = new PMT03500MeterNoParam
                //{
                //    CPROPERTY_ID = poParam.CPROPERTY_ID,
                //    CBUILDING_ID = poParam.CBUILDING_ID,
                //    CFLOOR_ID = poParam.CFLOOR_ID,
                //    CUNIT_ID = poParam.CUNIT_ID,
                //    CCHARGES_TYPE = poParam.CCHARGES_TYPE
                //};

                var loReturn =
                    await _model.GetAsync<PMT03500ListDTO<PMT03500MeterNoDTO>, PMT03500MeterNoParam>(
                        nameof(IPMT03500UpdateMeter.PMT03500GetMeterNoList), loParam);
                // MeterNoList = loReturn.Data;
                //hanya masukkan meter no selain _viewModel.Entity.CMETER_NO
                MeterNoList = loReturn.Data.Where(x => x.CMETER_NO != Entity.CMETER_NO).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUtilityTypeList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _modelUtility.GetAsync<PMT03500ListDTO<PMT03500FunctDTO>>(
                        nameof(IPMT03500UtilityUsage.PMT03500GetUtilityTypeList));
                UtilityTypeList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}