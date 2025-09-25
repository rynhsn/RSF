using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02620;
using PMT01400COMMON.DTOs.Helper;
using PMT02600COMMON.DTOs.Helper;
using PMT02600COMMON.DTOs;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02620UtilitiesViewModel : R_ViewModel<PMT02620UtilitiesDTO>
    {
        private PMT02620UtilitiesModel loModel = new PMT02620UtilitiesModel();
        #region Property Class
        public PMT02620UtilitiesDTO loUtilities { get; set; } = new PMT02620UtilitiesDTO();
        public List<CodeDescDTO> VAR_UTILITY_TYPE { get; set; } = new List<CodeDescDTO>();
        public LinkedList<PMT02620AgreementBuildingUtilitiesDTO> VAR_METER_NO { get; set; } = new LinkedList<PMT02620AgreementBuildingUtilitiesDTO>();
        public ObservableCollection<PMT02620UtilitiesDTO> loUtilitiesList { get; set; } = new ObservableCollection<PMT02620UtilitiesDTO>();
        public PMT02620UnitDTO loSelectedUnit { get; set; } = new PMT02620UnitDTO();
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            List<CodeDescDTO> loUniversalListResult = null;

            try
            {
                loUniversalListResult = await loModel.GetUtilitiyChargesTypeStreamAsync();
                VAR_UTILITY_TYPE = loUniversalListResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementUtilitiesList(PMT02620UtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                //poEntity.CDEPT_CODE = loSelectedUnit.CDEPT_CODE;
                var loResult = await loModel.GetAgreementUtilitiesStreamAsync(poEntity);
                loResult.ForEach(x => x.CCHARGES_ID_NAME = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")");

                loUtilitiesList = new ObservableCollection<PMT02620UtilitiesDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementUtilities(PMT02620UtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                //poEntity.COTHER_UNIT_ID = loSelectedUnit.COTHER_UNIT_ID;
                var loResult = await loModel.R_ServiceGetRecordAsync(poEntity);

                await GetBuildingUtilitiesList(loResult);
                loUtilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveAgreementUtilities(PMT02620UtilitiesDTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                var loResult = await loModel.R_ServiceSaveAsync(poEntity, poCRUDMode);

                loUtilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteAgreementUtilities(PMT02620UtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await loModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetBuildingUtilitiesList(PMT02620UtilitiesDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.COTHER_UNIT_ID = loSelectedUnit.COTHER_UNIT_ID;
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02620ParameterAgreementBuildingUtilitiesDTO>(poEntity);
                var loResult = await loModel.GetAllBuildingUtilitiesListAsync(loParam);

                VAR_METER_NO = new LinkedList<PMT02620AgreementBuildingUtilitiesDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
