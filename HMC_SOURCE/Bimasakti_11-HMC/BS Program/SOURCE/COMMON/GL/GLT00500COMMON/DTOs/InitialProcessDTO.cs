using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON.DTOs
{
    public class InitialProcessDTO : R_APIResultBaseDTO
    {
        public GetCompanyDTO CompanyData { get; set; }
        public GetSystemParamDTO SystemParamData { get; set; }
        public List<GetDeptLookUpListDTO> DeptLookUpListData { get; set; }
        public GetSoftPeriodStartDateDTO SoftPeriodStartDateData { get; set; }
        public GetUndoCommitJrnDTO UndoCommitJrnData { get; set; }
        public GetTransactionCodeDTO TransactionCodeData { get; set; }
        public GetPeriodDTO PeriodData { get; set; }
        public GetCurrentPeriodStartDateDTO CurrentPeriodStartDateData { get; set; }
    }
}
