using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using PMI00300COMMON.DTO;
using PMI00300MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMI00300FRONT
{
    public partial class PMI00300 : R_Page
    {
        // SEARCH "GRID_RIGHT_DISABLED" TO FIND WHAT TO ENABLE IF YOU WANT TO SHOW GRID RIGHT
        // CR2 : "GRID RIGHT" BECOME NEW GRID IN "FACILITY" TAB
        [Inject] private R_ILocalizer<PMI00300FrontResources.Resources_Dummy_Class> _localizer { get; set; } = null!;

        private PMI00300ViewModel loViewModel = new();
        private R_ConductorGrid _conductorGridRef = new();
        private R_Grid<PMI00300DTO> _headerGridRef = new();
        private R_Grid<PMI00300DetailLeftDTO> _detailLeftGridRef = new();
        private R_Grid<PMI00300DetailRightDTO> _detailRightGridRef = new();
        private R_TabStrip _tabStrip;

        private bool IsAllBuildingEnabled = false;
        private bool IsAgreementEnabled = false;
        private int PageSizeGridHDUnit = 10;
        private int PageSizeGridDTUnit= 5;

        public class RadioButtonOption
        {
            public string CCODE { get; set; } = string.Empty;
            public string CNAME { get; set; } = string.Empty; 
        }

        private List<RadioButtonOption> loUnitOptionRadioButtonList = new()
        {
            new() { CCODE = "01", CNAME = "Unit" },
            new() { CCODE = "02", CNAME = "Other Unit"}
        };

        private List<RadioButtonOption> loUnitCategoryOptionRadioButtonList = new()
        {
            new() { CCODE = "01", CNAME = "Lease" },
            new() { CCODE = "02", CNAME = "Strata"}
        };

        private List<PMI00300GetGSBCodeListDTO> loStatusList = new();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.InitialProcess();

                loViewModel.loDisplayProcess.CUNIT_OPTION = "01";
                loViewModel.loDisplayProcess.LALL_BUILDING = false;
                IsAllBuildingEnabled = false;

                loViewModel.loDisplayProcess.CUNIT_CATEGORY = "01";
                if (loViewModel.loLeaseStatusList.Count == 0 )
                {
                    await loViewModel.GetLeaseStatusListAsync();
                }
                loStatusList = loViewModel.loLeaseStatusList;
                loViewModel.loStatus = loStatusList.FirstOrDefault()!;
                loViewModel.loDisplayProcess.CSTATUS_ID = loViewModel.loStatus.CCODE;
                loViewModel.loDisplayProcess.CSTATUS_NAME = loViewModel.loStatus.CNAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region DISPLAY PROCESS METHOD
        private async Task PropertyDropdown_ValueChanged(string pCPROPERTY_ID)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loDisplayProcess.CPROPERTY_ID = pCPROPERTY_ID;
                loViewModel.loProperty = loViewModel.loPropertyList.Where(x => x.CPROPERTY_ID == pCPROPERTY_ID).FirstOrDefault()!;
                loViewModel.loDisplayProcess.CBUILDING_ID = "";
                loViewModel.loDisplayProcess.CBUILDING_NAME = "";
                loViewModel.loDisplayProcess.CFROM_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_NAME = "";
                await UnitCategoryOptionRadioButton_ValueChanged(loViewModel.loDisplayProcess.CUNIT_CATEGORY);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void StatusDropdown_ValueChanged(string pCCODE)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loStatus = loStatusList.Find(x => x.CCODE == pCCODE)!;
                loViewModel.loDisplayProcess.CSTATUS_ID = loViewModel.loStatus.CCODE;
                loViewModel.loDisplayProcess.CSTATUS_NAME = loViewModel.loStatus.CNAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void UnitOptionRadioButton_ValueChanged(string pCUNIT_OPTION)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loDisplayProcess.CUNIT_OPTION = pCUNIT_OPTION;

                if (pCUNIT_OPTION == "01")
                {
                    loViewModel.loDisplayProcess.LALL_BUILDING = false;
                    IsAllBuildingEnabled = false;
                }
                if (pCUNIT_OPTION == "02")
                {
                    loViewModel.loDisplayProcess.LALL_BUILDING = true;
                    IsAllBuildingEnabled = true;
                }
                loViewModel.loDisplayProcess.CBUILDING_ID = "";
                loViewModel.loDisplayProcess.CBUILDING_NAME = "";
                loViewModel.loDisplayProcess.CFROM_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task UnitCategoryOptionRadioButton_ValueChanged(string pCUNIT_CATEGORY)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loDisplayProcess.CUNIT_CATEGORY = pCUNIT_CATEGORY;

                if (pCUNIT_CATEGORY == "01")
                {
                    await loViewModel.GetLeaseStatusListAsync();
                    loStatusList = loViewModel.loLeaseStatusList;
                    loViewModel.loStatus = loStatusList.FirstOrDefault()!;
                }
                if (pCUNIT_CATEGORY == "02")
                {
                    await loViewModel.GetStrataStatusListAsync();
                    loStatusList = loViewModel.loStrataStatusList;
                    loViewModel.loStatus = loStatusList.FirstOrDefault()!;
                };

                loViewModel.loDisplayProcess.CSTATUS_ID = loViewModel.loStatus.CCODE;
                loViewModel.loDisplayProcess.CSTATUS_NAME = loViewModel.loStatus.CNAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void AllBuildingCheckbox_ValueChanged(bool pLALL_BUILDING)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loDisplayProcess.LALL_BUILDING = pLALL_BUILDING;

                loViewModel.loDisplayProcess.CBUILDING_ID = "";
                loViewModel.loDisplayProcess.CBUILDING_NAME = "";
                loViewModel.loDisplayProcess.CBUILDING_ID = "";
                loViewModel.loDisplayProcess.CBUILDING_NAME = "";
                loViewModel.loDisplayProcess.CFROM_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_OnLostFocus_Building()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(loViewModel.loDisplayProcess.CBUILDING_ID))
                {
                    loViewModel.loDisplayProcess.CBUILDING_NAME = "";
                    return;
                }

                LookupGSL02200ViewModel lookupGSL02200ViewModel = new LookupGSL02200ViewModel();
                var loParam = new GSL02200ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loProperty.CPROPERTY_ID,
                    CSEARCH_TEXT = loViewModel.loDisplayProcess.CBUILDING_ID
                };
                var loResult = await lookupGSL02200ViewModel.GetBuilding(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loViewModel.loDisplayProcess.CBUILDING_ID = "";
                    loViewModel.loDisplayProcess.CBUILDING_NAME = "";
                    loViewModel.loDisplayProcess.CFROM_FLOOR_ID = "";
                    loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = "";
                    loViewModel.loDisplayProcess.CTO_FLOOR_ID = "";
                    loViewModel.loDisplayProcess.CTO_FLOOR_NAME = "";
                }
                else
                {
                    loViewModel.loDisplayProcess.CBUILDING_ID = loResult.CBUILDING_ID;
                    loViewModel.loDisplayProcess.CBUILDING_NAME = loResult.CBUILDING_NAME;
                    await GetFromFloor(false);
                    await GetToFloor(false);
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.loDisplayProcess.CPROPERTY_ID))
            {
                return;
            }
            GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
            {
                CPROPERTY_ID = loViewModel.loDisplayProcess.CPROPERTY_ID,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        private void R_After_Open_LookupBuilding(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02200DTO loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            
            if (loTempResult.CBUILDING_ID != loViewModel.loDisplayProcess.CBUILDING_ID)
            {
                loViewModel.loDisplayProcess.CBUILDING_ID = loTempResult.CBUILDING_ID;
                loViewModel.loDisplayProcess.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
                loViewModel.loDisplayProcess.CFROM_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_ID = "";
                loViewModel.loDisplayProcess.CTO_FLOOR_NAME = "";
            }
        }
        private async Task R_OnLostFocus_FromFloor()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await GetFromFloor(true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetFromFloor(bool showError = true)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(loViewModel.loDisplayProcess.CFROM_FLOOR_ID))
                {
                    loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = "";
                    return;
                }

                LookupGSL02400ViewModel lookupGSL02400ViewModel = new LookupGSL02400ViewModel();
                var loParam = new GSL02400ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loDisplayProcess.CPROPERTY_ID,
                    CBUILDING_ID = loViewModel.loDisplayProcess.CBUILDING_ID,
                    CSEARCH_TEXT = loViewModel.loDisplayProcess.CFROM_FLOOR_ID
                };
                var loResult = await lookupGSL02400ViewModel.GetFloor(loParam);

                if (loResult == null)
                {
                    if(showError) loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loViewModel.loDisplayProcess.CFROM_FLOOR_ID = "";
                    loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = "";
                }
                else
                {
                    loViewModel.loDisplayProcess.CFROM_FLOOR_ID = loResult.CFLOOR_ID;
                    loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = loResult.CFLOOR_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupFromFloor(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.loDisplayProcess.CPROPERTY_ID) || string.IsNullOrWhiteSpace(loViewModel.loDisplayProcess.CBUILDING_ID))
            {
                return;
            }
            GSL02400ParameterDTO loParam = new GSL02400ParameterDTO()
            {
                CPROPERTY_ID = loViewModel.loDisplayProcess.CPROPERTY_ID,
                CBUILDING_ID = loViewModel.loDisplayProcess.CBUILDING_ID,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02400);
        }
        private void R_After_Open_LookupFromFloor(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02400DTO loTempResult = (GSL02400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.loDisplayProcess.CFROM_FLOOR_ID = loTempResult.CFLOOR_ID;
            loViewModel.loDisplayProcess.CFROM_FLOOR_NAME = loTempResult.CFLOOR_NAME;
        }
        private async Task R_OnLostFocus_ToFloor()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await GetToFloor(true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetToFloor(bool showError = true)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(loViewModel.loDisplayProcess.CTO_FLOOR_ID))
                {
                    loViewModel.loDisplayProcess.CTO_FLOOR_NAME = "";
                    return;
                }

                LookupGSL02400ViewModel lookupGSL02400ViewModel = new LookupGSL02400ViewModel();
                var loParam = new GSL02400ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loDisplayProcess.CPROPERTY_ID,
                    CBUILDING_ID = loViewModel.loDisplayProcess.CBUILDING_ID,
                    CSEARCH_TEXT = loViewModel.loDisplayProcess.CTO_FLOOR_ID
                };
                var loResult = await lookupGSL02400ViewModel.GetFloor(loParam);

                if (loResult == null)
                {
                    if(showError) loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loViewModel.loDisplayProcess.CTO_FLOOR_ID = "";
                    loViewModel.loDisplayProcess.CTO_FLOOR_NAME = "";
                }
                else
                {
                    loViewModel.loDisplayProcess.CTO_FLOOR_ID = loResult.CFLOOR_ID;
                    loViewModel.loDisplayProcess.CTO_FLOOR_NAME = loResult.CFLOOR_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupToFloor(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.loDisplayProcess.CPROPERTY_ID) || string.IsNullOrWhiteSpace(loViewModel.loDisplayProcess.CBUILDING_ID))
            {
                return;
            }
            GSL02400ParameterDTO loParam = new GSL02400ParameterDTO()
            {
                CPROPERTY_ID = loViewModel.loDisplayProcess.CPROPERTY_ID,
                CBUILDING_ID = loViewModel.loDisplayProcess.CBUILDING_ID,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL02400);
        }
        private void R_After_Open_LookupToFloor(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02400DTO loTempResult = (GSL02400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.loDisplayProcess.CTO_FLOOR_ID = loTempResult.CFLOOR_ID;
            loViewModel.loDisplayProcess.CTO_FLOOR_NAME = loTempResult.CFLOOR_NAME;
        }
        #endregion

        #region BUTTON METHODS
        private async Task OnClickRefresh()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.RefreshUnitInquiryValidation();
                await _headerGridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void R_Before_Open_Detail_Agreement(R_BeforeOpenDetailEventArgs eventArgs)
        {
            eventArgs.Parameter = new PMI00300AgreementFormDetailParameterDTO()
            {
                CPROPERTY_ID = loViewModel.loSelectedUnitInquiryHeader.CPROPERTY_ID,
                CPROPERTY_NAME = loViewModel.loSelectedUnitInquiryHeader.CPROPERTY_NAME,
                CBUILDING_ID = loViewModel.loSelectedUnitInquiryHeader.CBUILDING_ID,
                CBUILDING_NAME = loViewModel.loSelectedUnitInquiryHeader.CBUILDING_NAME,
                CFLOOR_ID = loViewModel.loSelectedUnitInquiryHeader.CFLOOR_ID,
                CUNIT_ID = loViewModel.loSelectedUnitInquiryHeader.CUNIT_ID
            };
            eventArgs.TargetPageType = typeof(PMI00300AgreementForm);
        }
        private void R_After_Open_Detail_Agreement(R_AfterOpenDetailEventArgs eventArgs)
        {
        }

        #endregion

        #region GRID METHOD
        private async Task R_ServiceGetListRecord (R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetUnitInquiryHeaderListAsync();
                if (loViewModel.loUnitInquiryHeaderList.Count() == 0)
                {
                    loViewModel.loSelectedUnitInquiryHeader = new PMI00300DTO();
                    //await R_MessageBox.Show("", "No data found!", R_eMessageBoxButtonType.OK);
                    await _detailLeftGridRef.R_RefreshGrid(null);
                    IsAgreementEnabled = false;
                }
                else if (loViewModel.loUnitInquiryHeaderList.Count() > 0)
                {
                    loViewModel.loSelectedUnitInquiryHeader = loViewModel.loUnitInquiryHeaderList.FirstOrDefault()!;
                    IsAgreementEnabled = true;
                }
                eventArgs.ListEntityResult = loViewModel.loUnitInquiryHeaderList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                R_Exception loEx = new R_Exception();

                try
                {
                    loViewModel.loSelectedUnitInquiryHeader = (PMI00300DTO)eventArgs.Data;
                    await _detailLeftGridRef.R_RefreshGrid(null);
                    //NOTES: GRID_RIGHT_DISABLED
                    await _detailRightGridRef.R_RefreshGrid(null);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.ThrowExceptionIfErrors();
            }
        }
        private async Task DetailLeft_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetUnitInquiryDetailLeftListAsync();
                //if (loViewModel.loUnitInquiryDetailLeftList.Count() == 0)
                //{
                //    await R_MessageBox.Show("", "No data found!", R_eMessageBoxButtonType.OK);
                //}
                eventArgs.ListEntityResult = loViewModel.loUnitInquiryDetailLeftList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task DetailRight_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();

            try
            {
                await loViewModel.GetUnitInquiryDetailRightListAsync();
                eventArgs.ListEntityResult = loViewModel.loUnitInquiryDetailRightList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
