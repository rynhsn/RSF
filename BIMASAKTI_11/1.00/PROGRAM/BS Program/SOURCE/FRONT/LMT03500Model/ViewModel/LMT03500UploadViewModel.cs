using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT03500Common;
using LMT03500Common.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace LMT03500Model.ViewModel
{
    public class LMT03500UploadViewModel : R_IProcessProgressStatus
    {
        public ObservableCollection<LMT03500UploadErrorValidateDTO> GridListUpload { get; set; } =
            new ObservableCollection<LMT03500UploadErrorValidateDTO>();

        public ELMT03500UtilityUsageType UtilityUsageType = new ELMT03500UtilityUsageType();
        
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        
        public int SumListExcel { get; set; }
        public bool VisibleError { get; set; } = false;
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }
        
        public async Task ConvertGridEC(List<LMT03500UploadExcelECDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //Onchanged Visible Error
                VisibleError = false;
                VisibleError = false;
                SumValidDataExcel = 0;
                SumInvalidDataExcel = 0;

                // Convert Excel DTO And Add SeqNo
                List<LMT03500UploadErrorValidateDTO> Data = poEntity.Select((item, i) => new LMT03500UploadErrorValidateDTO
                {
                    CSEQ_NO = item.DisplaySeq,
                    
                    CBUILDING_ID = item.BuildingId,
                    CDEPT_CODE = item.Department,
                    CREF_NO = item.AgreementNo,
                    CUTILITY_TYPE = item.UtilityType,
                    CFLOOR_ID = item.FloorId,
                    CUNIT_ID = item.UnitId,
                    CMETER_NO = item.MeterNo,
                    CUTILITY_PRD = item.UtilityPeriod,
                    CSTART_DATE = item.StartDate,
                    CEND_DATE = item.EndDate,
                    IBLOCK1_START = item.BlockIStart,
                    IBLOCK2_START = item.BlockIIStart,
                    IBLOCK1_END = item.BlockIEnd,
                    IBLOCK2_END = item.BlockIIEnd,
                    
                    CCOMPANY_ID = CompanyId
                }).ToList();

                SumListExcel = Data.Count;
                GridListUpload = new ObservableCollection<LMT03500UploadErrorValidateDTO>(Data);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        
        public Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            throw new NotImplementedException();
        }

        public Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            throw new NotImplementedException();
        }

        public Task ReportProgress(int pnProgress, string pcStatus)
        {
            throw new NotImplementedException();
        }
    }
}