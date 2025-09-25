using Lookup_GSCOMMON.DTOs;
using System.Collections.Generic;

namespace Lookup_GSLBACK
{
    public interface IPublicRecordLookup
    {
        Task<GSLGenericRecord<GSL00100DTO>> GSL00100GetSalesTax(GSL00100ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00110DTO>> GSL00110GetTaxByDate(GSL00110ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00200DTO>> GSL00200GetWithholdingTax(GSL00200ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00300DTO>> GSL00300GetCurrency(GSL00300ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00400DTO>> GSL00400GetJournalGroup(GSL00400ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00500DTO>> GSL00500GetGLAccount(GSL00500ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00510DTO>> GSL00510GetCOA(GSL00510ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00520DTO>> GSL00520GetGOACOA(GSL00520ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00550DTO>> GSL00550GetGOA(GSL00550ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00600DTO>> GSL00600GetUnitTypeCategory(GSL00600ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00700DTO>> GSL00700GetDepartment(GSL00700ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00710DTO>> GSL00710GetDepartmentProperty(GSL00710ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00800DTO>> GSL00800GetCurrencyType(GSL00800ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL00900DTO>> GSL00900GetCenter(GSL00900ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL01000DTO>> GSL01000GetUser(GSL01000ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL01100DTO>> GSL01100GetUserApproval(GSL01100ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL01200DTO>> GSL01200GetBank(GSL01200ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL01300DTO>> GSL01300GetBankAccount(GSL01300ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL01400DTO>> GSL01400GetOtherCharges(GSL01400ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL01500ResultDetailDTO>> GSL01500GetCashDetail(GSL01500ParameterDetailDTO poEntity);
        Task<GSLGenericRecord<GSL01600DTO>> GSL01600GetCashFlowGroupType(GSL01600ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL01800DTO>> GSL01800GetCategory(GSL01800DTOParameter poEntity);
        Task<GSLGenericRecord<GSL01900DTO>> GSL01900GetLOB(GSL01900ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02000CityDTO>> GSL02000GetCityGeography(GSL02000ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02100DTO>> GSL02100GetPaymentTerm(GSL02100ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02200DTO>> GSL02200GetBuilding(GSL02200ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02300DTO>> GSL02300GetBuildingUnit(GSL02300ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02400DTO>> GSL02400GetFloor(GSL02400ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02500DTO>> GSL02500GetCB(GSL02500ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02510DTO>> GSL02510GetCashBank(GSL02510ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02600DTO>> GSL02600GetCBAccount(GSL02600ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02700DTO>> GSL02700GetOtherUnit(GSL02700ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02800DTO>> GSL02800GetOtherUnitMaster(GSL02800ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02900DTO>> GSL02900GetSupplier(GSL02900ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL02910DTO>> GSL02910GetSupplierInfo(GSL02910ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03000DTO>> GSL03000GetProduct(GSL03000ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03010DTO>> GSL03010GetProductUnit(GSL03010ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03020DTO>> GSL03020GetProductUOM(GSL03020ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03100DTO>> GSL03100GetExpenditure(GSL03100ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03200DTO>> GSL03200GetProductAllocation(GSL03200ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03300DTO>> GSL03300GetTaxCharges(GSL03300ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03400DTO>> GSL03400GetDigitalSign(GSL03400ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03500DTO>> GSL03500GetWarehouse(GSL03500ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03600DTO>> GSL03600GetCompany(GSL03600ParameterDTO poEntity);
        Task<GSLGenericRecord<GSL03700DTO>> GSL03700GetMessage(GSL03700ParameterDTO poEntity);
    }
}
