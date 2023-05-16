using GFF00900COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GFF00900COMMON
{
    public interface IGFF00900
    {
        ValidationResultDTO UsernameAndPasswordValidationMethod();
        RSP_ACTIVITY_VALIDITYResultDTO RSP_ACTIVITY_VALIDITYMethod();
        //RSP_CREATE_ACTIVITY_APPROVAL_LOGResultDTO RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod();
    }
}
