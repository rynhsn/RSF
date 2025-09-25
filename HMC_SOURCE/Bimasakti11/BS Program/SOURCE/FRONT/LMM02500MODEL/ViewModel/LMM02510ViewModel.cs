using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM02500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMM02500Model.ViewModel
{
    public class LMM02510ViewModel : R_ViewModel<LMM02500ProfileAndTaxInfoDTO>

    {
        private readonly LMM02510Model _modelLMM02510 = new LMM02510Model();
        private readonly LMM02500Model _modelLMM02500 = new LMM02500Model();
        public LMM02500ProfileAndTaxInfoDTO? loEntityLMM02510 = new LMM02500ProfileAndTaxInfoDTO();
        public DateTime ltCID_EXPIRED_DATE;
        public LMM02500TabParameterDTO loTabParameter = new LMM02500TabParameterDTO();

        
        public List<LMM02500ComboBoxType> loTaxTypeList { get; set; } = new List<LMM02500ComboBoxType>();
        public List<LMM02500ComboBoxType> loIdTypeList { get; set; } = new List<LMM02500ComboBoxType>();
        public List<LMM02500ComboBoxType> loTaxCodeTypeList { get; set; } = new List<LMM02500ComboBoxType>();
        
        public async Task GetEntity()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM02500ProfileAndTaxInfoDTO
                {
                    Profile =
                    {
                        CCOMPANY_ID = loTabParameter.CCOMPANY_ID,
                        CUSER_LOGIN_ID = loTabParameter.CUSER_LOGIN_ID,
                        CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                        CTENANT_GROUP_ID = loTabParameter.CTENANT_GROUP_ID,
                        CTENANT_GROUP_NAME = loTabParameter.CTENANT_GROUP_NAME
                    }
                };

                var loResult = await _modelLMM02510.R_ServiceGetRecordAsync(loParam);
                if (loResult.Profile.LUSE_GROUP_TAX == false)
                    ltCID_EXPIRED_DATE = new DateTime(1, 1, 1);
                else
                    ltCID_EXPIRED_DATE = await ChangeFormatStringToDateTime(loResult.TaxInfo.CID_EXPIRED_DATE);

                loEntityLMM02510 = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<DateTime> ChangeFormatStringToDateTime(string pcDateTime)
        {
            var ltReturn = new DateTime();
            var loEx = new R_Exception();

            try
            {
                //YYYYMMDD
                ltReturn = DateTime.ParseExact(pcDateTime, "yyyyMMdd", null);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return ltReturn;
        }


        public async Task<string> ChangeFormatDateTimeToString(DateTime pcDateTime)
        {
            var lcReturn = "";
            var loEx = new R_Exception();

            try
            {
                //YYYYMMDD
                lcReturn = pcDateTime.ToString("yyyyMMdd");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return lcReturn;
        }


        public async Task SaveCashTenantGroup(LMM02500ProfileAndTaxInfoDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    //yang dikirim via context
                }

                poNewEntity.TaxInfo.CID_EXPIRED_DATE = await ChangeFormatDateTimeToString(ltCID_EXPIRED_DATE);

                var loResult = await _modelLMM02510.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityLMM02510 = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ProfileValidation(LMM02500ProfileAndTaxInfoDTO poParam)
        {
            bool llCancel = false;
            R_Exception loException = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CTENANT_GROUP_ID);
                if (llCancel)
                {
                    loException.Add("", "Tab Profile's Group Id is required");
                }
                
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CTENANT_GROUP_NAME);
                if (llCancel)
                {
                    loException.Add("", "Tab Profile's Group Name is required");
                }
                
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CADDRESS);
                if (llCancel)
                {
                    loException.Add("", "Tab Profile's Address is required");
                }
                
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CPIC_NAME);
                if (llCancel)
                {
                    loException.Add("", "Tab Profile's PIC Name is required");
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            
            loException.ThrowExceptionIfErrors();

        }
        

        public async Task GetTaxTypeComboBox()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelLMM02500.GetTaxTypeComboBoxAsync();
                loTaxTypeList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetIdTypeComboBox()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelLMM02500.GetIdTypeComboBoxAsync();
                loIdTypeList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxCodeComboBox()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelLMM02500.GetTaxCodeComboBoxAsync();
                loTaxCodeTypeList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        
        public async Task DeleteTenantGroup(LMM02500ProfileAndTaxInfoDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMM02510.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public class DataTypeOnChangeUseTaxInfo
        {
            public bool Cancel { get; set; }
            public bool Value { get; set; }
        }
    }
}