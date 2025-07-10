using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON.DTOs.PMT02130;
using PMT02100COMMON;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02131ViewModel : R_ViewModel<PMT02100HandoverBuildingDTO>
    {
        private PMT02130Model loModel = new PMT02130Model();

        public PMT02100HandoverDTO loHeader = new PMT02100HandoverDTO();

        public PMT02130HandoverUnitDTO loHandoverUnit = new PMT02130HandoverUnitDTO();

        public PMT02130HandoverUnitChecklistDTO loHandoverUnitChecklist = new PMT02130HandoverUnitChecklistDTO();

        public ObservableCollection<PMT02130HandoverUnitDTO> loHandoverUnitList = new ObservableCollection<PMT02130HandoverUnitDTO>();

        public ObservableCollection<PMT02130HandoverUnitChecklistDTO> loHandoverUnitChecklistList = new ObservableCollection<PMT02130HandoverUnitChecklistDTO>();

        //public PMT02100TabParameterDTO loTabParameter = null;

        public async Task GetHandoverUnitListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02130HandoverUnitParameterDTO loParam = null;
            PMT02130HandoverUnitResultDTO loRtn = null;
            try
            {
                loParam = new PMT02130HandoverUnitParameterDTO()
                {
                    CPROPERTY_ID = loHeader.CPROPERTY_ID,
                    CDEPT_CODE = loHeader.CDEPT_CODE,
                    CREF_NO = loHeader.CREF_NO,
                    CTRANS_CODE = loHeader.CTRANS_CODE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02130_GET_HANDOVER_UNIT_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loModel.GetHandoverUnitListStreamAsync();
                loHandoverUnitList = new ObservableCollection<PMT02130HandoverUnitDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetHandoverUnitChecklistListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02130HandoverUnitChecklistParameterDTO loParam = null;
            PMT02130HandoverUnitChecklistResultDTO loRtn = null;
            try
            {
                loParam = new PMT02130HandoverUnitChecklistParameterDTO()
                {
                    CPROPERTY_ID = loHeader.CPROPERTY_ID,
                    CDEPT_CODE = loHeader.CDEPT_CODE,
                    CREF_NO = loHeader.CREF_NO,
                    CTRANS_CODE = loHeader.CTRANS_CODE,
                    CUNIT_ID = loHandoverUnit.CUNIT_ID,
                    CFLOOR_ID = loHandoverUnit.CFLOOR_ID,
                    CBUILDING_ID = loHeader.CBUILDING_ID
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02130_GET_HANDOVER_UNIT_CHECKLIST_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loModel.GetHandoverUnitChecklistListStreamAsync();

                loRtn.Data.ForEach(x => {
                    x.CBASE_QUANTITY = x.IBASE_QUANTITY == 0 ? "-" : x.IBASE_QUANTITY.ToString();
                    x.CACTUAL_QUANTITY = x.IBASE_QUANTITY == 0 ? "-" : x.IACTUAL_QUANTITY.ToString();
                });

                loHandoverUnitChecklistList = new ObservableCollection<PMT02130HandoverUnitChecklistDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}