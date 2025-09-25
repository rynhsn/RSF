using PMT02000COMMON.Upload.Agreement;
using PMT02000COMMON.Upload.Unit;
using PMT02000COMMON.Upload.Utility;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Upload
{
    public class PMT0200MultiListDataDTO  : R_APIResultBaseDTO
    {
        public List<AgreementUploadDTO>? AgreementList {  get; set; }
        public List<UnitUploadDTO>? UnitList { get; set; }
        public List<UtilityUploadDataFromDBDTO>? UtilityList { get; set; }
    }
}
