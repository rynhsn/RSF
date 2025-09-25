using APT00100COMMON.DTOs.APT00131;
using APT00100MODEL.ViewModel;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Popup;

namespace APT00100FRONT
{
    public partial class APT00131 : R_Page
    {
        [Inject] private R_ILocalizer<APT00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        [Inject] private R_PopupService PopupService { get; set; }

        private APT00131ViewModel loViewModel = new APT00131ViewModel();

        private R_ConductorGrid _conductorRef;

        private R_Grid<APT00131DTO> _gridAdditionalRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private bool IsDiscountTypeEnable = false;

        private bool IsDiscountPercentEnable = false;

        private bool IsDiscountAmountEnable = false;

        private string lcLabel = "";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loViewModel.loTabParam = (AdditionalParameterDTO)poParameter;
                if (loViewModel.loTabParam.CADDITIONAL_TYPE == "A")
                {
                    lcLabel = _localizer["_Addition"];
                }
                else if (loViewModel.loTabParam.CADDITIONAL_TYPE == "D")
                {
                    lcLabel = _localizer["_Deduction"];
                }
                else
                {
                    lcLabel = _localizer["_Addition"] + " / " + _localizer["_Deduction"];
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loTabParam.CREC_ID))
                {
                    await loViewModel.GetCompanyInfoAsync();
                    await loViewModel.GetHeaderInfoAsync();
                    await _gridAdditionalRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        protected override async Task R_PageClosing(R_PageClosingEventArgs eventArgs)
        {
            //await loViewModel.GetHeaderInfoAsync();

            //if (loViewModel.loTabParam.CADDITIONAL_TYPE == "A")
            //{
            //    await this.Close(true, loViewModel.loHeader.NADDITION);
            //}
            //else  
            //{
            //    await this.Close(true, loViewModel.loHeader.NDEDUCTION);
            //}
        }

        private void Additional_Display(R_DisplayEventArgs eventArgs)
        {
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetAdditionalListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loAdditionalList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Additional_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetAdditionalAsync((APT00131DTO)eventArgs.Data);
                eventArgs.Result = loViewModel.loAdditional;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Additional_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
        }

        private async Task Additional_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", string.Format(_localizer["M009"], lcLabel), R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.No)
            {
                eventArgs.Cancel = true;
                return;
            }
        }

        private async Task Additional_AfterDelete()
        {
            await R_MessageBox.Show("", string.Format(_localizer["M010"], lcLabel), R_eMessageBoxButtonType.OK);
        }


        private void Additional_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsDiscountTypeEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private void Additional_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            IsDiscountTypeEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private async Task Additional_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveAdditionalAsync((APT00131DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loAdditional;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Additional_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.DeleteAdditionalAsync((APT00131DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        #region Lookup

        private void Grid_BeforelookUp(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(APT00131DTO.CADD_DEPT_CODE))
            {
                eventArgs.Parameter = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loHeader.CPROPERTY_ID
                };
                eventArgs.TargetPageType = typeof(GSL00710);
            }
            else if (eventArgs.ColumnName == nameof(APT00131DTO.CCHARGES_ID))
            {
                eventArgs.Parameter = new GSL01400ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loHeader.CPROPERTY_ID,
                    CCHARGES_TYPE_ID = loViewModel.loTabParam.CADDITIONAL_TYPE
                };
                eventArgs.TargetPageType = typeof(GSL01400);
            }
        }

        private void Grid_AfterlookUp(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(APT00131DTO.CADD_DEPT_CODE))
            {
                GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                APT00131DTO loGetData = (APT00131DTO)eventArgs.ColumnData;
                loGetData.CADD_DEPT_CODE = loTempResult.CDEPT_CODE;
                loGetData.CADD_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
            else if (eventArgs.ColumnName == nameof(APT00131DTO.CCHARGES_ID))
            {
                GSL01400DTO loTempResult = (GSL01400DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                APT00131DTO loGetData = (APT00131DTO)eventArgs.ColumnData;
                loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
                loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
                //loGetData.CCHARGES_DESC = loTempResult.;
            }
        }
        #endregion
    }
}
