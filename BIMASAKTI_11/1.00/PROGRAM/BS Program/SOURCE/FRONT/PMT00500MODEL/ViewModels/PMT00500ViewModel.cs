using PMT00500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00500ViewModel : R_ViewModel<PMT00500DTO>
    {
        private PMT00500InitModel _PMT00500InitModel = new PMT00500InitModel();

        #region Property Class
        public ObservableCollection<PMT00500BuildingUnitDTO> BuildingUnitGrid { get; set; } = new ObservableCollection<PMT00500BuildingUnitDTO>();
        public ObservableCollection<PMT00500BuildingUnitDTO> SelectedBuildingUnitGrid { get; set; } = new ObservableCollection<PMT00500BuildingUnitDTO>();
        public List<PMT00500PropertyDTO> PropertyList { get; set; } = new List<PMT00500PropertyDTO>();
        public PMT00500BuildingDTO BuildingUnit = new PMT00500BuildingDTO();
        public string PROPERTY_ID { get; set; } = "";
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMT00500InitModel.GetAllPropertyListAsync();

                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingUnitList()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500BuildingUnitDTO>(BuildingUnit);
                var loResult = await _PMT00500InitModel.GetAllBuildingUnitListAsync(loParam);
                if (loResult.Count > 0)
                {
                    loResult.ForEach(x =>
                    {
                        x.DSTRATA_HO_ACTUAL_DATE = DateTime.TryParseExact(x.CSTRATA_HO_ACTUAL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStrataHOActualDate) ? (DateTime?)ldStrataHOActualDate : null;
                    });
                }
                
                BuildingUnitGrid = new ObservableCollection<PMT00500BuildingUnitDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetSelectedBuildingUnitList(List<PMT00500BuildingUnitDTO> poListEntity)
        {
            var loEx = new R_Exception();

            try
            {
                
                SelectedBuildingUnitGrid = new ObservableCollection<PMT00500BuildingUnitDTO>(poListEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
