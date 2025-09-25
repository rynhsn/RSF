using GSM04500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM04500FrontResources;
using R_BlazorFrontEnd.Helpers;

namespace GSM04500Model
{
    public class GSM04500ViewModel : R_ViewModel<GSM04500DTO>
    {
        private GSM04500Model _model = new GSM04500Model();
        private GSM04500ModelTemplate _modelUploadTemplate = new GSM04500ModelTemplate();
        public List<GSM04500PropertyDTO> PropertyList { get; set; } = new List<GSM04500PropertyDTO>();
        public List<GSM04500JournalGroupTypeDTO> JournalGroupTypeList { get; set; } = new List<GSM04500JournalGroupTypeDTO>();

        public ObservableCollection<GSM04500DTO> JournalGroupList = new ObservableCollection<GSM04500DTO>();
        public GSM04500DTO JournalGroup { get; set; } = new GSM04500DTO();
        public GSM04500PropertyDTO CurrentProperty = new GSM04500PropertyDTO();
        public GSM04500DTO JournalGroupCurrent = new GSM04500DTO();

        public string JournalGroupTypeValue = "";
        public string JournalGroupCodeValue = "";
        public bool DropdownGroupType = true;
        public bool _lGroupTypeExist;

        public bool VisibleColumn_LACCRUAL = false;


        public async Task GetJournalGroupTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetJournalGroupTypeListAsyncModel();
                if (loResult != null)
                {
                    JournalGroupTypeValue = loResult.Data[0].CCODE;
                    _lGroupTypeExist = true;
                }
                else
                {
                    _lGroupTypeExist = false;
                }
                JournalGroupTypeList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAllJournalAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                switch (JournalGroupTypeValue)
                {
                    case "10":
                    case "11":
                    case "40":
                        VisibleColumn_LACCRUAL = true;
                        break;
                    default:
                        VisibleColumn_LACCRUAL = false;
                        break;
                }

                var x = VisibleColumn_LACCRUAL;
                var loResult = await _model.GetAllJournalGroupListAsync(JournalGroupTypeValue);
                JournalGroupList = new ObservableCollection<GSM04500DTO>(loResult.ListData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        public async Task<GSM04500DTO> GetGroupJournalOneRecord(GSM04500DTO poProperty)
        {
            var loEx = new R_Exception();
            GSM04500DTO loResult = null;

            try
            {
                var loParam = new GSM04500DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CUSER_ID = poProperty.CUSER_ID,
                    CPROPERTY_ID = "",
                    CJRNGRP_TYPE = poProperty.CJRNGRP_TYPE,
                    CJRNGRP_CODE = poProperty.CJRNGRP_CODE
                };
                loResult = await _model.R_ServiceGetRecordAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        public async Task DeleteOneRecordJournalGroup(GSM04500DTO poProperty)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM04500DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CJRNGRP_TYPE = poProperty.CJRNGRP_TYPE,
                    CJRNGRP_CODE = poProperty.CJRNGRP_CODE,
                    CJRNGRP_NAME = poProperty.CJRNGRP_NAME,
                    LACCRUAL = poProperty.LACCRUAL
                };
                await _model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationFieldEmpty(GSM04500DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(poEntity.CJRNGRP_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM04500_Class), "Error_01");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(poEntity.CJRNGRP_NAME))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM04500_Class), "Error_02");
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
        public async Task<GSM04500DTO> SaveJournalGroup(GSM04500DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM04500DTO loResult = null;

            try
            {
                poNewEntity.CJRNGRP_TYPE = JournalGroupTypeValue;
                loResult = await _model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                JournalGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        #region Template
        public async Task<GSM04500UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GSM04500UploadFileDTO loResult = null;

            try
            {
                loResult = await _modelUploadTemplate.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
