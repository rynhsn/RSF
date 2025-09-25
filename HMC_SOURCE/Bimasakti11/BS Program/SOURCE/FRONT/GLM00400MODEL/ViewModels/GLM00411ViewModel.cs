using GLM00400COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00411ViewModel : R_ViewModel<GLM00412DTO>
    {
        private GLM00410Model _GLM00410Model = new GLM00410Model();
        public ObservableCollection<GLM00412DTO> AllocationCenterGrid { get; set; } = new ObservableCollection<GLM00412DTO>();
        public ObservableCollection<GLM00413DTO> AllocationCenterPeriodGrid { get; set; } = new ObservableCollection<GLM00413DTO>();
        public GLM00413DTO AllocationCenterPeriod { get; set; } = new GLM00413DTO();
        public int Year { get; set; } = DateTime.Now.Year;

        public async Task GetAllocationCenterList(GLM00412DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00410Model.GetAllocationTargetCenterListAsync(poParam);

                AllocationCenterGrid = new ObservableCollection<GLM00412DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAllocationCenterPeriodList(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00410Model.GetAllocationTargetCenterByPeriodListAsync(poParam);

                AllocationCenterPeriodGrid = new ObservableCollection<GLM00413DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAllocationCenterPeriod(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00410Model.GetAllocationTargetCenterByPeriodAsync(poParam);

                AllocationCenterPeriod = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveAllocationCenterPeriod(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00410Model.SaveAllocationTargetCenterByPeriodAsync(poParam);

                AllocationCenterPeriod = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
