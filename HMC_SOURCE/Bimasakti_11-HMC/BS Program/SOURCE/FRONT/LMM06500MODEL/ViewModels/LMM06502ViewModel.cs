using LMM06500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM06500MODEL
{
    public class LMM06502ViewModel
    {
        private LMM06502Model _LMM06502Model = new LMM06502Model();

        public ObservableCollection<LMM06502DetailDTO> StaffMoveGrid { get; set; } = new ObservableCollection<LMM06502DetailDTO>();

        public LMM06502DTO StaffMove = new LMM06502DTO();

        public LMM06502HeaderDTO StaffMoveHeader = new LMM06502HeaderDTO();

        public string PropertyValueContext = "";
        public string DeptCode = "";

        public async Task GetStaffMoveList(LMM06502DetailDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(PropertyValueContext))
                    poParam.CPROPERTY_ID = PropertyValueContext;

                var loResult = await _LMM06502Model.GetStaffMoveListAsync(poParam);
                StaffMoveGrid = new ObservableCollection<LMM06502DetailDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveStaffMove(LMM06502DTO poNewEntity)
        {
            var loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loUserParameneters;
            R_IProcessProgressStatus loProgressStatus;

            try
            {
                //preapare Batch Parameter
                loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = poNewEntity.Header.CCOMPANY_ID;
                loUploadPar.USER_ID = poNewEntity.Header.CUSER_ID;
                loUploadPar.UserParameters = new List<R_KeyValue>();
                loUploadPar.ClassName = "LMM06500BACK.LMM06502Cls";
                loUploadPar.BigObject = poNewEntity;

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM");

                var loKeyGuid = await loCls.R_BatchProcess<LMM06502DTO>(loUploadPar, 1);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }

}
