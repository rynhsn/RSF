using GLM00400COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00420ViewModel : R_ViewModel<GLM00420DTO>
    {
        private GLM00420Model _GLM00420Model = new GLM00420Model();

        public ObservableCollection<GLM00420DTO> SourceAllocationCenterGrid { get; set; } = new ObservableCollection<GLM00420DTO>();
        public ObservableCollection<GLM00420DTO> AllocationCenterGrid { get; set; } = new ObservableCollection<GLM00420DTO>();

        public async Task GetSourceAllocationCenterList(GLM00420DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00420Model.GetSourceAllocationCenterListAsync(poParam);

                SourceAllocationCenterGrid = new ObservableCollection<GLM00420DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAllocationCenterList(GLM00421DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00420Model.GetAllocationCenterListAsync(poParam);
                var loTempResult = R_FrontUtility.ConvertCollectionToCollection<GLM00420DTO>(loResult);

                AllocationCenterGrid = new ObservableCollection<GLM00420DTO>(loTempResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveAllocationCenterList(GLM00421DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _GLM00420Model.SaveAllocationCenterListAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
