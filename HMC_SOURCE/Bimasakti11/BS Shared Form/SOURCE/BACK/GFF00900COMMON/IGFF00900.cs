using GFF00900COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GFF00900COMMON
{
    public interface IGFF00900
    {
        ValidationResultDTO UsernameAndPasswordValidationMethod(GFF00900DTO poParam);
        RSP_ACTIVITY_VALIDITYResultDTO RSP_ACTIVITY_VALIDITYMethod(RSP_ACTIVITY_VALIDITYParameterDTO poParam);
        //RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod();
    }
}
