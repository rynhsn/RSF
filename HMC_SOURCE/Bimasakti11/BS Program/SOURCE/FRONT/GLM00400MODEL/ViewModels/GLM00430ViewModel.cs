using GLM00400COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00430ViewModel : R_ViewModel<GLM00430DTO>
    {
        private GLM00430Model _GLM00430Model = new GLM00430Model();

        public ObservableCollection<GLM00430DTO> SourceAllocationAccountGrid { get; set; } = new ObservableCollection<GLM00430DTO>();
        public ObservableCollection<GLM00430DTO> AllocationAccountGrid { get; set; } = new ObservableCollection<GLM00430DTO>();

        public async Task GetSourceAllocationAccountList(GLM00430DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _GLM00430Model.GetSourceAllocationAccountListAsync();

                SourceAllocationAccountGrid = new ObservableCollection<GLM00430DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAllocationAccountList(GLM00431DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00430Model.GetAllocationAccountListAsync(poParam);
                var loTempResult = R_FrontUtility.ConvertCollectionToCollection<GLM00430DTO>(loResult);

                AllocationAccountGrid = new ObservableCollection<GLM00430DTO>(loTempResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveAllocationAccountList(GLM00431DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _GLM00430Model.SaveAllocationAccountListAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
