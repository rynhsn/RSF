using LMM06500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace LMM06500MODEL
{
    public class LMM06500ViewModel : R_ViewModel<LMM06500DTO>
    {
        private LMM06500Model _LMM06500Model = new LMM06500Model();
        private LMM06501Model _LMM06501Model = new LMM06501Model();

        public ObservableCollection<LMM06500DTO> StaffGrid { get; set; } = new ObservableCollection<LMM06500DTO>();
        public List<LMM06500DTOInitial> PropertyList { get; set; } = new List<LMM06500DTOInitial>();


        public LMM06500DTO Staff = new LMM06500DTO();

        public string PropertyValueContext = "";
        public bool StatusChange = false;
        public DateTime JoinDateTime = DateTime.Now;
        public string Position = "";

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM06500Model.GetPropertyAsync();
                PropertyList = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStaffList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM06500Model.GetStaffListAsync(PropertyValueContext);

                StaffGrid = new ObservableCollection<LMM06500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStaff(LMM06500DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);

                var loResult = await _LMM06500Model.R_ServiceGetRecordAsync(poParam);

                if (loResult.CJOIN_DATE != "0")
                {
                    JoinDateTime = DateTime.ParseExact(loResult.CJOIN_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                Position = loResult.CPOSITION;

                Staff = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void StaffValidation(LMM06500DTO poParam, eCRUDMode peCRUDMode)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CSTAFF_ID);
                if (llCancel)
                {
                    loEx.Add("", "Staff ID is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CSTAFF_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Staff Name is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CEMAIL);
                if (llCancel)
                {
                    loEx.Add("", "Staff Email is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CMOBILE_PHONE1);
                if (llCancel)
                {
                    loEx.Add("", "Staff Mobile Phone 1 is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CGENDER);
                if (llCancel)
                {
                    loEx.Add("", "Staff Gender is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveStaff(LMM06500DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM06500Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                Staff = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteStaff(LMM06500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM06500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ActiveInactiveProcessAsync(LMM06500DTO poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                poParameter.CPROPERTY_ID = PropertyValueContext;
                poParameter.LACTIVE = StatusChange;

                await _LMM06500Model.LMM06500ActiveInactiveAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task<LMM06500UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            LMM06500UploadFileDTO loResult = null;

            try
            {
                loResult = await _LMM06501Model.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }

    public class RadioButton
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class RadioButtonInt
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
