using GSM00300COMMON;
using GSM00300COMMON.DTO_s;
using GSM00300COMMON.DTO_s.Helper;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;
using GSM00300FrontResources;
using R_BlazorFrontEnd.Helpers;

namespace GSM00300MODEL.View_Model
{
    public class GSM00300ViewModel : R_ViewModel<CompanyParamRecordDTO>
    {
        private GSM00300Model _model = new GSM00300Model();

        public CompanyParamRecordDTO CompanyParamRecord { get; set; } = new CompanyParamRecordDTO();

        public CheckPrimaryAccountDTO CheckPrimaryAccountRecord { get; set; } = new CheckPrimaryAccountDTO();
        public ValidateCompanyDTO CheckCompanyParamEditableRecord { get; set; } = new ValidateCompanyDTO();

        public List<GeneralTypeDTO> CenterByList { get; set; } = new List<GeneralTypeDTO>();

        public async Task InitProcess(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //generate report type
                CenterByList = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CCODE = "IS", CDESCRIPTION = poParamLocalizer["_radioitem_incstatementbycenter"] },
                    new GeneralTypeDTO { CCODE = "BS", CDESCRIPTION = poParamLocalizer ["_radioitem_balancesheetbycenter"] },
                };
                await CheckPrimaryAccountAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        public async Task CheckPrimaryAccountAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.CheckPrimaryAccountAsync();
                CheckPrimaryAccountRecord = new CheckPrimaryAccountDTO() { LIS_PRIMARY = loResult.LIS_PRIMARY };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task CheckIsCompanyParamEditableAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.CheckIsCompanyParamEditableAsync();
                CheckCompanyParamEditableRecord = new ValidateCompanyDTO() { LALLOW_EDIT= loResult.LALLOW_EDIT };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task CompParamGetRecord()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(new CompanyParamRecordDTO());
                CompanyParamRecord = R_FrontUtility.ConvertObjectToObject<CompanyParamRecordDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task CompParamSaveAsync(CompanyParamRecordDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, poCRUDMode);
                CompanyParamRecord = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


    }
}
