using GSM04500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM04500Model
{
    public class GSM04501ViewModel : R_ViewModel<GSM04510GOADTO>
    {
        private GSM04501Model _model = new GSM04501Model();
        public ObservableCollection<GSM04510GOADTO> GOAList = new ObservableCollection<GSM04510GOADTO>();
        public GSM04510GOADTO GOA { get; set; } = new GSM04510GOADTO();

        public GSM04500DTO CurrentJournalGroup = new GSM04500DTO();
        public GSM04510GOADTO CurrentGOA = new GSM04510GOADTO();



        public async Task GetAllJournalGrupGOAAsync(string lcJournalGRPType, string lcJournalGRPCode)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _model.GetAllGOAListAsync(lcJournalGRPType, lcJournalGRPCode);
                if (loResult.ListData.Count > 0)
                {
                    GOAList = new ObservableCollection<GSM04510GOADTO>(loResult.ListData);
                    CurrentGOA = GOAList.FirstOrDefault();
                }
                else
                {
                    CurrentGOA = null!;
                    GOAList = new ObservableCollection<GSM04510GOADTO>();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task<GSM04510GOADTO> GetGOAOneRecord(GSM04510GOADTO poEntity)
        {
            var loEx = new R_Exception();
            GSM04510GOADTO loResult = null;
            try
            {
                var loParam = new GSM04510GOADTO
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CUSER_ID = poEntity.CUSER_ID,
                    CPROPERTY_ID = "",
                    CJRNGRP_TYPE = poEntity.CJRNGRP_TYPE,
                    CJRNGRP_CODE = poEntity.CJRNGRP_CODE,
                    CGOA_CODE = poEntity.CGOA_CODE
                };
                loResult = await _model.R_ServiceGetRecordAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<GSM04510GOADTO> SaveGOA(GSM04510GOADTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM04510GOADTO loResult = null;

            try
            {
                loResult = await _model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                GOA = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    
    }
}
