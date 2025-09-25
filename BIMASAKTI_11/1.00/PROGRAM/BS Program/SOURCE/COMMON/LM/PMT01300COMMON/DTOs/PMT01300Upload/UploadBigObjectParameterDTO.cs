using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public class PMT01300UploadBigObjectParameterDTO
    {
        public List<PMT01300UploadAgreementDTO>? AgreementList { get; set; }
        public List<PMT01300UploadUnitDTO>? UnitList { get; set; }
        public List<PMT01300UploadUtilitiesDTO>? UtilityList { get; set; }
        public List<PMT01300UploadChargesDTO>? ChargesList { get; set; }
        public List<PMT01300UploadDepositDTO>? DepositList { get; set; }
    }
}
