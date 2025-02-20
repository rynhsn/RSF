using BMM00500COMMON.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMM00500MODEL.ViewModel
{
    public class BMM00500ViewModel : R_ViewModel<BMM00500DTO>
    {
        private readonly BMM00500Model _model = new BMM00500Model();
        public ObservableCollection<BMM00500DTO> loMobileProgramList = new ObservableCollection<BMM00500DTO>();
        public BMM00500DTO loSelectedMobileProgram = new BMM00500DTO();

        public async Task GetList()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetMobileProgramListAsync();
                loMobileProgramList = new ObservableCollection<BMM00500DTO>(loResult.Data);
                loSelectedMobileProgram = loMobileProgramList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceGetRecord(BMM00500DTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(ConvertToCRUDParameter<BMM00500DTO, BMM00500CRUDParameterDTO>(poEntity));
                loSelectedMobileProgram = ConvertFromCRUDParameter<BMM00500CRUDParameterDTO, BMM00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(BMM00500DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(ConvertToCRUDParameter<BMM00500DTO, BMM00500CRUDParameterDTO>(poEntity), peCRUDMode);
                loSelectedMobileProgram = ConvertFromCRUDParameter<BMM00500CRUDParameterDTO, BMM00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(BMM00500DTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(ConvertToCRUDParameter<BMM00500DTO, BMM00500CRUDParameterDTO>(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private T ConvertToCRUDParameter<F, T>(F poEntity) 
            where F : class, new() 
            where T : CRUDParameterDTO<F>, new()
        {
            F loEntity = poEntity ?? new F();
            return new T() { Data = loEntity };
        }

        private T ConvertFromCRUDParameter<F, T>(F poCRUDEntity)
            where F : CRUDParameterDTO<T>, new()
            where T : class, new()
        {
            T loEntity = poCRUDEntity.Data ?? new T();
            return loEntity;
        }
    }
}
