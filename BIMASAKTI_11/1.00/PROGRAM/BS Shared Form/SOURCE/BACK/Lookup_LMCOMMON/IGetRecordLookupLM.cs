using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.GET_USER_PARAM_DETAIL;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMCOMMON.DTOs.LML01400;
using Lookup_PMCOMMON.DTOs.LML01500;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01700;
using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.LML01900;
using System.Collections.Generic;

namespace Lookup_PMCOMMON
{
    public interface IGetRecordLookupLM
    {
        LMLGenericRecord<LML00200DTO> LML00200UnitCharges(LML00200ParameterDTO poParam);
        LMLGenericRecord<LML00300DTO> LML00300Supervisor(LML00300ParameterDTO poParam);
        LMLGenericRecord<LML00400DTO> LML00400UtilityCharges(LML00400ParameterDTO poParam);
        LMLGenericRecord<LML00500DTO> LML00500Salesman(LML00500ParameterDTO poParam);
        LMLGenericRecord<LML00600DTO> LML00600Tenant(LML00600ParameterDTO poParam);
        LMLGenericRecord<LML00700DTO> LML00700Discount(LML00700ParameterDTO poParam);
        LMLGenericRecord<LML00800DTO> LML00800Agreement(LML00800ParameterDTO poParam);
        LMLGenericRecord<LML00900DTO> LML00900Transaction(LML00900ParameterDTO poParam);
        LMLGenericRecord<LML01000DTO> LML01000BillingRule(LML01000ParameterDTO poParam);
        LMLGenericRecord<LML01100DTO> LML01100TNC(LML01100ParameterDTO poParam);
        LMLGenericRecord<LML01200DTO> LML01200InvoiceGroup(LML01200ParameterDTO poParam);
        LMLGenericRecord<LML01300DTO> LML01300LOIAgreement(LML01300ParameterDTO poParam);
        LMLGenericRecord<LML01400DTO> LML01400AgreementUnitCharges(LML01400ParameterDTO poParam);
        LMLGenericRecord<LML01500DTO> LML01500SLACategory(LML01500ParameterDTO poParam);
        LMLGenericRecord<LML01600DTO> LML01600SLACallType(LML01600ParameterDTO poParam);
        LMLGenericRecord<LML01800DTO> LML01800UnitTenant(LML01800ParameterDTO poParam);
        LMLGenericRecord<LML01900DTO> LML01900Staff(LML01900ParamaterDTO poParam);

        //UPDATED 05/07/2024
        LMLGenericRecord<GET_USER_PARAM_DETAILDTO> UserParamDetail (GET_USER_PARAM_DETAILParameterDTO poParam); 
    }
}
