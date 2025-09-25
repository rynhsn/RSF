using Lookup_HDCOMMON.DTOs;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using Lookup_HDCOMMON.DTOs.HDL00300;
using Lookup_HDCOMMON.DTOs.HDL00400;
using Lookup_HDCOMMON.DTOs.HDL00500;

namespace Lookup_HDCOMMON
{
    public interface IPublicHDLookupRecord
    {
        HDLGenericRecord<HDL00100DTO> HDL00100GetRecord(HDL00100ParameterDTO poEntity);
        HDLGenericRecord<HDL00200DTO> HDL00200GetRecord(HDL00200ParameterDTO poEntity);
        HDLGenericRecord<HDL00300DTO> HDL00300GetRecord(HDL00300ParameterDTO poEntity);
        HDLGenericRecord<HDL00400DTO> HDL00400GetRecord(HDL00400ParameterDTO poEntity);
        HDLGenericRecord<HDL00500DTO> HDL00500GetRecord(HDL00500ParameterDTO poEntity);
    }
}