using PMT02000COMMON.Upload.Agreement;
using PMT02000COMMON.Upload.Unit;
using PMT02000COMMON.Upload.Utility;
using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace PMT02000COMMON.DownloadTemplate
{
    public class SaveMultiListDataExcelDTO : R_APIResultBaseDTO
    {
        public List<SaveAgreementToExcelDTO>? AgreementListExcel { get; set; }
        public List<SaveUnitToExcelDTO>? UnitListExcel { get; set; }
        public List<SaveUtilityToExcelDTO>? UtilityListExcel { get; set; }
    }
}
