using GSM04500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Enums;
using R_CommonFrontBackAPI;
using GSM04500FrontResources;
using R_BlazorFrontEnd.Helpers;

namespace GSM04500Model
{
    public class GSM04502ViewModel : R_ViewModel<GSM04510GOADeptDTO>
    {
        private GSM04502Model _modelGOADept = new GSM04502Model();
        public ObservableCollection<GSM04510GOADeptDTO> GOADeptList = new ObservableCollection<GSM04510GOADeptDTO>();
        public GSM04510GOADeptDTO GOADept { get; set; } = new GSM04510GOADeptDTO();
        public string? GroupOfAccount { get; set; }
        public bool _lGOADeptAllowAdd;

        public async Task GetGOAAllByDept(GSM04510GOADTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                GSM04510GOADeptListDTO loResult = new GSM04510GOADeptListDTO();
                if (poEntity.CGOA_CODE != null)
                {

                    R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_TYPE, poEntity.CJRNGRP_TYPE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, "");
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CJOURNAL_GRP_CODE, poEntity.CJRNGRP_CODE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CGOA_CODE, poEntity.CGOA_CODE);

                    loResult = await _modelGOADept.GetAllGOADeptAsync();
                    GroupOfAccount = poEntity.GROUPOFACCOUNT;
                    GOADeptList = new ObservableCollection<GSM04510GOADeptDTO>(loResult.ListData);
                }
                else
                {
                    GOADeptList = new ObservableCollection<GSM04510GOADeptDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSM04510GOADeptDTO> GetGOADeptOneRecord(GSM04510GOADeptDTO poEntity)
        {
            var loEx = new R_Exception();
            GSM04510GOADeptDTO loResult = null;
            try
            {
                var loParam = new GSM04510GOADeptDTO
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CUSER_ID = poEntity.CUSER_ID,
                    CPROPERTY_ID = "",
                    CJRNGRP_TYPE = poEntity.CJRNGRP_TYPE,
                    CJRNGRP_CODE = poEntity.CJRNGRP_CODE,
                    CGOA_CODE = poEntity.CGOA_CODE,
                    CDEPT_CODE = poEntity.CDEPT_CODE
                };
                loResult = await _modelGOADept.R_ServiceGetRecordAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<GSM04510GOADeptDTO> SaveGOADept(GSM04510GOADeptDTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM04510GOADeptDTO loResult = null;

            try
            {
                loResult = await _modelGOADept.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                GOADept = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task DeleteOneRecordGOADept(GSM04510GOADeptDTO poProperty)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM04510GOADeptDTO()
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CPROPERTY_ID = "",
                    CJRNGRP_TYPE = poProperty.CJRNGRP_TYPE,
                    CJRNGRP_CODE = poProperty.CJRNGRP_CODE,
                    CGOA_CODE = poProperty.CGOA_CODE,
                    CDEPT_CODE = poProperty.CDEPT_CODE,
                    CGLACCOUNT_NO = poProperty.CGLACCOUNT_NO,
                    CUSER_ID = poProperty.CUSER_ID
                };
                await _modelGOADept.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationGOADept(GSM04510GOADeptDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (!_lGOADeptAllowAdd)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM04500_Class), "Validation");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
