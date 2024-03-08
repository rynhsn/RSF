using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using LMT03500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Model.ViewModel
{
    public class LMT03500UtilityUsageViewModel : R_ViewModel<LMT03500BuildingDTO>
    {
        private LMT03500UtilityUsageModel _model = new LMT03500UtilityUsageModel();

        public ObservableCollection<LMT03500BuildingDTO> GridBuildingList =
            new ObservableCollection<LMT03500BuildingDTO>();

        public ObservableCollection<LMT03500UtilityUsageDTO> GridUtilityUsageList =
            new ObservableCollection<LMT03500UtilityUsageDTO>();

        public LMT03500BuildingDTO Entity = new LMT03500BuildingDTO();
        public LMT03500UtilityUsageDTO EntityUtility = new LMT03500UtilityUsageDTO();

        public List<LMT03500FunctDTO> UtilityTypeList = new List<LMT03500FunctDTO>();
        public List<LMT03500FloorDTO> FloorList = new List<LMT03500FloorDTO>();
        public List<LMT03500PeriodDTO> InvPeriodList = new List<LMT03500PeriodDTO>();
        public List<LMT03500PeriodDTO> UtilityPeriodList = new List<LMT03500PeriodDTO>();

        public string PropertyId = string.Empty;
        public string TransCodeId = string.Empty;

        public string UtilityTypeId = string.Empty;
        public string FloorId = string.Empty;
        public string InvPeriodYear = string.Empty;
        public string InvPeriodNo = string.Empty;
        public string UtilityPeriodYear = string.Empty;
        public string UtilityPeriodNo = string.Empty;
        public bool AllFloor = false;
        public bool Invoiced = false;
        public string UtilityPeriodFromDt = string.Empty;
        public string UtilityPeriodToDt = string.Empty;
        public DateTime UtilityPeriodFromDtDt = DateTime.Now;
        public DateTime UtilityPeriodToDtDt = DateTime.Now;

        public async Task Init(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                PropertyId = poParam.ToString();
                await GetUtilityTypeList();
                await GetPeriodList();
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
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CPROPERTY_ID, PropertyId);
                var loReturn = await _model.GetListStreamAsync<LMT03500BuildingDTO>(nameof(ILMT03500UtilityUsage
                        .LMT03500GetBuildingListStream));

                GridBuildingList = new ObservableCollection<LMT03500BuildingDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingRecord(LMT03500BuildingDTO poData)
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
                
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CPROPERTY_ID, PropertyId);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CBUILDING_ID, Entity.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CUTILITY_TYPE, UtilityTypeId);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CFLOOR_LIST, FloorId);
                //LALL_FLOOR
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.LALL_FLOOR, AllFloor);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CINVOICE_PRD, InvPeriodNo);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.LINVOICED, Invoiced);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CUTILITY_PRD, UtilityPeriodNo);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CUTILITY_PRD_FROM_DATE,
                    UtilityPeriodFromDt);
                R_FrontContext.R_SetStreamingContext(LMT03500ContextConstant.CUTILITY_PRD_TO_DATE, UtilityPeriodToDt);

                var loReturn =
                    await _model.GetListStreamAsync<LMT03500UtilityUsageDTO>(nameof(ILMT03500UtilityUsage
                        .LMT03500GetUtilityUsageListStream));
                GridUtilityUsageList = new ObservableCollection<LMT03500UtilityUsageDTO>(loReturn);
                
                //buat dummy data 20
                // for (int i = 0; i < 20; i++)
                // {
                //     GridUtilityUsageList.Add(new LMT03500UtilityUsageDTO()
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
                    await _model.GetAsync<LMT03500ListDTO<LMT03500FunctDTO>>(nameof(ILMT03500UtilityUsage
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
                var loParam = new LMT03500FloorParam
                {
                    CPROPERTY_ID = PropertyId,
                    CBUILDING_ID = Entity.CBUILDING_ID
                };
                var loReturn = await _model.GetAsync<LMT03500ListDTO<LMT03500FloorDTO>, LMT03500FloorParam>(nameof(ILMT03500UtilityUsage.LMT03500GetFloorList), loParam);
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
                var loParam = new LMT03500PeriodParam { CYEAR = lcYear.Year.ToString() };

                var loReturn =
                    await _model.GetAsync<LMT03500ListDTO<LMT03500PeriodDTO>, LMT03500PeriodParam>(
                        nameof(ILMT03500UtilityUsage.LMT03500GetPeriodList), loParam);

                InvPeriodList = loReturn.Data;
                UtilityPeriodList = loReturn.Data;
                InvPeriodYear = InvPeriodList.FirstOrDefault()?.CPERIOD;
                InvPeriodNo = InvPeriodList.FirstOrDefault()?.CPERIOD_NO;
                UtilityPeriodYear = UtilityPeriodList.FirstOrDefault()?.CPERIOD;
                UtilityPeriodNo = UtilityPeriodList.FirstOrDefault()?.CPERIOD_NO;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}