using Lookup_GSCOMMON.DTOs;
using System.Collections.Generic;

namespace Lookup_GSCOMMON
{
    public interface IPublicRecordLookup
    {
        GSLGenericRecord<GSL00100DTO> GSL00100GetSalesTax(GSL00100ParameterDTO poEntity);
        GSLGenericRecord<GSL00110DTO> GSL00110GetTaxByDate(GSL00110ParameterDTO poEntity);
        GSLGenericRecord<GSL00200DTO> GSL00200GetWithholdingTax(GSL00200ParameterDTO poEntity);
        GSLGenericRecord<GSL00300DTO> GSL00300GetCurrency(GSL00300ParameterDTO poEntity);
        GSLGenericRecord<GSL00400DTO> GSL00400GetJournalGroup(GSL00400ParameterDTO poEntity);
        GSLGenericRecord<GSL00500DTO> GSL00500GetGLAccount(GSL00500ParameterDTO poEntity);
        GSLGenericRecord<GSL00510DTO> GSL00510GetCOA(GSL00510ParameterDTO poEntity);
        GSLGenericRecord<GSL00520DTO> GSL00520GetGOACOA(GSL00520ParameterDTO poEntity);
        GSLGenericRecord<GSL00550DTO> GSL00550GetGOA(GSL00550ParameterDTO poEntity);
        GSLGenericRecord<GSL00600DTO> GSL00600GetUnitTypeCategory(GSL00600ParameterDTO poEntity);
        GSLGenericRecord<GSL00700DTO> GSL00700GetDepartment(GSL00700ParameterDTO poEntity);
        GSLGenericRecord<GSL00710DTO> GSL00710GetDepartmentProperty(GSL00710ParameterDTO poEntity);
        GSLGenericRecord<GSL00800DTO> GSL00800GetCurrencyType(GSL00800ParameterDTO poEntity);
        GSLGenericRecord<GSL00900DTO> GSL00900GetCenter(GSL00900ParameterDTO poEntity);
        GSLGenericRecord<GSL01000DTO> GSL01000GetUser(GSL01000ParameterDTO poEntity);
        GSLGenericRecord<GSL01100DTO> GSL01100GetUserApproval(GSL01100ParameterDTO poEntity);
        GSLGenericRecord<GSL01200DTO> GSL01200GetBank(GSL01200ParameterDTO poEntity);
        GSLGenericRecord<GSL01300DTO> GSL01300GetBankAccount(GSL01300ParameterDTO poEntity);
        GSLGenericRecord<GSL01400DTO> GSL01400GetOtherCharges(GSL01400ParameterDTO poEntity);
        //GSLGenericRecord<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroup();
        GSLGenericRecord<GSL01500ResultDetailDTO> GSL01500GetCashDetail(GSL01500ParameterDetailDTO poEntity);
        GSLGenericRecord<GSL01600DTO> GSL01600GetCashFlowGroupType(GSL01600ParameterDTO poEntity);
        //GSLGenericRecord<GSL01700DTO> GSL01700GetCurrencyRate();
        //GSLGenericRecord<GSL01701DTO> GSL01700GetRateType();
        //GSLGenericRecord<GSL01702DTO> GSL01700GetLocalAndBaseCurrency();
        GSLGenericRecord<GSL01800DTO> GSL01800GetCategory(GSL01800DTOParameter poEntity);
        GSLGenericRecord<GSL01900DTO> GSL01900GetLOB(GSL01900ParameterDTO poEntity);
        //GSLGenericRecord<GSL02000CountryDTO> GSL02000GetCountryGeography();
        //GSLGenericRecord<GSL02000CityDTO> GSL02000GetCityGeography();
        GSLGenericRecord<GSL02100DTO> GSL02100GetPaymentTerm(GSL02100ParameterDTO poEntity);
        GSLGenericRecord<GSL02200DTO> GSL02200GetBuilding(GSL02200ParameterDTO poEntity);
        GSLGenericRecord<GSL02300DTO> GSL02300GetBuildingUnit(GSL02300ParameterDTO poEntity);
        GSLGenericRecord<GSL02400DTO> GSL02400GetFloor(GSL02400ParameterDTO poEntity);
        GSLGenericRecord<GSL02500DTO> GSL02500GetCB(GSL02500ParameterDTO poEntity);
        GSLGenericRecord<GSL02600DTO> GSL02600GetCBAccount(GSL02600ParameterDTO poEntity);
        GSLGenericRecord<GSL02700DTO> GSL02700GetOtherUnit(GSL02700ParameterDTO poEntity);
    }
}
