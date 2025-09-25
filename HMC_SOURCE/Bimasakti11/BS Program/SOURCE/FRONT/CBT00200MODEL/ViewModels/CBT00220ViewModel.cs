using CBT00200COMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CBT00200MODEL
{
    public class CBT00220ViewModel 
    {
        #region Model
        private CBT00200InitialProcessModel _CBT00200InitialProcessModel = new CBT00200InitialProcessModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private CBT00200Model _CBT00200Model = new CBT00200Model();
        #endregion

        #region Public Property ViewModel
        public DateTime? RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        public CBT00200DTO Journal { get; set; } = new CBT00200DTO();
        public CBT00210DTO JournalDetail { get; set; } = new CBT00210DTO();
        public bool FlagIsCopy { get; set; } = false;
        #endregion

        public async Task SaveJournalDeposit(CBT00200DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new CBT00200SaveParamDTO { Data = poEntity, CRUDMode = poCRUDMode, PARAM_CALLER = ePARAM_CALLER.DEPOSIT  };
                var loResult = await _CBT00200Model.SaveJournalRecordAsync(loParam);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
