using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50621;
using PMT50600MODEL.ViewModel;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using System.Diagnostics.Tracing;
using Lookup_PMModel.ViewModel.LML00200;
using PMT50600FrontResources;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace PMT50600FRONT
{
    public partial class PMT50621
    {
        [Inject] private R_ILocalizer<PMT50600FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private R_TextBox _productDeptRef;

        private PMT50621ViewModel loViewModel = new PMT50621ViewModel();

        private R_Conductor _conductorRef;

        private bool IsAllocationEnable = false;

        private string lcExpProdLabel = "Product/Expenditure";

        private LinkedList<PurchaseUnitDTO> loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>();

        private List<PurchaseUnitDTO> loTempPurchaseUnitList = new List<PurchaseUnitDTO>();

        private List<CustomerOptionRadioButton> loOptionDiscountRadioButtonList = new List<CustomerOptionRadioButton>()
        {
            new CustomerOptionRadioButton() { CTENANT_OPTION_CODE = "P", CTENANT_OPTION_NAME = "%" },
            new CustomerOptionRadioButton() { CTENANT_OPTION_CODE = "V", CTENANT_OPTION_NAME = "Amount"}
        };

        private bool IsPurchaseUnitOther = false;

        private bool IsDiscountPercentEnable = false;

        private bool IsDiscountAmountEnable = false;

        private bool IsTransStatus00 = false;

        [Inject] IClientHelper _clientHelper { get; set; }

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMT50621DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT50600",
                        Table_Name = "PMT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT50600",
                        Table_Name = "PMT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loViewModel.loTabParam = (TabItemEntryParameterDTO)poParameter;
                if (loViewModel.loTabParam != null)
                {
                    if (loViewModel.loTabParam.Data != null)
                    {
                        loViewModel.loHeader = loViewModel.loTabParam.Data;
                        TransStatusChanged();
                    }
                    await loViewModel.GetProductTypeListStreamAsync();
                    if (loViewModel.loTabParam.LIS_NEW)
                    {
                        await _conductorRef.Add();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(loViewModel.loTabParam.CREC_ID))
                        {
                            loViewModel.loCreditNoteItem.CREC_ID = loViewModel.loTabParam.CREC_ID;
                            await _conductorRef.R_GetEntity(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task CreditNoteItem_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await _productDeptRef.FocusAsync();
                if (loViewModel.Data.CDISC_TYPE == "P")
                {
                    IsDiscountPercentEnable = true;
                }
                else if (loViewModel.Data.CDISC_TYPE == "V")
                {
                    IsDiscountAmountEnable = true;
                }
                if (loViewModel.Data.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = "Product";
                    IsAllocationEnable = true;
                }
                else if (loViewModel.Data.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = "Expenditure";
                    IsAllocationEnable = false;
                }
            }
        }

        private async Task CreditNoteItem_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            List<PurchaseUnitDTO> loPurchaseUnitList = new List<PurchaseUnitDTO>();

            try
            {
                await loViewModel.GetInfoAsync();

                TransStatusChanged();
                if (loViewModel.loCreditNoteItem.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = "Product";
                }
                else if (loViewModel.loCreditNoteItem.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = "Expenditure";
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loCreditNoteItem.CUNIT1))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 1,
                        CDESCRIPTION = loViewModel.loCreditNoteItem.CUNIT1
                    });
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loCreditNoteItem.CUNIT2))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 2,
                        CDESCRIPTION = loViewModel.loCreditNoteItem.CUNIT2
                    });
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loCreditNoteItem.CUNIT3))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 3,
                        CDESCRIPTION = loViewModel.loCreditNoteItem.CUNIT3
                    });
                }

                loPurchaseUnitList.Add(new PurchaseUnitDTO()
                {
                    ICODE = 4,
                    CDESCRIPTION = "Other"
                });

                loTempPurchaseUnitList = new List<PurchaseUnitDTO>(loPurchaseUnitList);
                loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>(loPurchaseUnitList);

                loViewModel.loCreditNoteItem.NTOTAL_AMOUNT = loViewModel.loCreditNoteItem.NTAXABLE_AMOUNT + loViewModel.loCreditNoteItem.NTAX_AMOUNT + loViewModel.loCreditNoteItem.NOTHER_TAX_AMOUNT;
                eventArgs.Result = loViewModel.loCreditNoteItem;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task CreditNoteItem_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.DeleteInvoiceItemAsync((PMT50621DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void CreditNoteItem_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50621DTO loData = (PMT50621DTO)eventArgs.Data;
                loViewModel.InvoiceItemValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task CreditNoteItem_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await _productDeptRef.FocusAsync();
            IsTransStatus00 = false;
            PMT50621DTO loData = (PMT50621DTO)eventArgs.Data;
            loData.CTAX_ID = loViewModel.loHeader.CTAX_ID;
            loData.CTAX_NAME = loViewModel.loHeader.CTAX_NAME;
            loData.NTAX_PCT = loViewModel.loHeader.NTAX_PCT;
            loData.CDISC_TYPE = loOptionDiscountRadioButtonList.FirstOrDefault().CTENANT_OPTION_CODE;
            IsDiscountPercentEnable = true;
            loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>();
        }

        private void TransStatusChanged()
        {
            if (loViewModel.loHeader.CTRANS_STATUS == "00")
            {
                IsTransStatus00 = true;
            }
            else
            {
                IsTransStatus00 = false;
            }
        }

        private void CreditNoteItem_SetEdit(R_SetEventArgs eventArgs)
        {
            if (eventArgs.Enable)
            {
                IsTransStatus00 = false;
            }
        }

        //private void CreditNoteItem_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        //{
        //    IsTransStatus00 = false;
        //}

        private async Task CreditNoteItem_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M007"], R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.No)
            {
                eventArgs.Cancel = true;
                return;
            }
        }

        private async Task CreditNoteItem_AfterDelete()
        {
            await R_MessageBox.Show("", _localizer["M008"], R_eMessageBoxButtonType.OK);
        }

        private async Task CreditNoteItem_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M006"], R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.No)
            {
                eventArgs.Cancel = true;
                return;
            }

            TransStatusChanged();
            IsAllocationEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
            if (loTempPurchaseUnitList.Count > 0 && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
            {
                loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>(loTempPurchaseUnitList);
            }
        }

        private void CreditNoteItem_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            TransStatusChanged();
            IsAllocationEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private async Task CreditNoteItem_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveInvoiceItemAsync((PMT50621DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                loViewModel.loCreditNoteItem.NTOTAL_AMOUNT = loViewModel.loCreditNoteItem.NTAXABLE_AMOUNT + loViewModel.loCreditNoteItem.NTAX_AMOUNT + loViewModel.loCreditNoteItem.NOTHER_TAX_AMOUNT;
                eventArgs.Result = loViewModel.loCreditNoteItem;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region OnLostFocus

        private async Task OnLostFocusServiceDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50621DTO loGetData = (PMT50621DTO)loViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CPROD_DEPT_CODE))
                {
                    loGetData.CPROD_DEPT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                    CSEARCH_TEXT = loViewModel.Data.CPROD_DEPT_CODE
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CPROD_DEPT_CODE = "";
                    loGetData.CPROD_DEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CPROD_DEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CPROD_DEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnLostFocusService()
        {
            R_Exception loEx = new R_Exception();
            string lcTaxableFlag = "";
            string lcMode = "";
            List<PurchaseUnitDTO> loPurchaseUnitList = new List<PurchaseUnitDTO>();

            try
            {
                //    PMT50621DTO loGetData = (PMT50621DTO)loViewModel.Data;

                //    if (string.IsNullOrWhiteSpace(loGetData.CPRODUCT_ID))
                //    {
                //        loGetData.CPRODUCT_NAME = "";
                //        loGetData.COTHER_TAX_ID = "";
                //        loGetData.COTHER_TAX_NAME = "";
                //        loGetData.NOTHER_TAX_PCT = 0;
                //        loGetData.CUNIT1 = "";
                //        loGetData.CUNIT2 = "";
                //        loGetData.CUNIT3 = "";
                //        return;
                //    }

                //    if (loViewModel.loTabParam.Data.LTAXABLE)
                //    {
                //        lcTaxableFlag = "1";
                //    }
                //    else
                //    {
                //        lcTaxableFlag = "2";
                //    }
                //    if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                //    {
                //        lcMode = "1";
                //    }
                //    else
                //    {
                //        lcMode = "0";
                //    }
                //    if (loViewModel.Data.CPROD_TYPE == "P")
                //    {
                //        //LookupAPL00300ViewModel loLookupViewModel1 = new LookupAPL00300ViewModel();

                //        //APL00300ParameterDTO loParam1 = new APL00300ParameterDTO()
                //        //{
                //        //    CCOMPANY_ID = "",
                //        //    CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                //        //    CTAXABLE_TYPE = lcTaxableFlag,
                //        //    CACTIVE_TYPE = lcMode,
                //        //    CLANGUAGE_ID = "",
                //        //    CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE,
                //        //    CBUYSELL = "S",
                //        //    CSEARCH_CODE = loViewModel.Data.CPRODUCT_ID
                //        //};

                //        //APL00300DTO loResult1 = await loLookupViewModel1.GetProductLookup(loParam1);

                //        if (loResult1 == null)
                //        {
                //            loEx.Add(R_FrontUtility.R_GetError(
                //                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                //                    "_ErrLookup01"));
                //            loGetData.CPRODUCT_ID = "";
                //            loGetData.CPRODUCT_NAME = "";
                //            loGetData.COTHER_TAX_ID = "";
                //            loGetData.COTHER_TAX_NAME = "";
                //            loGetData.NOTHER_TAX_PCT = 0;
                //            loGetData.CUNIT1 = "";
                //            loGetData.CUNIT2 = "";
                //            loGetData.CUNIT3 = "";
                //            //await GLAccount_TextBox.FocusAsync();
                //        }
                //        else
                //        {
                //            loGetData.CPRODUCT_ID = loResult1.CPRODUCT_ID;
                //            loGetData.CPRODUCT_NAME = loResult1.CPRODUCT_NAME;
                //            loGetData.COTHER_TAX_ID = loResult1.COTHER_TAX_ID;
                //            loGetData.COTHER_TAX_NAME = loResult1.COTHER_TAX_NAME;
                //            loGetData.NOTHER_TAX_PCT = loResult1.NOTHER_TAX_PCT;
                //            loGetData.CUNIT1 = loResult1.CUNIT1;
                //            loGetData.CUNIT2 = loResult1.CUNIT2;
                //            loGetData.CUNIT3 = loResult1.CUNIT3;
                //        }
                //    }
                //    else if (loViewModel.Data.CPROD_TYPE == "S")
                //    {
                //        LookupLML00200ViewModel loLookupViewModel2 = new LookupLML00200ViewModel();

                //        LML00200ParameterDTO loParam2 = new LML00200ParameterDTO()
                //        {
                //            CCOMPANY_ID = "",
                //            CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                //            CCHARGE_TYPE_ID = "01",
                //            CUSER_ID = "",
                //            CTAXABLE_TYPE = lcTaxableFlag,
                //            CACTIVE_TYPE = "1",
                //            CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE,
                //            CSEARCH_TEXT = loViewModel.Data.CPRODUCT_ID
                //        };

                //        LML00200DTO loResult2 = await loLookupViewModel2.GetUnitCharges(loParam2);

                //        if (loResult2 == null)
                //        {
                //            loEx.Add(R_FrontUtility.R_GetError(
                //                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                //                    "_ErrLookup01"));
                //            loGetData.CPRODUCT_ID = "";
                //            loGetData.CPRODUCT_NAME = "";
                //            loGetData.COTHER_TAX_ID = "";
                //            loGetData.COTHER_TAX_NAME = "";
                //            loGetData.NOTHER_TAX_PCT = 0;
                //            loGetData.CUNIT1 = "";
                //            loGetData.CUNIT2 = "";
                //            loGetData.CUNIT3 = "";
                //            //await GLAccount_TextBox.FocusAsync();
                //        }
                //        else
                //        {
                //            loGetData.CPRODUCT_ID = loResult2.CCHARGES_ID;
                //            loGetData.CPRODUCT_NAME = loResult2.CCHARGES_NAME;
                //            loGetData.COTHER_TAX_ID = loResult2.COTHER_TAX_ID;
                //            loGetData.COTHER_TAX_NAME = loResult2.COTHER_TAX_NAME;
                //            loGetData.NOTHER_TAX_PCT = loResult2.NOTHER_TAX_PCT;
                //            loGetData.CUNIT1 = loResult2.CUOM;
                //            loGetData.CUNIT2 = "";
                //            loGetData.CUNIT3 = "";
                //        }
                //    }

                //    if (!string.IsNullOrWhiteSpace(loGetData.CUNIT1))
                //    {
                //        loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //        {
                //            ICODE = 1,
                //            CDESCRIPTION = loGetData.CUNIT1
                //        });
                //    }

                //    if (!string.IsNullOrWhiteSpace(loGetData.CUNIT2))
                //    {
                //        loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //        {
                //            ICODE = 2,
                //            CDESCRIPTION = loGetData.CUNIT2
                //        });
                //    }

                //    if (!string.IsNullOrWhiteSpace(loGetData.CUNIT3))
                //    {
                //        loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //        {
                //            ICODE = 3,
                //            CDESCRIPTION = loGetData.CUNIT3
                //        });
                //    }

                //    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //    {
                //        ICODE = 4,
                //        CDESCRIPTION = "Other"
                //    });

                //    loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>(loPurchaseUnitList);
                //    loViewModel.Data.IBILL_UNIT = 1;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion


        #region Lookup
        private void R_Before_Open_LookupServiceDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.loTabParam.Data.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                CUSER_LOGIN_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void R_After_Open_LookupServiceDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CPROD_DEPT_CODE = loTempResult.CDEPT_CODE;
            loViewModel.Data.CPROD_DEPT_NAME = loTempResult.CDEPT_NAME;
        }

        //private void R_Before_Open_LookupAllocation(R_BeforeOpenLookupEventArgs eventArgs)
        //{
        //    R_Exception loEx = new R_Exception();
        //    string lcMode = "";

        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(loViewModel.loTabParam.Data.CPROPERTY_ID))
        //        {
        //            return;
        //        }
        //        if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
        //        {
        //            lcMode = "1";
        //        }
        //        else
        //        {
        //            lcMode = "0";
        //        }
        //        APL00400ParameterDTO loParam = new APL00400ParameterDTO()
        //        {
        //            CCOMPANY_ID = "",
        //            CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
        //            CACTIVE_TYPE = lcMode,
        //            CLANGUAGE_ID = ""
        //        };
        //        eventArgs.Parameter = loParam;
        //        eventArgs.TargetPageType = typeof(APL00400);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        //private void R_After_Open_LookupAllocation(R_AfterOpenLookupEventArgs eventArgs)
        //{
        //    APL00400DTO loTempResult = (APL00400DTO)eventArgs.Result;
        //    if (loTempResult == null)
        //    {
        //        return;
        //    }
        //    loViewModel.Data.CALLOC_ID = loTempResult.CALLOC_ID;
        //    loViewModel.Data.CALLOC_NAME = loTempResult.CALLOC_NAME;
        //}

        private void R_Before_Open_LookupService(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            string lcTaxableFlag = "";
            string lcMode = "";
            try
            {
                //    if (loViewModel.loTabParam.Data.LTAXABLE)
                //    {
                //        lcTaxableFlag = "1";
                //    }
                //    else
                //    {
                //        lcTaxableFlag = "2";
                //    }
                //    if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                //    {
                //        lcMode = "1";
                //    }
                //    else
                //    {
                //        lcMode = "0";
                //    }
                //    if (loViewModel.Data.CPROD_TYPE == "P")
                //    {
                //        APL00300ParameterDTO loParam1 = new APL00300ParameterDTO()
                //        {
                //            CCOMPANY_ID = "",
                //            CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                //            CTAXABLE_TYPE = lcTaxableFlag,
                //            CACTIVE_TYPE = lcMode,
                //            CLANGUAGE_ID = "",
                //            CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE,
                //            CBUYSELL = "S"
                //        };
                //        eventArgs.Parameter = loParam1;
                //        eventArgs.TargetPageType = typeof(APL00300);
                //    }
                //    else if (loViewModel.Data.CPROD_TYPE == "S")
                //    {
                //        LML00200ParameterDTO loParam2 = new LML00200ParameterDTO()
                //        {
                //            CCOMPANY_ID = "",
                //            CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                //            CCHARGE_TYPE_ID = "01",
                //            CUSER_ID = "",
                //            CTAXABLE_TYPE = lcTaxableFlag,
                //            CACTIVE_TYPE = "1",
                //            CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE
                //        };
                //        eventArgs.Parameter = loParam2;
                //        eventArgs.TargetPageType = typeof(LML00200);
                //    }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_After_Open_LookupService(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            List<PurchaseUnitDTO> loPurchaseUnitList = new List<PurchaseUnitDTO>();

            try
            {
                //if (loViewModel.Data.CPROD_TYPE == "P")
                //{
                //    APL00300DTO loTempResult1 = (APL00300DTO)eventArgs.Result;
                //    if (loTempResult1 == null)
                //    {
                //        return;
                //    }
                //    loViewModel.Data.CPRODUCT_ID = loTempResult1.CPRODUCT_ID;
                //    loViewModel.Data.CPRODUCT_NAME = loTempResult1.CPRODUCT_NAME;
                //    loViewModel.Data.COTHER_TAX_ID = loTempResult1.COTHER_TAX_ID;
                //    loViewModel.Data.COTHER_TAX_NAME = loTempResult1.COTHER_TAX_NAME;
                //    loViewModel.Data.NOTHER_TAX_PCT = loTempResult1.NOTHER_TAX_PCT;
                //    loViewModel.Data.CUNIT1 = loTempResult1.CUNIT1;
                //    loViewModel.Data.CUNIT2 = loTempResult1.CUNIT2;
                //    loViewModel.Data.CUNIT3 = loTempResult1.CUNIT3;
                //}
                //else if (loViewModel.Data.CPROD_TYPE == "S")
                //{
                //    LML00200DTO loTempResult2 = (LML00200DTO)eventArgs.Result;
                //    if (loTempResult2 == null)
                //    {
                //        return;
                //    }
                //    loViewModel.Data.CPRODUCT_ID = loTempResult2.CCHARGES_ID;
                //    loViewModel.Data.CPRODUCT_NAME = loTempResult2.CCHARGES_NAME;
                //    loViewModel.Data.COTHER_TAX_ID = loTempResult2.COTHER_TAX_ID;
                //    loViewModel.Data.COTHER_TAX_NAME = loTempResult2.COTHER_TAX_NAME;
                //    loViewModel.Data.NOTHER_TAX_PCT = loTempResult2.NOTHER_TAX_PCT;
                //    loViewModel.Data.CUNIT1 = loTempResult2.CUOM;
                //    loViewModel.Data.CUNIT2 = "";
                //    loViewModel.Data.CUNIT3 = "";
                //}

                //if (!string.IsNullOrWhiteSpace(loViewModel.Data.CUNIT1))
                //{
                //    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //    {
                //        ICODE = 1,
                //        CDESCRIPTION = loViewModel.Data.CUNIT1
                //    });
                //}

                //if (!string.IsNullOrWhiteSpace(loViewModel.Data.CUNIT2))
                //{
                //    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //    {
                //        ICODE = 2,
                //        CDESCRIPTION = loViewModel.Data.CUNIT2
                //    });
                //}

                //if (!string.IsNullOrWhiteSpace(loViewModel.Data.CUNIT3))
                //{
                //    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //    {
                //        ICODE = 3,
                //        CDESCRIPTION = loViewModel.Data.CUNIT3
                //    });
                //}

                //loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //{
                //    ICODE = 4,
                //    CDESCRIPTION = "Other"
                //});

                //loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>(loPurchaseUnitList);
                //loViewModel.Data.IBILL_UNIT = 1;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region OnChange

        private async Task ServiceTypeComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CPROD_TYPE = poParam;
                loViewModel.Data.CPRODUCT_ID = "";
                loViewModel.Data.CPRODUCT_NAME = "";
                loViewModel.Data.COTHER_TAX_ID = "";
                loViewModel.Data.COTHER_TAX_NAME = "";
                loViewModel.Data.NOTHER_TAX_PCT = 0;
                loViewModel.Data.CUNIT1 = "";
                loViewModel.Data.CUNIT2 = "";
                loViewModel.Data.CUNIT3 = "";
                //if (loViewModel.Data.CPROD_TYPE == "P")
                //{
                //    lcExpProdLabel = "Product";
                //    IsAllocationEnable = true;
                //}
                //else if (loViewModel.Data.CPROD_TYPE == "E")
                //{
                //    lcExpProdLabel = "Expenditure";
                //    loViewModel.Data.CALLOC_ID = "";
                //    loViewModel.Data.CALLOC_NAME = "";
                //    IsAllocationEnable = false;
                //}
                loViewModel.Data.CBILL_UNIT = "";
                loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DiscountOptionRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CDISC_TYPE = poParam;
                if (loViewModel.Data.CDISC_TYPE == "P")
                {
                    loViewModel.Data.NDISC_AMOUNT = 0;
                    IsDiscountPercentEnable = true;
                    IsDiscountAmountEnable = false;
                }
                else if (loViewModel.Data.CDISC_TYPE == "V")
                {
                    loViewModel.Data.NDISC_PCT = 0;
                    IsDiscountPercentEnable = false;
                    IsDiscountAmountEnable = true;
                }
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void RefreshItemValue()
        {
            loViewModel.Data.NAMOUNT = loViewModel.Data.NBILL_UNIT_QTY * loViewModel.Data.NUNIT_PRICE;
            if (loViewModel.Data.CDISC_TYPE == "P")
            {
                loViewModel.Data.NDISC_AMOUNT = loViewModel.Data.NDISC_PCT * loViewModel.Data.NAMOUNT * 0.01m;
            }
            loViewModel.Data.NTAXABLE_AMOUNT = loViewModel.Data.NAMOUNT - loViewModel.Data.NDISC_AMOUNT - loViewModel.Data.NDIST_DISCOUNT;
            loViewModel.Data.NTAX_AMOUNT = loViewModel.Data.NTAXABLE_AMOUNT * loViewModel.Data.NTAX_PCT * 0.01m;
            loViewModel.Data.NOTHER_TAX_AMOUNT = loViewModel.Data.NTAXABLE_AMOUNT * loViewModel.Data.NOTHER_TAX_PCT * 0.01m;
            loViewModel.Data.NTOTAL_AMOUNT = loViewModel.Data.NTAXABLE_AMOUNT + loViewModel.Data.NTAX_AMOUNT + loViewModel.Data.NOTHER_TAX_AMOUNT;
        }

        private async Task PurchaseQtyTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NBILL_UNIT_QTY = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task UnitPriceTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NUNIT_PRICE = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        private async Task DiscountPercentageTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NDISC_PCT = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        private async Task DiscountAmountTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NDISC_AMOUNT = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PurchaseUnitComboBox_ValueChanged(int poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.IBILL_UNIT = poParam;
                if (loViewModel.Data.IBILL_UNIT == 4)
                {
                    IsPurchaseUnitOther = true;
                }
                else
                {
                    loViewModel.Data.CBILL_UNIT = loPurchaseUnitComboboxLinkedList.Where(x => x.ICODE == loViewModel.Data.IBILL_UNIT).FirstOrDefault().CDESCRIPTION;
                    loViewModel.Data.NSUPP_CONV_FACTOR = 0;
                    IsPurchaseUnitOther = false;
                }
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
