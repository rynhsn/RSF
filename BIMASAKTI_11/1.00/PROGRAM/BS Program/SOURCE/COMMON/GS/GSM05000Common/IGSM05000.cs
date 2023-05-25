using System.Collections.Generic;
using GSM05000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM05000Common
{
    public interface IGSM05000 : R_IServiceCRUDBase<GSM05000DTO>
    {
        //ambil data dari database
        GSM05000ListDTO<GSM05000GridDTO> GetTransactionCodeList();
        GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList();
    }
}