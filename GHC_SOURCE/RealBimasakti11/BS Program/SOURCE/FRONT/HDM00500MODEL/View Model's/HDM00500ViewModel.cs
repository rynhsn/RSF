using HDM00500COMMON;
using HDM00500COMMON.DTO_s;
using HDM00500COMMON.DTO_s.Helper;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace HDM00500MODEL.View_Model_s
{
    public class HDM00500ViewModel : R_ViewModel<TaskchecklistDTO>
    {
        //var
        private HDM00500Model _model = new HDM00500Model();
        public List<PropertyDTO> _propertyList { get; set; } = new List<PropertyDTO>();
        public ObservableCollection<TaskchecklistDTO> _taskchecklist_list { get; set; } = new ObservableCollection<TaskchecklistDTO>();
        public TaskchecklistDTO _taskchecklist_record { get; set; } = new TaskchecklistDTO();
        public string _propertyId { get; set; } = "";

        //method
        public async Task GetList_PropertyAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetList_PropertyAsync();
                if (loResult.Count > 0)
                {
                    _propertyList = new List<PropertyDTO>(loResult);
                    _propertyId = _propertyList[0].CPROPERTY_ID ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetList_TaskchecklistAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(Taskchecklist_ContextConstant.CPROPERTY_ID, _propertyId);
                var loResult = await _model.GetList_TaskchecklistAsync();
                if (loResult.Count > 0)
                {
                    _taskchecklist_list = new ObservableCollection<TaskchecklistDTO>(loResult);
                }
                else
                {
                    _taskchecklist_list = new ObservableCollection<TaskchecklistDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRecord_TaskchecklistAsync(TaskchecklistDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                _taskchecklist_record = R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRecord_TaskChecklist(TaskchecklistDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                poParam.CACTION = poCRUDMode switch
                {
                    eCRUDMode.AddMode => "NEW",
                    eCRUDMode.EditMode => "EDIT",
                    _ => ""
                };
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                _taskchecklist_record = R_FrontUtility.ConvertObjectToObject<TaskchecklistDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRecord_TaskChecklistAsync(TaskchecklistDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                await _model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ActiveInactive_TaskchecklistAsync(TaskchecklistDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.LACTIVE = !poParam.LACTIVE;
                await _model.ActiveInactive_TaskchecklistAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
