using Lookup_GLCOMMON.DTOs;
using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;

namespace Lookup_GLCOMMON
{
    public interface IPublicLookupGetRecordGL
    {
        GLLGenericRecord<GLL00100DTO> GLL00100ReferenceNoLookUp(GLL00100ParameterGetRecordDTO poParameter);
        GLLGenericRecord<GLL00110DTO> GLL00100ReferenceNoLookUpByPeriod(GLL00110ParameterGetRecordDTO poParameter);
    }
}
