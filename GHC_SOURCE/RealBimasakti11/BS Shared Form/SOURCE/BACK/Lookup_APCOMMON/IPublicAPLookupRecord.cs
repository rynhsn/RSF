using Lookup_APCOMMON.DTOs;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APCOMMON.DTOs.APL00600;
using Lookup_APCOMMON.DTOs.APL00700;

namespace Lookup_APCOMMON
{
    public interface IPublicAPLookupRecord
    {
        APLGenericRecord<APL00100DTO> APL00100GetRecord(APL00100ParameterDTO poEntity);
        APLGenericRecord<APL00200DTO> APL00200GetRecord(APL00200ParameterDTO poEntity);
        APLGenericRecord<APL00300DTO> APL00300GetRecord(APL00300ParameterDTO poEntity);
        APLGenericRecord<APL00110DTO> APL00110GetRecord(APL00110ParameterDTO poEntity);
        APLGenericRecord<APL00400DTO> APL00400GetRecord(APL00400ParameterDTO poEntity);
        APLGenericRecord<APL00500DTO> APL00500GetRecord(APL00500ParameterDTO poEntity);
        APLGenericRecord<APL00600DTO> APL00600GetRecord(APL00600ParameterDTO poEntity);
        APLGenericRecord<APL00700DTO> APL00700GetRecord(APL00700ParameterDTO poEntity);

    }
}
