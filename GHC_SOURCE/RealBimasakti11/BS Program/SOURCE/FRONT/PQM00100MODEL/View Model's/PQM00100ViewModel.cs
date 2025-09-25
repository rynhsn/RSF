using PQM00100COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PQM00100MODEL.View_Model_s
{
    public class PQM00100ViewModel : R_ViewModel<ServiceGridDTO>
    {
        //var
        private PQM00100Model _model = new PQM00100Model();

        public ObservableCollection<ServiceGridDTO> ServiceList { get; set; } = new ObservableCollection<ServiceGridDTO>();
        public ServiceGridDTO ServiceRecord { get; set; } = new ServiceGridDTO();

        //methods
        public async Task GetList_ServiceAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetList_ServiceAsync();
                if (loResult != null)
                {
                    ServiceList = new ObservableCollection<ServiceGridDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRecord_ServiceAsync(ServiceGridDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poParam);
                if (loResult != null)
                {
                    ServiceRecord = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRecord_ServiceAsync(ServiceGridDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                switch (peCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        poNewEntity.CACTION_MODE = "A";
                        break;

                    case eCRUDMode.EditMode:
                        poNewEntity.CACTION_MODE = "U";
                        break;
                }
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                ServiceRecord = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteRecord_ServiceAsync(ServiceGridDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (poParam != null)
                {
                    poParam.CACTION_MODE = "D";
                    await _model.R_ServiceDeleteAsync(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}