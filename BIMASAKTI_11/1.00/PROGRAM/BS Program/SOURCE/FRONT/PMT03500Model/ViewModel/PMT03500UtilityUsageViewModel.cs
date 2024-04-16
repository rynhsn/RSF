using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public class PMT03500UtilityUsageViewModel : R_ViewModel<PMT03500BuildingDTO>
    {
        private PMT03500UtilityUsageModel _model = new PMT03500UtilityUsageModel();

        public ObservableCollection<PMT03500BuildingDTO> GridBuildingList =
            new ObservableCollection<PMT03500BuildingDTO>();

        public ObservableCollection<PMT03500UtilityUsageDTO> GridUtilityUsageList =
            new ObservableCollection<PMT03500UtilityUsageDTO>();

        public PMT03500BuildingDTO Entity = new PMT03500BuildingDTO();
        public PMT03500UtilityUsageDTO EntityUtility = new PMT03500UtilityUsageDTO();
        public PMT03500PropertyDTO Property = new PMT03500PropertyDTO();

        public List<PMT03500FunctDTO> UtilityTypeList = new List<PMT03500FunctDTO>();
        public List<PMT03500FloorDTO> FloorList = new List<PMT03500FloorDTO>();
        public List<PMT03500YearDTO> InvYearList = new List<PMT03500YearDTO>();
        public List<PMT03500PeriodDTO> InvPeriodList = new List<PMT03500PeriodDTO>();
        public List<PMT03500YearDTO> UtilityYearList = new List<PMT03500YearDTO>();
        public List<PMT03500PeriodDTO> UtilityPeriodList = new List<PMT03500PeriodDTO>();

        // public string PropertyId = string.Empty;
        public string TransCodeId = string.Empty;

        public string UtilityTypeId = string.Empty;
        public EPMT03500UtilityUsageType UtilityType;

        public string FloorId = string.Empty;
        public bool AllFloor = false;

        public string InvPeriodYear = string.Empty;
        public string InvPeriodNo = string.Empty;
        public bool Invoiced = false;

        public string UtilityPeriodYear = string.Empty;
        public string UtilityPeriodNo = string.Empty;

        public string UtilityPeriodFromDt = string.Empty;
        public string UtilityPeriodToDt = string.Empty;

        public DateTime UtilityPeriodFromDtDt = DateTime.Now;
        public DateTime UtilityPeriodToDtDt = DateTime.Now;

        public DataSet ExcelDataSetCutOff { get; set; }
        public DataSet ExcelDataSetUtility { get; set; }

        // public Func<Task> ActionDataSetExcel { get; set; }
        public async Task Init(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                Property = (PMT03500PropertyDTO)poParam;
                // PropertyId = poParam.ToString();
                // PropertyId = Property.CPROPERTY_ID;
                await GetUtilityTypeList();
                await GetPeriodList();
                await GetYearList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingList()
        {
            var loEx = new R_Exception();
            try
            {
                // R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID, PropertyId);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID, Property.CPROPERTY_ID);
                var loReturn = await _model.GetListStreamAsync<PMT03500BuildingDTO>(nameof(IPMT03500UtilityUsage
                    .LMT03500GetBuildingListStream));

                GridBuildingList = new ObservableCollection<PMT03500BuildingDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingRecord(PMT03500BuildingDTO poData)
        {
            var loEx = new R_Exception();
            try
            {
                Entity = poData;
                if (Entity != null)
                {
                    await GetFloorList();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUtilityUsageList()
        {
            var loEx = new R_Exception();
            try
            {
                // ubah utility period from dan to date ke string yyyymmdd
                UtilityPeriodFromDt = UtilityPeriodFromDtDt.ToString("yyyyMMdd");
                UtilityPeriodToDt = UtilityPeriodToDtDt.ToString("yyyyMMdd");

                // R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID, PropertyId);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CPROPERTY_ID, Property.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CBUILDING_ID, Entity.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CUTILITY_TYPE, UtilityTypeId);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CFLOOR_LIST, FloorId);
                //LALL_FLOOR
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.LALL_FLOOR, AllFloor);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CINVOICE_PRD, InvPeriodNo);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.LINVOICED, Invoiced);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CUTILITY_PRD, UtilityPeriodNo);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CUTILITY_PRD_FROM_DATE,
                    UtilityPeriodFromDt);
                R_FrontContext.R_SetStreamingContext(PMT03500ContextConstant.CUTILITY_PRD_TO_DATE, UtilityPeriodToDt);

                var loReturn =
                    await _model.GetListStreamAsync<PMT03500UtilityUsageDTO>(nameof(IPMT03500UtilityUsage
                        .LMT03500GetUtilityUsageListStream));
                GridUtilityUsageList = new ObservableCollection<PMT03500UtilityUsageDTO>(loReturn);
                
                ConvertToExcelUtility();
                ConvertToExcelCutOff();
                //buat dummy data 20
                // for (int i = 0; i < 20; i++)
                // {
                //     GridUtilityUsageList.Add(new PMT03500UtilityUsageDTO()
                //     {
                //         CFLOOR_ID = "Floor" + i,
                //         CUNIT_ID = "Unit" + i,
                //         CMETER_NO = "Meter" + i,
                //     });
                // }
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
                    await _model.GetAsync<PMT03500ListDTO<PMT03500FunctDTO>>(nameof(IPMT03500UtilityUsage
                        .LMT03500GetUtilityTypeList));
                UtilityTypeList = loReturn.Data;
                UtilityTypeId = UtilityTypeList.FirstOrDefault()?.CCODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetFloorList()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT03500FloorParam
                {
                    // CPROPERTY_ID = PropertyId,
                    CPROPERTY_ID = Property.CPROPERTY_ID,
                    CBUILDING_ID = Entity.CBUILDING_ID
                };
                var loReturn =
                    await _model.GetAsync<PMT03500ListDTO<PMT03500FloorDTO>, PMT03500FloorParam>(
                        nameof(IPMT03500UtilityUsage.LMT03500GetFloorList), loParam);
                FloorList = loReturn.Data;
                FloorId = FloorList.FirstOrDefault()?.CFLOOR_ID;
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
                    await _model.GetAsync<PMT03500ListDTO<PMT03500PeriodDTO>, PMT03500PeriodParam>(
                        nameof(IPMT03500UtilityUsage.LMT03500GetPeriodList), loParam);

                InvPeriodList = loReturn.Data;
                UtilityPeriodList = loReturn.Data;

                InvPeriodNo = InvPeriodList.FirstOrDefault()?.CPERIOD_NO;
                UtilityPeriodNo = UtilityPeriodList.FirstOrDefault()?.CPERIOD_NO;
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
                var lcYear = DateTime.Now;
                var loParam = new PMT03500PeriodParam { CYEAR = lcYear.Year.ToString() };

                var loReturn =
                    await _model.GetAsync<PMT03500ListDTO<PMT03500YearDTO>>(nameof(IPMT03500UtilityUsage
                        .LMT03500GetYearList));

                InvYearList = loReturn.Data;
                UtilityYearList = loReturn.Data;

                InvPeriodYear = loParam.CYEAR;
                UtilityPeriodYear = loParam.CYEAR;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMT03500ExcelDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            PMT03500ExcelDTO loResult = null;

            try
            {
                loResult = await _model.GetAsync<PMT03500ExcelDTO>(nameof(IPMT03500UtilityUsage
                    .LMT03500DownloadTemplateFile));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void ConvertToExcelCutOff()
        {
            if (UtilityType == EPMT03500UtilityUsageType.EC)
            {
                var loConvertData = GridUtilityUsageList.Select(item => new PMT03500CutOffExcelECDTO
                {
                    BuildingId = item.CBUILDING_ID,
                    Department = item.CDEPT_CODE,
                    AgreementNo = item.CREF_NO,
                    UtilityType = item.CUTILITY_TYPE,
                    FloorId = item.CFLOOR_ID,
                    UnitId = item.CUNIT_ID,
                    ChargesType = item.CCHARGES_TYPE,
                    ChargesId = item.CCHARGES_ID,
                    MeterNo = item.CMETER_NO,
                    SeqNo = item.CSEQ_NO,
                    InvoicePeriod = item.CINV_PRD,
                    UtilityPeriod = item.CUTILITY_PRD,
                    StartDate = item.CSTART_DATE,
                    EndDate = item.CEND_DATE,
                    BlockIStart = item.IBLOCK1_START,
                    BlockIIStart = item.IBLOCK2_START,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
                loDataTable.TableName = "UtilityUsage";

                var loDataSet = new DataSet();
                loDataSet.Tables.Add(loDataTable);

                // Asign Dataset
                ExcelDataSetCutOff = loDataSet;
            }
            else if (UtilityType == EPMT03500UtilityUsageType.WG)
            {
                var loConvertData = GridUtilityUsageList.Select(item => new PMT03500CutOffExcelWGDTO
                {
                    BuildingId = item.CBUILDING_ID,
                    Department = item.CDEPT_CODE,
                    AgreementNo = item.CREF_NO,
                    UtilityType = item.CUTILITY_TYPE,
                    FloorId = item.CFLOOR_ID,
                    UnitId = item.CUNIT_ID,
                    ChargesType = item.CCHARGES_TYPE,
                    ChargesId = item.CCHARGES_ID,
                    MeterNo = item.CMETER_NO,
                    SeqNo = item.CSEQ_NO,
                    InvoicePeriod = item.CINV_PRD,
                    UtilityPeriod = item.CUTILITY_PRD,
                    StartDate = item.CSTART_DATE,
                    EndDate = item.CEND_DATE,
                    MeterStart = item.IMETER_START,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
                loDataTable.TableName = "UtilityUsage";

                var loDataSet = new DataSet();
                loDataSet.Tables.Add(loDataTable);

                // Asign Dataset
                ExcelDataSetCutOff = loDataSet;
            }
        }

        public void ConvertToExcelUtility()
        {
            if (UtilityType == EPMT03500UtilityUsageType.EC)
            {
                var loConvertData = GridUtilityUsageList.Select(item => new PMT03500UtilityExcelECDTO
                {
                    BuildingId = item.CBUILDING_ID,
                    Department = item.CDEPT_CODE,
                    AgreementNo = item.CREF_NO,
                    UtilityType = item.CUTILITY_TYPE,
                    FloorId = item.CFLOOR_ID,
                    UnitId = item.CUNIT_ID,
                    ChargesType = item.CCHARGES_TYPE,
                    ChargesId = item.CCHARGES_ID,
                    MeterNo = item.CMETER_NO,
                    SeqNo = item.CSEQ_NO,
                    InvoicePeriod = item.CINV_PRD,
                    UtilityPeriod = item.CUTILITY_PRD,
                    StartDate = item.CSTART_DATE,
                    EndDate = item.CEND_DATE,
                    BlockIStart = item.IBLOCK1_START,
                    BlockIIStart = item.IBLOCK2_START,
                    BlockIEnd = item.IBLOCK1_END,
                    BlockIIEnd = item.IBLOCK2_END,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
                loDataTable.TableName = "UtilityUsage";

                var loDataSet = new DataSet();
                loDataSet.Tables.Add(loDataTable);

                // Asign Dataset
                ExcelDataSetUtility = loDataSet;
            }else if (UtilityType == EPMT03500UtilityUsageType.WG)
            {
                var loConvertData = GridUtilityUsageList.Select(item => new PMT03500UtilityExcelWGDTO
                {
                    BuildingId = item.CBUILDING_ID,
                    Department = item.CDEPT_CODE,
                    AgreementNo = item.CREF_NO,
                    UtilityType = item.CUTILITY_TYPE,
                    FloorId = item.CFLOOR_ID,
                    UnitId = item.CUNIT_ID,
                    ChargesType = item.CCHARGES_TYPE,
                    ChargesId = item.CCHARGES_ID,
                    MeterNo = item.CMETER_NO,
                    SeqNo = item.CSEQ_NO,
                    InvoicePeriod = item.CINV_PRD,
                    UtilityPeriod = item.CUTILITY_PRD,
                    StartDate = item.CSTART_DATE,
                    EndDate = item.CEND_DATE,
                    MeterStart = item.IMETER_START,
                    MeterEnd = item.IMETER_END
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
                loDataTable.TableName = "UtilityUsage";

                var loDataSet = new DataSet();
                loDataSet.Tables.Add(loDataTable);

                // Asign Dataset
                ExcelDataSetUtility = loDataSet;
            }
        }
    }
}