using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.PMT02620;
using PMT02600COMMON.DTOs.Helper;
using PMT02600COMMON.DTOs.PMT02610;
using PMT02600COMMON.DTOs;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02620UnitViewModel : R_ViewModel<PMT02620UnitDTO>
    {
        private PMT02620UnitModel loUnitModel = new PMT02620UnitModel();
        private PMT02610Model loAgreementModel = new PMT02610Model();

        #region Property Class
        public ObservableCollection<PMT02620UnitDTO> loUnitList = new ObservableCollection<PMT02620UnitDTO>();
        public TabParameterDTO loTabParameter { get; set; } = new TabParameterDTO();

        public PMT02610DTO loHeader = new PMT02610DTO();
        public PMT02620UnitDTO loUnit { get; set; } = new PMT02620UnitDTO();

        #endregion

        public async Task GetHeaderAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await loAgreementModel.R_ServiceGetRecordAsync(new PMT02610ParameterDTO()
                {
                    Data = new PMT02610DTO()
                    {
                        CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                        CDEPT_CODE = loTabParameter.CDEPT_CODE,
                        CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE,
                        CREF_NO = loTabParameter.CREF_NO
                    }
                });

                loResult.Data.DSTART_DATE = DateTime.ParseExact(loResult.Data.CSTART_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loResult.Data.DEND_DATE = DateTime.ParseExact(loResult.Data.CEND_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                loHeader = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementUnitList()
        {
            var loEx = new R_Exception();

            try
            {
                //poEntity.CPROPERTY_ID = loTabParameter.CPROPERTY_ID;
                //poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                //poEntity.CDEPT_CODE = loTabParameter.CDEPT_CODE;
                //poEntity
                var loResult = await loUnitModel.GetAgreementUnitListStreamAsync(new PMT02620UnitDTO()
                {
                    CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                    CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE,
                    CDEPT_CODE = loTabParameter.CDEPT_CODE,
                    CREF_NO = loTabParameter.CREF_NO
                });

                loUnitList = new ObservableCollection<PMT02620UnitDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementUnit(PMT02620UnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = loTabParameter.CPROPERTY_ID;
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                poEntity.CDEPT_CODE = loTabParameter.CDEPT_CODE;
                var loResult = await loUnitModel.R_ServiceGetRecordAsync(poEntity);

                loUnit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveAgreementUnit(PMT02620UnitDTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = loTabParameter.CPROPERTY_ID;
                poEntity.CDEPT_CODE = loTabParameter.CDEPT_CODE;
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                var loResult = await loUnitModel.R_ServiceSaveAsync(poEntity, poCRUDMode);

                loUnit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteAgreementUnit(PMT02620UnitDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = loTabParameter.CPROPERTY_ID;
                poEntity.CDEPT_CODE = loTabParameter.CDEPT_CODE;
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                await loUnitModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
