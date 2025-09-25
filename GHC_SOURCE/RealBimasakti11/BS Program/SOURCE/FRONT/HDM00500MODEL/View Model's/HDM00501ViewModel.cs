using HDM00500COMMON;
using HDM00500COMMON.DTO_s;
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
    public class HDM00501ViewModel
    {
        //var
        private HDM00501Model _model = new HDM00501Model();
        public ObservableCollection<ChecklistDTO> _checklist_list { get; set; } = new ObservableCollection<ChecklistDTO>();
        public ChecklistDTO _checklist_record { get; set; } = new ChecklistDTO();
        public string _propertyId { get; set; } = "";
        public string _taskChecklistID { get; set; } = "";

        //method
        public async Task GetList_Checklist()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(Taskchecklist_ContextConstant.CPROPERTY_ID, _propertyId);
                R_FrontContext.R_SetStreamingContext(Taskchecklist_ContextConstant.CTASK_CHECKLIST_ID, _taskChecklistID);

                var loResult = await _model.GetList_ChecklistAsync();
                _checklist_list = new ObservableCollection<ChecklistDTO>(loResult)??new ObservableCollection<ChecklistDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRecord_ChecklistAsync(ChecklistDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                poParam.CTASK_CHECKLIST_ID = _taskChecklistID;
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                _checklist_record = R_FrontUtility.ConvertObjectToObject<ChecklistDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRecord_Checklist(ChecklistDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                poParam.CTASK_CHECKLIST_ID = _taskChecklistID;
                poParam.CACTION = poCRUDMode switch
                {
                    eCRUDMode.AddMode => "NEW",
                    eCRUDMode.EditMode => "EDIT",
                    _ => ""
                };
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                _checklist_record = R_FrontUtility.ConvertObjectToObject<ChecklistDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteRecord_ChecklistAsync(ChecklistDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CPROPERTY_ID = _propertyId;
                poParam.CTASK_CHECKLIST_ID = _taskChecklistID;
                await _model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}