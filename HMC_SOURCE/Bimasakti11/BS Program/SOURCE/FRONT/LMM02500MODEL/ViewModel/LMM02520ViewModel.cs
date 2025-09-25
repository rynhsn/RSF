using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMM02500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMM02500Model.ViewModel
{
    public class LMM02520ViewModel : R_ViewModel<LMM02520DTO>
    {
        private readonly LMM02520Model _model = new LMM02520Model();
        public LMM02520DTO loEntityLMM02520 = new LMM02520DTO();
        public ObservableCollection<LMM02520GridDTO> loGridListLMM02520 = new ObservableCollection<LMM02520GridDTO>();
        public LMM02500TabParameterDTO loTabParameter = new LMM02500TabParameterDTO();

        public async Task GetAllTenantGroupList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetAllTenantGroupListStreamAsync(pcCPROPERTY_ID: loTabParameter.CPROPERTY_ID, pcCTENANT_GROUP_ID: loTabParameter.CTENANT_GROUP_ID);

                loGridListLMM02520 = new ObservableCollection<LMM02520GridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetEntity(LMM02520DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);

                loEntityLMM02520 = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}